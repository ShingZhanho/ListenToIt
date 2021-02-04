using CommandLine;

namespace ListenToIt.Updater.CmdOptions {
    [Verb("install", HelpText = "Installs update package to the specified directory.")]
    public class InstallOptions {

        [Option('p', "package-path", HelpText = "The path of the package Json file.", Required = true)]
        // Required parameters
        public string PackagePath { get; }

        [Option('d', "install-dir", 
            HelpText = "The directory where the package will be installed. " +
                       "This should be the directory where ListenToIt.Runner.exe is placed", Required = true)]
        public string InstallDir { get; }
        
        public InstallOptions(string packagePath, string installDir) {
            PackagePath = packagePath;
            InstallDir = installDir;
        }
    }
}