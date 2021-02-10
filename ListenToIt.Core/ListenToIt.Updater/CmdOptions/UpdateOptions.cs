using System.Collections.Generic;
using System.IO;
using CommandLine;

namespace ListenToIt.Updater.CmdOptions {
    [Verb("check", HelpText = "Checks for update from the server and downloads if available.")]
    public class UpdateOptions {
        private string _downloadDir;
        private string _currentVersion;
        private bool _checkOnly;
        private IEnumerable<string> _userCredentials;
        private bool _includePrerelease;

        [Option('v', "current-version", HelpText = "The current version of ListenToIt.", Required = true)]
        public string CurrentVersion {
            get => _currentVersion;
            set => _currentVersion = value;
        }

        [Option('d', "download-dir", HelpText = "The directory for storing downloaded update package.",
            Required = true)]
        public string DownloadDir {
            get => _downloadDir;
            set => _downloadDir = Path.GetFullPath(value);
        }

        [Option('c', "check",
            HelpText = "Check for update only, not downloading even available." +
                       "If an update is available, the program will exit with code 200, otherwise 201.")]
        public bool CheckOnly {
            get => _checkOnly;
            set => _checkOnly = value;
        }

        [Option('u', "user-cred", Default = null, Min = 2, Max = 2,
            HelpText = "GitHub Account for checking update.", Separator = ';')]
        public IEnumerable<string> UserCredentials {
            get => _userCredentials;
            set => _userCredentials = value;
        }

        [Option('p', "prerelease", HelpText = "Include prerelease versions.")]
        public bool IncludePrerelease {
            get => _includePrerelease;
            set => _includePrerelease = value;
        }

        public UpdateOptions(string currentVersion,
            string downloadDir,
            bool checkOnly,
            IEnumerable<string> userCredentials,
            bool includePrerelease) {
            CurrentVersion = currentVersion;
            DownloadDir = Path.GetFullPath(downloadDir);
            CheckOnly = checkOnly;
            UserCredentials = userCredentials;
            IncludePrerelease = includePrerelease;
        }

        public UpdateOptions() { }
    }
}