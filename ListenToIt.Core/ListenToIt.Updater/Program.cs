using System;
using System.Collections.Generic;
using CommandLine;
using ListenToIt.Updater.CmdOptions;

namespace ListenToIt.Updater {
    public static partial class Program {
        static void Main(string[] args) {
            var arguments = args;
            // Uncomment the following lines for debugging
            // arguments = "DEBUG PARAMETERS".Split(' ');
            
            // Parses command line options
            Parser.Default.ParseArguments<UpdateOptions>(arguments)
                .WithParsed(CheckUpdate)
                .WithNotParsed(ErrorParsingArgs);
        }

        private static void CheckUpdate(UpdateOptions options) {
            Console.WriteLine("\"check\" command received");
            var update = new UpdateDownloader(options);
            if (options.CheckOnly)
                Environment.Exit(update.IsUpToDate() ? 0 : 200);
        }

        private static void ErrorParsingArgs(IEnumerable<Error> errors) {
            // ignored
        }
    }
}
