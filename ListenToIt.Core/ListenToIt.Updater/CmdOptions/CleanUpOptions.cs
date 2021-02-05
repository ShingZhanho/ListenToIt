﻿using CommandLine;

namespace ListenToIt.Updater.CmdOptions {
    [Verb("clean", HelpText = "Merge new versions and removes old versions")]
    public class CleanUpOptions {
        [Option('d', "cleanup-dir", HelpText = "The directory path to clean up.", Required = true)]
        public string CleanUpDir { get; }
        
        [Option('s', "suffix", HelpText = "Defines dirs and files with this suffix will be merged", Required = true)]
        public string MergeSuffix { get; }
    }
}