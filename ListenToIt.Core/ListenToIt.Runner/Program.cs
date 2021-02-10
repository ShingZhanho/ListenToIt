using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ListenToIt.Runner.UpdateService;
using ListenToIt.Runner.UpdateService.Options;
using Version = ListenToIt.Runner.UpdateService.Version;

namespace ListenToIt.Runner {
    public static class Program {
        private static Version _localLatestVersion, _serverLatestVersion;
        private static bool _updateAvailable, _updateDownloaded;

        public static void Main() {
            _localLatestVersion = GetLatest();
            if (_localLatestVersion is null) Environment.Exit(1); // exits if no versions installed.

            // Check update
            var updateThread = new Thread(CheckAndDownload);
            updateThread.Start();
            
            // Run app
            var exePath = 
                Path.Combine(Application.StartupPath, _localLatestVersion.GetVersionString(), "ListenToIt.App.exe");
            var appProc = new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = exePath,
                    UseShellExecute = false,
                    CreateNoWindow = false
                }
            };
            appProc.Start();
            appProc.WaitForExit();
        }

        private static Version GetLatest() {
            var versions = Directory.GetDirectories(Application.StartupPath)
                .Where(dir => Version.IsValidRawVersionString(Path.GetFileName(dir)))
                .Where(dir => File.Exists(Path.Combine(dir, "ListenToIt.App.exe"))).ToList();

            var latest = new Version(Path.GetFileName(versions[0]));
            for (var i = 1; i < versions.Count; i++) {
                var ver = new Version[] {
                    new Version(Path.GetFileName(versions[i - 1])),
                    new Version(Path.GetFileName(versions[i]))
                };
                latest = ver[1].IsNewerThan(ver[0]) ? ver[1] : latest;
            }

            return latest;
        }

        private static void CheckAndDownload() {
            // call this method in a new thread
            // Checks for internet connection
            try {
                using (var client = new WebClient())
                    client.OpenRead(new Uri("https://github.com"));
            } catch {
                return;
            }

            Directory.CreateDirectory(Path.Combine(Application.StartupPath, "./SUCache")); // Creates dir for update cache

            var options = new CheckOptions {
                CheckOnly = true,
                CurrentVersion = _localLatestVersion,
                DownloadDir = Path.Combine(Application.StartupPath, "SUCache"),
                IncludePrerelease = true
            };
            
            // Checks for update
            _updateAvailable = Updater.Run(options) == 200;
            if (!_updateAvailable) return;
            
            // Download
            options.CheckOnly = false;
            _updateDownloaded = Updater.Run(options) == 0;
            
            // Gets the latest downloaded version
            var downloadedPkgInfo = Directory.GetFiles(Path.Combine(Application.StartupPath, "SUCache"))
                .Where(file => Path.GetExtension(file) == ".json")
                .Where(file =>
                    Version.IsValidRawVersionString(Path.GetFileNameWithoutExtension(file)
                        .Replace("package_info_", string.Empty)))
                .ToList();
            _serverLatestVersion = new Version(Path.GetFileNameWithoutExtension(downloadedPkgInfo[0])
                .Replace("package_info_", string.Empty));
            for (var i = 1; i < downloadedPkgInfo.Count; i++) {
                var ver = new Version[] {
                    new Version(Path.GetFileNameWithoutExtension(downloadedPkgInfo[i - 1])
                        .Replace("package_info_", "")),
                    new Version(Path.GetFileNameWithoutExtension(downloadedPkgInfo[i])
                        .Replace("package_info_", ""))
                };
                _serverLatestVersion = ver[1].IsNewerThan(ver[0]) ? ver[1] : _serverLatestVersion;
            }

            if (!_updateDownloaded) return; // Exits if not downloaded
            // Install update
            var installOpts = new InstallOptions {
                InstallDir = Application.StartupPath,
                PackagePath = Path.Combine(Application.StartupPath, "SUCache",
                    $"package_info_{_serverLatestVersion.GetVersionString()}.json"),
                RemoveAfterInstall = false
            };
            Updater.Run(installOpts);
        }
    }
}