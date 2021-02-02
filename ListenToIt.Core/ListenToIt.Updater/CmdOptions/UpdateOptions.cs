using System.Collections.Generic;
using CommandLine;

namespace ListenToIt.Updater.CmdOptions {
    /// <summary>
    /// Options for checking update. This class is immutable.
    /// </summary>
    [Verb("check", true, HelpText = "Checks for update from the server and downloads if available.")]
    public class UpdateOptions {
        /// <summary>
        /// CurrentVersion specifies the current version of ListenToIt.
        /// </summary>
        [Option('v', "current-version", HelpText = "The current version of ListenToIt.", Required = true)]
        public string CurrentVersion { get; }
        
        /// <summary>
        /// DownloadDir specifies the directory for saving update package.
        /// </summary>
        [Option('d', "download-dir", HelpText = "The directory for storing downloaded update package.",
            Required = true)]
        public string DownloadDir { get; }

        [Option('c', "check", HelpText = "Check for update only, not downloading even available." +
                                         "If an update is available, the program will exit with code 200.")] 
        public bool CheckOnly { get; }
        
        [Option('u', "user-cred", Default = null, Min = 2, Max = 2, 
            HelpText = "GitHub Account for checking update.", Separator = ';')]
        public IEnumerable<string> UserCredentials { get; } 
        
        [Option('p', "prerelease", HelpText = "Include prerelease versions.")]
        public bool IncludePrerelease { get; }

        public UpdateOptions(string currentVersion,
            string downloadDir,
            bool checkOnly,
            IEnumerable<string> userCredentials,
            bool includePrerelease) {
            CurrentVersion = currentVersion;
            DownloadDir = downloadDir;
            CheckOnly = checkOnly;
            UserCredentials = userCredentials;
            IncludePrerelease = includePrerelease;
        }
    }
}