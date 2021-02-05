using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ListenToIt.Updater.CmdOptions;

namespace ListenToIt.Updater.Install {
    public class CleanUpHelper {
        public CleanUpHelper(CleanUpOptions options) {
            _options = options;
        }

        private readonly CleanUpOptions _options;

        /// <summary>Merge</summary>
        public void Merge() {
            var newDirs = Directory.GetDirectories(_options.CleanUpDir)
                .Where(directory => PathIsWithSuffix(directory,
                    "new",
                    false))
                .ToList();

            foreach (var newDir in newDirs) {
                var pathWithoutSuffix = 
                    Path.Combine(_options.CleanUpDir, GetNameWithoutSuffix(newDir, "new", false));
                if (Directory.Exists(pathWithoutSuffix))
                    Directory.Delete(pathWithoutSuffix, true); // Deletes old dirs
                Directory.Move(newDir, pathWithoutSuffix);
            }

            var newFiles = Directory.GetFiles(_options.CleanUpDir)
                .Where(file => PathIsWithSuffix(file, "new", true)).ToList();

            foreach (var newFile in newFiles) {
                var pathWithoutSuffix = 
                    Path.Combine(_options.CleanUpDir, GetNameWithoutSuffix(newFile, "new", true));
                if (File.Exists(pathWithoutSuffix))
                    File.Delete(pathWithoutSuffix); // Delete old files
                File.Move(newFile, pathWithoutSuffix);
            }
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