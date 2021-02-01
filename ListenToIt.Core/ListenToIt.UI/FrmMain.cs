using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ListenToIt.UI.UserControls;

namespace ListenToIt.UI {
    public partial class FrmMain : Form {
        public FrmMain() {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e) {
            
        }

        private void btnSearch_Click(object sender, EventArgs e) {
            // Search and download
            var searchDialog = new DialogDownload(txtSearch.Text.Replace(" ", "-"));
            searchDialog.ShowDialog();
        }
    }
}
