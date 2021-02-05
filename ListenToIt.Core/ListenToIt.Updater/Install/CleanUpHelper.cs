using System;
using System.IO;
using ListenToIt.Updater.CmdOptions;

namespace ListenToIt.Updater.Install {
    public class CleanUpHelper {
        public CleanUpHelper(CleanUpOptions options) {
            _options = options;
        }

        private readonly CleanUpOptions _options;

        /// <summary>Merge</summary>
        public void Merge() {
            
        }

        private static bool PathIsWithSuffix(string path, string suffix, bool isFile) {
            var name = isFile 
                ? Path.GetFileNameWithoutExtension(path) 
                : Path.GetFileName(path);
            return name.EndsWith($"_{suffix}");
        }

        private static string GetNameWithoutSuffix(string path, string suffix, bool isFile) {
            if (!PathIsWithSuffix(path, suffix, isFile))
                throw new ArgumentException("The given file or dir does not have the specified suffix.");
            if (isFile) {
                var fileName = Path.GetFileNameWithoutExtension(path);
                return $"{fileName.Substring(0, fileName.Length - suffix.Length - 1)}{Path.GetExtension(path)}";
            }

            var dirName = Path.GetFileName(path);
            return $"{dirName.Substring(0, dirName.Length - suffix.Length - 1)}";
        }
    }
}