using System;
using System.IO;
using ListenToIt.Updater.CmdOptions;
using ListenToIt.Updater.Package;
using ICSharpCode.SharpZipLib.Zip;
using Newtonsoft.Json;
using Version = ListenToIt.Updater.Package.Version;

namespace ListenToIt.Updater.Install {
    public class Installer {
        private readonly InstallOptions _options;
        private readonly PackageInfo _packageInfo;
        private string _extractedPath;
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

            _extractedPath = Path.Combine(Path.GetDirectoryName(_packageInfo.PackagePath), _packageInfo.PackageVersion);
            
            return this;
        }

        /// <summary>
        /// Copies the core files of the new package.
        /// </summary>
        public Installer CopyNewVersion() {
            // Look for the core files in the extracted folders
            foreach (var dir in Directory.GetDirectories(_extractedPath)) {
                if (!Version.IsValidRawVersionString(Path.GetFileName(dir))) continue;
                var newDir = Path.Combine(Path.GetFullPath(_options.InstallDir), _packageInfo.PackageVersion);
                Directory.CreateDirectory(newDir);
                CopyDirectory(dir, newDir);
            }
            
            return this;
        }
        
        // Copies a directory recursively
        private static void CopyDirectory(string directory, string destination) {
            destination = Path.GetFullPath(destination);
            try {
                foreach (var dir in Directory.GetDirectories(directory, "*", SearchOption.AllDirectories)) {
                    Directory.CreateDirectory(Path.Combine(destination, dir.Substring(directory.Length + 1)));
                }

                foreach (var fileName in Directory.GetFiles(directory, "*", SearchOption.AllDirectories)) {
                    File.Copy(fileName, Path.Combine(destination, fileName.Substring(directory.Length + 1)));
                }
            }
            catch {
                // ignored
            }
        }
    }
}