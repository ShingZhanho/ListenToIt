using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ListenToIt.UI.SearchUtil.Cache {
    /// <summary>
    /// A class for managing cached audio files.
    /// </summary>
    internal class CacheManager {
        /// <summary>
        /// Creates a CacheManager instance without cache list, cache list will be created.
        /// </summary>
        public CacheManager() {
            
        }
        
        /// <summary>
        /// Creates a CacheManager instance with an existing cache list.
        /// </summary>
        /// <param name="cacheList">Path of the cache list file.</param>
        public CacheManager(string cacheList) {
            
        }

        private List<CacheFile> _cacheFiles;
    }
}
