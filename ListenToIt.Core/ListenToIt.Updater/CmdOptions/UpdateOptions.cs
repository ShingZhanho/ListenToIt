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
        [Option("current-version", HelpText = "The current version of ListenToIt.", Required = true)]
        public string CurrentVersion { get; }

        public UpdateOptions(string currentVersion) {
            CurrentVersion = currentVersion;
        }
    }
}