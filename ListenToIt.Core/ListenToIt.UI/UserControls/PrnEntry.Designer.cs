
namespace ListenToIt.UI.UserControls {
    partial class PrnEntry {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.lblWord = new System.Windows.Forms.Label();
            this.lblPOS = new System.Windows.Forms.Label();
            this.btnUK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBritish = new System.Windows.Forms.Label();
            this.lblAmerica = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAmerica = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblWord
            // 
            this.lblWord.AutoSize = true;
            this.lblWord.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWord.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(118)))), ((int)(((byte)(209)))));
            this.lblWord.Location = new System.Drawing.Point(12, 9);
            this.lblWord.Name = "lblWord";
            this.lblWord.Size = new System.Drawing.Size(96, 19);
            this.lblWord.TabIndex = 0;
            this.lblWord.Text = "entry_word";
            // 
            // lblPOS
            // 
            this.lblPOS.AutoSize = true;
            this.lblPOS.Location = new System.Drawing.Point(13, 28);
            this.lblPOS.Name = "lblPOS";
            this.lblPOS.Size = new System.Drawing.Size(96, 16);
            this.lblPOS.TabIndex = 1;
            this.lblPOS.Text = "part_of_speech";
            // 
            // btnUK
            // 
            this.btnUK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUK.BackgroundImage = global::ListenToIt.UI.Properties.Resources.icon_sound;
            this.btnUK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnUK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUK.Location = new System.Drawing.Point(20, 52);
            this.btnUK.Name = "btnUK";
            this.btnUK.Size = new System.Drawing.Size(35, 33);
            this.btnUK.TabIndex = 2;
            this.btnUK.UseVisualStyleBackColor = true;
            this.btnUK.Click += new System.EventHandler(this.btnUK_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(61, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "British";
            // 
            // lblBritish
            // 
            this.lblBritish.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBritish.AutoSize = true;
            this.lblBritish.Location = new System.Drawing.Point(61, 68);
            this.lblBritish.Name = "lblBritish";
            this.lblBritish.Size = new System.Drawing.Size(99, 16);
            this.lblBritish.TabIndex = 4;
            this.lblBritish.Text = "british_phonetic";
            // 
            // lblAmerica
            // 
            this.lblAmerica.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAmerica.AutoSize = true;
            this.lblAmerica.Location = new System.Drawing.Point(228, 68);
            this.lblAmerica.Name = "lblAmerica";
            this.lblAmerica.Size = new System.Drawing.Size(110, 16);
            this.lblAmerica.TabIndex = 7;
            this.lblAmerica.Text = "america_phonetic";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(228, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "America";
            // 
            // btnAmerica
            // 
            this.btnAmerica.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAmerica.BackgroundImage = global::ListenToIt.UI.Properties.Resources.icon_sound;
            this.btnAmerica.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAmerica.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAmerica.Location = new System.Drawing.Point(187, 52);
            this.btnAmerica.Name = "btnAmerica";
            this.btnAmerica.Size = new System.Drawing.Size(35, 33);
            this.btnAmerica.TabIndex = 5;
            this.btnAmerica.UseVisualStyleBackColor = true;
            this.btnAmerica.Click += new System.EventHandler(this.btnAmerica_Click);
            // 
            // PrnEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblAmerica);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnAmerica);
            this.Controls.Add(this.lblBritish);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnUK);
            this.Controls.Add(this.lblPOS);
            this.Controls.Add(this.lblWord);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PrnEntry";
            this.Size = new System.Drawing.Size(364, 95);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWord;
        private System.Windows.Forms.Label lblPOS;
        private System.Windows.Forms.Button btnUK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblBritish;
        private System.Windows.Forms.Label lblAmerica;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAmerica;
    }
}
