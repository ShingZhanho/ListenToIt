using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using ListenToIt.Runner.UpdateService;

namespace ListenToIt.Runner {
    public static class Program {
        public static void Main() {
            // Look for the latest version, and start the app
            var exePath = string.Empty;
            foreach (var directory in Directory.GetDirectories(Application.StartupPath)) {
                if (!Version.IsValidRawVersionString(directory)) continue;
                exePath = Path.Combine(directory, "ListenToIt.App.exe");
            }
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