using Newtonsoft.Json;

namespace ListenToIt.Updater.Package {
    /// <summary>
    /// This class will be serialized as JSON.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class PackageInfo {
        public string PackagePath;
        public string PackageVersion;
    }
}