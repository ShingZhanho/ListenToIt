using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using ListenToIt.Runner.UpdateService.Options;

namespace ListenToIt.Runner.UpdateService {
    public class Updater {
        public static int Run(CheckOptions options) => StartUpdater(options.GetCmdArgs());

        private static int StartUpdater(string args) {
            var suProcess = new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = Path.Combine(Application.StartupPath, "SU\\ListenToIt.Updater.exe"),
                    Arguments = args,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            suProcess.Start();
            suProcess.WaitForExit();
            return suProcess.ExitCode;
        }
    }
}