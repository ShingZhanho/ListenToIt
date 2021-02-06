using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ListenToIt.Runner.UpdateService;

namespace ListenToIt.Runner {
    public static class Program {
        public static void Main() {
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
            
            // Starts the latest version
            var appProcess = new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = exePath
                }
            };
            appProcess.Start();
            appProcess.WaitForExit();
        }
    }
}