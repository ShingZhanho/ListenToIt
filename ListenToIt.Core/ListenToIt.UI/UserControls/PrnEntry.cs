using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio;
using NAudio.Wave;

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

            _ukAudioFile = britishFile;
            _usAudioFile = americaFile;
        }

        private string _ukAudioFile, _usAudioFile;

        private void btnAmerica_Click(object sender, EventArgs e) {
            try {
                var reader = new Mp3FileReader(_usAudioFile);
                var waveOut = new WaveOut();
                waveOut.Init(reader);
                waveOut.Play();
            } catch {
                // Ignored
            }
        }

        private void btnUK_Click(object sender, EventArgs e) {
            try {
                var reader = new Mp3FileReader(_ukAudioFile);
                var waveOut = new WaveOut();
                waveOut.Init(reader);
                waveOut.Play();
            } catch {
                // Ignored
            }
        }
    }
}
