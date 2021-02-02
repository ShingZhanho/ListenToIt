﻿using System;
using System.Linq;
using ListenToIt.Updater.CmdOptions;
using Octokit;

namespace ListenToIt.Updater {
    public partial class Program {
        private class UpdateDownloader {
            private readonly UpdateOptions _options; // Options fetched from command line
            
            // Information fetched from server
            private Release _latestRelease;
            private Version _latestVersion;
            private readonly Version _currentVersion;

            public UpdateDownloader(UpdateOptions options) {
                _options = options;
                _currentVersion = new Version(options.CurrentVersion);
                FetchFromServer();
            }

            private void FetchFromServer() {
                var client = new GitHubClient(new ProductHeaderValue("ListenToIt"));
                // Apply user password if specified
                if (_options.UserCredentials != null)
                    client.Credentials = new Credentials(_options.UserCredentials.ToList()[0],
                            _options.UserCredentials.ToList()[1]);

                var releases = client.Repository.Release.GetAll("ShingZhanho", "ListenToIt").Result;
                foreach (var release in releases) {
                    if (!_options.IncludePrerelease) if (release.Prerelease) continue; // Skip for beta versions
                    _latestRelease = release;
                    _latestVersion = new Version(release.TagName);
                }
            }

            public bool IsUpToDate() {
                return !_latestVersion.IsNewerThan(_currentVersion);
            }
        }

        public class Version {
            public Version(int major, int minor, int patch, int revision, VersionSuffix suffix) {
                Major = major;
                Minor = minor;
                Patch = patch;
                Revision = revision;
                Suffix = suffix;
            }

            /// <summary>
            /// Construct a Version instance.
            /// </summary>
            /// <param name="rawVersion">rawVersion should follow "Major.Minor.Patch.Revision-Suffix" format.</param>
            public Version(string rawVersion) : this (
                Convert.ToInt32(rawVersion.Split('-')[0].Split('.')[0]),
                Convert.ToInt32(rawVersion.Split('-')[0].Split('.')[1]),
                Convert.ToInt32(rawVersion.Split('-')[0].Split('.')[2]),
                Convert.ToInt32(rawVersion.Split('-')[0].Split('.')[3]),
                rawVersion.Split('-')[1] == "stable" ? VersionSuffix.Stable : VersionSuffix.Beta
                ){}
            
            public int Major { get; }
            public int Minor { get; }
            public int Patch { get; }
            public int Revision { get; }
            public VersionSuffix Suffix { get; }

            public bool IsNewerThan(Version ver) {
                if (Major > ver.Major) return true;
                if (Minor > ver.Major) return true;
                if (Patch > ver.Patch) return true;
                return Revision > ver.Revision;
            }

            public enum VersionSuffix { Stable, Beta }
        }
    }
}