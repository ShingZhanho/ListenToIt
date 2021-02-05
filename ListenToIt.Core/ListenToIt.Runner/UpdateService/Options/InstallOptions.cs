using System.IO;

namespace ListenToIt.Runner.UpdateService.Options {
    public sealed class InstallOptions : BaseOptions {
        public string InstallDir { get; set; }
        public string PackagePath { get; set; }
        public bool RemoveAfterInstall { get; set; }
        public override string GetCmdArgs() {
            var args = string.Empty;

            args = $"{args} -d {Path.GetFullPath(InstallDir)}"; // set -d argument
            args = $"{args} -p {Path.GetFullPath(PackagePath)}"; // set -p argument
            args = RemoveAfterInstall ? $"{args} -r" : args; // set -r flag
            
            return args;
        }
    }
}