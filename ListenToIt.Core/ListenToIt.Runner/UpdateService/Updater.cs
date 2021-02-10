using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using ListenToIt.Runner.UpdateService.Options;

namespace ListenToIt.Runner.UpdateService {
    public class Updater {
        public static int Run(CheckOptions options) => StartUpdater(options.GetCmdArgs());
        public static int Run(InstallOptions options) => StartUpdater(options.GetCmdArgs());
        public static void Run(CleanUpOptions options) => StartCleanup(options.GetCmdArgs());

        private static int StartUpdater(string args, bool wait = true) {
            var suProcess = new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = Path.Combine(Application.StartupPath, "SU\\ListenToIt.Updater.exe"),
                    Arguments = args,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            suProcess.Start();
            if (wait) suProcess.WaitForExit();
            return suProcess.HasExited ? suProcess.ExitCode : 999;
        }

        private static void StartCleanup(string args) {
            // Creates temporary working directory for cleanup
            Directory.CreateDirectory(Path.Combine(Application.StartupPath, "SU_temp"));
            CopyDir(Path.Combine(Application.StartupPath, "SU"),
                Path.Combine(Application.StartupPath, "SU_temp"));
            var suProcess = new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = Path.Combine(Application.StartupPath, "SU_temp\\ListenToIt.Updater.exe"),
                    Arguments = args,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            suProcess.Start(); 
        }

        private static void CopyDir(string from, string destination) {
            // Recreate the directory structure
            foreach (var dir in Directory.GetDirectories(from, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(Path.Combine(destination, dir.Substring(from.Length + 1)));
            
            // Copies all files
            foreach (var file in Directory.GetFiles(from, "*", SearchOption.AllDirectories))
                File.Copy(file, Path.Combine(destination, file.Substring(from.Length + 1)));
        }
    }
}