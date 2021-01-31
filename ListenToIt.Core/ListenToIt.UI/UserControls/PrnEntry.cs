using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListenToIt.UI.UserControls {
    public partial class PrnEntry : UserControl {
        private PrnEntry() {
            InitializeComponent();
        }

        public PrnEntry(string word, string pos, string british, string britishFile, string america,
            string americaFile) : this() {
            lblWord.Text = word;
            lblPOS.Text = pos;
            lblBritish.Text = british;
            lblAmerica.Text = america;
        }
    }
}
