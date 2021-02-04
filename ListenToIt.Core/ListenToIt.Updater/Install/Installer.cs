using System;
using System.IO;
using ListenToIt.Updater.CmdOptions;
using ListenToIt.Updater.Package;
using ICSharpCode.SharpZipLib.Zip;
using Newtonsoft.Json;

namespace ListenToIt.Updater.Install {
    public class Installer {
        private readonly InstallOptions _options;
        private readonly PackageInfo _packageInfo;
        public Installer(InstallOptions options) {
            _options = options;
            
            // Loads package info
            using var fr = new StreamReader(_options.PackagePath);
            _packageInfo = JsonConvert.DeserializeObject<PackageInfo>(fr.ReadToEnd());
        }

        /// <summary>
        /// Extracts the package specified in command line options.
        /// </summary>
        public Installer ExtractPackage() {
            if (!File.Exists(_options.PackagePath)) return null;
            
            // Extract package
            try {
                var fastZip = new FastZip();
                fastZip.ExtractZip(_packageInfo.PackagePath,
                    Path.Combine(Path.GetDirectoryName(_packageInfo.PackagePath), _packageInfo.PackageVersion),
                    null);
            }
            catch (Exception e) {
                Console.WriteLine(e);
                Environment.Exit(1);
            }
            
            return this;
        }

        /// <summary>
        /// Copies the core files of the new package.
        /// </summary>
        public Installer CopyNewVersion() {
            
            
            return this;
        }
    }
}