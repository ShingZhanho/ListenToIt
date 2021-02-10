using System;
using System.IO;
using System.Linq;
using System.Net;
using ListenToIt.Updater.CmdOptions;
using ListenToIt.Updater.Package;
using Newtonsoft.Json;
using Octokit;

namespace ListenToIt.Updater.Install {
    public class UpdateDownloader {
        private readonly UpdateOptions _options; // Options fetched from command line

        // Information fetched from server
        public Release LatestRelease;
        public Package.Version LatestVersion;
        public readonly Package.Version CurrentVersion;

        public UpdateDownloader(UpdateOptions options) {
            _options = options;
            CurrentVersion = new Package.Version(options.CurrentVersion);
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
                if (!_options.IncludePrerelease)
                    if (release.Prerelease)
                        continue; // Skip for beta versions
                LatestRelease = release;
                LatestVersion = new Package.Version(release.TagName);
                break;
            }
        }

        public void DownloadPackage(Uri url) {
            var webClient = new WebClient();
            webClient.DownloadFile(url, Path.Combine(_options.DownloadDir, Path.GetFileName(url.LocalPath)));
        }

        public bool IsUpToDate() {
            return !LatestVersion.IsNewerThan(CurrentVersion);
        }

        public void WritePackageJson() {
            var pkgInfo = new PackageInfo {
                PackagePath = Path.GetFullPath(Path.Combine(_options.DownloadDir,
                    Path.GetFileName(LatestRelease.Assets[0].BrowserDownloadUrl))),
                PackageVersion = LatestVersion.GetVersionString()
            };
            var json = JsonConvert.SerializeObject(pkgInfo);
            try {
                using var writer = new StreamWriter(Path.Combine(_options.DownloadDir,
                    $"package_info_{LatestVersion.GetVersionString()}.json"));
                writer.Write(json);
            }
            catch {
                Environment.Exit(1); // 1 indicates failure
            }
        }
    }
}