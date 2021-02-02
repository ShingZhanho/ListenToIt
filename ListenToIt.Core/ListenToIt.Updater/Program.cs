using System;
using System.Collections.Generic;
using CommandLine;
using ListenToIt.Updater.CmdOptions;

namespace ListenToIt.Updater {
    public static partial class Program {
        static void Main(string[] args) {
            var arguments = args;
            // Uncomment the following lines for debugging
            //arguments = "-d C:\\ -v 0.0.0.1-beta -p".Split(' ');
            
            // Parses command line options
            Parser.Default.ParseArguments<UpdateOptions>(arguments)
                .WithParsed(CheckUpdate)
                .WithNotParsed(ErrorParsingArgs);
        }

        private static void CheckUpdate(UpdateOptions options) {
            Console.WriteLine("\"check\" command received");
            var update = new UpdateDownloader(options);
            
            // Exit if check only is true
            if (options.CheckOnly)
                Environment.Exit(update.IsUpToDate() ? 201 : 200);
            
            // Download files
            update.DownloadPackage(new Uri(update.LatestRelease.Assets[0].BrowserDownloadUrl));
        }

        private static void ErrorParsingArgs(IEnumerable<Error> errors) {
            // ignored
        }
    }
}
