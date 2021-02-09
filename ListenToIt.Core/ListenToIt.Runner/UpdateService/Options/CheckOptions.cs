using System.IO;
using ListenToIt.Runner.UpdateService;

namespace ListenToIt.Runner.UpdateService.Options {
    public class CheckOptions : BaseOptions {
        public Version CurrentVersion { get; set; }
        public string DownloadDir { get; set; } = string.Empty;
        public bool CheckOnly { get; set; }
        public string[] UserCredentials { get; set; } = new string[2];
        public bool IncludePrerelease { get; set; }

        /// <summary>Gets the command line arguments of current settings.</summary>
        public override string GetCmdArgs() {
            var args = "check"; // set verb

            args = $"{args} -v {CurrentVersion.GetVersionString()}"; // set -v argument
            args = $"{args} -d {Path.GetFullPath(DownloadDir)}"; // set -d argument
            args = CheckOnly ? $"{args} -c" : args; // set -c flag
            if (UserCredentials.Length == 2 && !(UserCredentials[0] is null) && !(UserCredentials[1] is null))
                args = $"{args} -u {UserCredentials[0]} {UserCredentials[1]}"; // set -u argument
            args = IncludePrerelease ? $"{args} -p" : args; // set -p flag

            return args;
        }
    }
}