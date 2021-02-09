using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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

        public static void Main() {
            
        }
    }
}