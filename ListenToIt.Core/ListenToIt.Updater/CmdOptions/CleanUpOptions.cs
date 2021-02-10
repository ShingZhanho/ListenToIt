using System.IO;
using CommandLine;

namespace ListenToIt.Updater.CmdOptions {
    [Verb("clean", HelpText = "Merge new versions and removes old versions")]
    public class CleanUpOptions {
        private string _cleanUpDir;
        private string _mergeSuffix;

        public CleanUpOptions(string cleanUpDir, string mergeSuffix) {
            CleanUpDir = Path.GetFullPath(cleanUpDir);
            MergeSuffix = mergeSuffix;
        }
        
        public CleanUpOptions() { }

        [Option('d', "cleanup-dir", HelpText = "The directory path to clean up.", Required = true)]
        public string CleanUpDir {
            get => _cleanUpDir;
            set => _cleanUpDir = Path.GetFullPath(value);
        }

        [Option('s', "suffix", HelpText = "Defines dirs and files with this suffix will be merged", Required = true)]
        public string MergeSuffix {
            get => _mergeSuffix;
            set => _mergeSuffix = value;
        }
    }
}