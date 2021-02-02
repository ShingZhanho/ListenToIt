using System;
using System.Linq;
using ListenToIt.Updater.CmdOptions;
using Octokit;

namespace ListenToIt.Updater {
    public partial class Program {
        private class UpdateDownloader {
            private UpdateOptions _options; // Options fetched from command line
            
            // Information fetched from server
            private Release _latestRelease;

            public UpdateDownloader(UpdateOptions options) {
                _options = options;
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
                }
            }

            public bool IsUpToDate() {
                return true;
            }
        }
    }
}