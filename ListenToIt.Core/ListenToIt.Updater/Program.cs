using System;
using System.Collections.Generic;
using CommandLine;
using ListenToIt.Updater.CmdOptions;
using ListenToIt.Updater.Install;

namespace ListenToIt.Updater {
    public static partial class Program {
        static void Main(string[] args) {
            var arguments = args;
            // Uncomment the following lines for debugging
            // arguments = "install -d ./Cache/Install -p ./Cache/package_info_0.0.1.0-beta.json".Split(' ');
            
            // Parses command line options
            Parser.Default.ParseArguments<UpdateOptions, InstallOptions>(arguments)
                .WithParsed<UpdateOptions>(CheckUpdate)
                .WithParsed<InstallOptions>(InstallUpdate)
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

        private static void ErrorParsingArgs(IEnumerable<Error> errors) {
            // ignored
        }
    }
}
