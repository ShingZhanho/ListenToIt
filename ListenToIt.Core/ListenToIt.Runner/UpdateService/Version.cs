using System;

namespace ListenToIt.Runner.UpdateService {
    public class Version {
        public Version(int major, int minor, int patch, int revision, VersionSuffix suffix) {
            Major = major;
            Minor = minor;
            Patch = patch;
            Revision = revision;
            Suffix = suffix;
        }

        /// <summary>
        /// Construct a Version instance.
        /// </summary>
        /// <param name="rawVersion">rawVersion should follow "Major.Minor.Patch.Revision-Suffix" format.</param>
        public Version(string rawVersion) {
            Major = Convert.ToInt32(rawVersion.Split('-')[0].Split('.')[0]);
            Minor = Convert.ToInt32(rawVersion.Split('-')[0].Split('.')[1]);
            Patch = Convert.ToInt32(rawVersion.Split('-')[0].Split('.')[2]);
            Revision = Convert.ToInt32(rawVersion.Split('-')[0].Split('.')[3]);
            Suffix = (VersionSuffix) Enum.Parse(typeof(VersionSuffix), rawVersion.Split('-')[1], true);
        }
            
        public int Major { get; }
        public int Minor { get; }
        public int Patch { get; }
        public int Revision { get; }
        public VersionSuffix Suffix { get; }

        public bool IsNewerThan(Version ver) {
            if (Major > ver.Major) return true;
            if (Minor > ver.Minor) return true;
            if (Patch > ver.Patch) return true;
            return Revision > ver.Revision;
        }

        public static bool IsValidRawVersionString(string rawString) {
            try {
                var _ = new Version(rawString);
            }
            catch {
                return false;
            }

            return true;
        }

        public string GetVersionString() => $"{Major}.{Minor}.{Patch}.{Revision}-{Suffix.ToString().ToLower()}";

        public enum VersionSuffix { Stable, Beta }
    }
}