using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CommandLine;
using ListenToIt.Updater.CmdOptions;
using ListenToIt.Updater.Install;

namespace ListenToIt.Updater {
    public static class Program {
        public static void Main(string[] args) {
            var arguments = args;
            // Uncomment the following lines for debugging
            // arguments = "check --current-version 0.0.0.1-beta --download-dir ./Cache --check -p".Split(' ');
            
            // Parses command line options
            Parser.Default.ParseArguments<UpdateOptions, InstallOptions, CleanUpOptions>(arguments)
                .WithParsed<UpdateOptions>(CheckUpdate)
                .WithParsed<InstallOptions>(InstallUpdate)
                .WithParsed<CleanUpOptions>(CleanUp)
                .WithNotParsed(ErrorParsingArgs);
        }

        private static void CheckUpdate(UpdateOptions options) {
            var update = new UpdateDownloader(options);
            
            // Exit if check only is true
            if (options.CheckOnly)
                Environment.Exit(update.IsUpToDate() ? 201 : 200);

            if (update.IsUpToDate()) Environment.Exit(5); // Download only if new version is available. 5 means no updates
            // Download files
            update.DownloadPackage(new Uri(update.LatestRelease.Assets[0].BrowserDownloadUrl));

            // Writes Json package info
            update.WritePackageJson();
        }

        private static void InstallUpdate(InstallOptions options) {
            var installer = new Installer(options);
            installer
                .ExtractPackage()
                .CopyNewVersion();
            if (options.RemoveAfterInstall) installer.RemovePackages();
        }

        private static void CleanUp(CleanUpOptions options) {
            // Kills the existing process
            var procList = Process.GetProcessesByName("ListenToIt.Runner");
            foreach (var proc in procList) proc.Kill();
            
            var cleanupHelper = new CleanUpHelper(options);
            cleanupHelper.Merge();
        }

        private static void ErrorParsingArgs(IEnumerable<Error> errors) {
            foreach (var error in errors.ToList()) {
                Console.WriteLine(error.Tag.ToString());
            }
            Environment.Exit(5);
        }
    }
}
