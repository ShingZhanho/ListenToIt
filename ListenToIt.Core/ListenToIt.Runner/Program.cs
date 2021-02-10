﻿using System;
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
        // The below code is under rebuild
        /*public static void Main() {
            // Check for internet connection
            var networkIsConnected = false;
            try {
                using (var client = new WebClient())
                using (var stream = client.OpenRead(new Uri("https://github.com")))
                    networkIsConnected = true;
            }
            catch {
                networkIsConnected = false;
            }
            
            // Look for all available versions
            var availableVersions = Directory.GetDirectories(Application.StartupPath)
                .Where(directory => 
                    Version.IsValidRawVersionString(Path.GetFileName(directory)))
                .Where(directory => 
                    File.Exists(Path.Combine(directory, "ListenToIt.App.exe")))
                .ToList();
            
            // Finds the latest version
            var exePath = string.Empty;
            for (var i = 1; i < availableVersions.Count; i++) {
                var versions = new List<Version> {
                    new Version(Path.GetFileName(availableVersions[i - 1])),
                    new Version(Path.GetFileName(availableVersions[i]))
                };
                exePath = versions[1].IsNewerThan(versions[0])
                    ? Path.Combine(availableVersions[i], "ListenToIt.App.exe")
                    : exePath;
            }
            
            // Check update
            var checkOptions = new CheckOptions { 
                CheckOnly = true,
                CurrentVersion = new Version(Path.GetDirectoryName(exePath)),
                DownloadDir = "./SUCache",
                IncludePrerelease = true};
            Directory.CreateDirectory("./SUCache");
            Task<int> checkTask = null;
            if (networkIsConnected) checkTask = Updater.CheckUpdateAsync(checkOptions);
            
            // Starts the latest version
            var appProcess = new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = exePath
                }
            };
            appProcess.Start();
            
            // Get update results, download if available
            if (networkIsConnected) {
                if (checkTask.Result == 200) {
                    checkOptions.CheckOnly = false;
                    
                }
            }
            
            appProcess.WaitForExit();
        }*/

        // The latest installed version on this computer
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
                latest = ver[1].IsNewerThan(ver[0]) ? ver[1] : ver[0];
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
                        .Replace("package_info_", "")))
                .ToList();
            _serverLatestVersion = new Version(downloadedPkgInfo[0]);
            for (var i = 1; i < downloadedPkgInfo.Count; i++) {
                var ver = new Version[] {
                    new Version(Path.GetFileNameWithoutExtension(downloadedPkgInfo[i - 1])
                        .Replace("package_info_", "")),
                    new Version(Path.GetFileNameWithoutExtension(downloadedPkgInfo[i])
                        .Replace("package_info_", ""))
                };
                _serverLatestVersion = ver[1].IsNewerThan(ver[0]) ? ver[1] : ver[0];
            }
        }
    }
}