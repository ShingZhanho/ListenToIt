using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListenToIt.UI {
    public partial class DialogDownload : Form {
        private DialogDownload() {
            InitializeComponent();
        }

        public DialogDownload(string word) : this() {

        }

        public SearchResults Results { get; } = new SearchResults();

        /// <summary>
        /// Class for search results
        /// </summary>
        public class SearchResults {
            public bool IsSuccessful = false;
        }
    }
}
