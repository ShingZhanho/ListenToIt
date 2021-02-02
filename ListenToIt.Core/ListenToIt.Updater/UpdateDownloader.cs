using System;
using System.IO;
using System.Linq;
using System.Net;
using ListenToIt.Updater.CmdOptions;
using Octokit;

namespace ListenToIt.Updater {
    public partial class Program {
        private class UpdateDownloader {
            private readonly UpdateOptions _options; // Options fetched from command line
            
            // Information fetched from server
            public Release LatestRelease;
            public Version LatestVersion;
            public readonly Version CurrentVersion;

            public UpdateDownloader(UpdateOptions options) {
                _options = options;
                CurrentVersion = new Version(options.CurrentVersion);
                FetchFromServer();
            }

            private void FetchFromServer() {
                // Fetch the latest version from GitHub repository
                var client = new GitHubClient(new ProductHeaderValue("ListenToIt"));
                // Apply user password if specified
                if (_options.UserCredentials.ToList().Count == 2)
                    client.Credentials = new Credentials(_options.UserCredentials.ToList()[0],
                            _options.UserCredentials.ToList()[1]);

                var releases = client.Repository.Release.GetAll("ShingZhanho", "ListenToIt").Result;
                foreach (var release in releases) {
                    if (!_options.IncludePrerelease) if (release.Prerelease) continue; // Skip for beta versions
                    LatestRelease = release;
                    LatestVersion = new Version(release.TagName);
                }
            }

            public void DownloadPackage(Uri url) {
                var webClient = new WebClient();
                webClient.DownloadFile(url, Path.Combine(_options.DownloadDir, Path.GetFileName(url.LocalPath)));
            }

            public bool IsUpToDate() {
                return !LatestVersion.IsNewerThan(CurrentVersion);
            }

            /// <summary>
            /// This class will be serialized as JSON.
            /// </summary>
            public class PackageInfo {
                
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