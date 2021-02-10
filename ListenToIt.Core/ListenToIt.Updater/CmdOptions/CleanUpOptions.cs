using System.IO;
using CommandLine;

namespace ListenToIt.Updater.CmdOptions {
    [Verb("clean", HelpText = "Merge new versions and removes old versions")]
    public class CleanUpOptions {
        private string _cleanUpDir;
        private string _mergeSuffix;

        public CleanUpOptions(string cleanUpDir, string mergeSuffix) {
            _cleanUpDir = Path.GetFullPath(cleanUpDir);
            _mergeSuffix = mergeSuffix;
        }
        
        public CleanUpOptions() { }

        [Option('d', "cleanup-dir", HelpText = "The directory path to clean up.", Required = true)]
        public string CleanUpDir {
            get => _cleanUpDir;
            set => _cleanUpDir = Path.GetFullPath(_cleanUpDir);
        }

        [Option('s', "suffix", HelpText = "Defines dirs and files with this suffix will be merged", Required = true)]
        public string MergeSuffix => _mergeSuffix;
    }
}