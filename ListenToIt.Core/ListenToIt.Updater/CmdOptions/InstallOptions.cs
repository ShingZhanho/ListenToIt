using System.IO;
using CommandLine;

namespace ListenToIt.Updater.CmdOptions {
    [Verb("install", HelpText = "Installs update package to the specified directory.")]
    public class InstallOptions {
        private string _packagePath;
        private string _installDir;
        private bool _removeAfterInstall;

        [Option('p', "package-path", HelpText = "The path of the package Json file.", Required = true)]
        // Required parameters
        public string PackagePath {
            get => _packagePath;
            set => _packagePath = Path.GetFullPath(value);
        }

        [Option('d', "install-dir",
            HelpText = "The directory where the package will be installed. " +
                       "This should be the directory where ListenToIt.Runner.exe is placed", Required = true)]
        public string InstallDir {
            get => _installDir;
            set => _installDir = Path.GetFullPath(value);
        }

        [Option('r', "remove", HelpText = "Remove downloaded package after install.")]
        public bool RemoveAfterInstall => _removeAfterInstall;

        public InstallOptions(string packagePath, string installDir, bool removeAfterInstall) {
            _packagePath = Path.GetFullPath(packagePath);
            _installDir = Path.GetFullPath(installDir);
            _removeAfterInstall = removeAfterInstall;
        }

        public InstallOptions() { }
    }
}