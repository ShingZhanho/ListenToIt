using System;
using System.Collections.Generic;
using CommandLine;
using ListenToIt.Updater.CmdOptions;

namespace ListenToIt.Updater {
    class Program {
        static void Main(string[] args) {
            // Parses command line options
            Parser.Default.ParseArguments<UpdateOptions>(args)
                .WithParsed(CheckUpdate)
                .WithNotParsed(ErrorParsingArgs);
        }

        private static void CheckUpdate(UpdateOptions options) {
            Console.WriteLine("\"check\" command received");
        }

        private static void ErrorParsingArgs(IEnumerable<Error> errors) {
            // ignored
        }
    }
}
