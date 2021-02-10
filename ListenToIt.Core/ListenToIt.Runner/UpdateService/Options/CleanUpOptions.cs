using System.IO;

namespace ListenToIt.Runner.UpdateService.Options {
    public class CleanUpOptions : BaseOptions {
        public string CleanUpDir { get; set; }
        public string MergeSuffix { get; set; }
        
        public override string GetCmdArgs() {
            var args = "clean"; // set verb

            args = $"{args} -d {Path.GetFullPath(CleanUpDir)}"; // set -d argument
            args = $"{args} -s {MergeSuffix}"; // set -s argument
            
            return args;
        }
    }
}