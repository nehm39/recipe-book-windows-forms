namespace KsiazkaKucharska
{
    partial class EdytujSkladnikPrzepisu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbSkladniki = new System.Windows.Forms.ComboBox();
            this.cbJednostki = new System.Windows.Forms.ComboBox();
            this.txbIlosc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnZapisz = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbSkladniki
            // 
            this.cbSkladniki.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSkladniki.FormattingEnabled = true;
            this.cbSkladniki.Location = new System.Drawing.Point(16, 29);
            this.cbSkladniki.Name = "cbSkladniki";
            this.cbSkladniki.Size = new System.Drawing.Size(203, 21);
            this.cbSkladniki.TabIndex = 0;
            // 
            // cbJednostki
            // 
            this.cbJednostki.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbJednostki.FormattingEnabled = true;
            this.cbJednostki.Location = new System.Drawing.Point(16, 116);
            this.cbJednostki.Name = "cbJednostki";
            this.cbJednostki.Size = new System.Drawing.Size(203, 21);
            this.cbJednostki.TabIndex = 1;
            // 
            // txbIlosc
            // 
            this.txbIlosc.Location = new System.Drawing.Point(16, 73);
            this.txbIlosc.Name = "txbIlosc";
            this.txbIlosc.Size = new System.Drawing.Size(203, 20);
            this.txbIlosc.TabIndex = 2;
            this.txbIlosc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbIlosc_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Składnik:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ilość:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Jednostka";
            // 
            // btnZapisz
            // 
            this.btnZapisz.Location = new System.Drawing.Point(16, 155);
            this.btnZapisz.Name = "btnZapisz";
            this.btnZapisz.Size = new System.Drawing.Size(203, 23);
            this.btnZapisz.TabIndex = 6;
            this.btnZapisz.Text = "Zapisz";
            this.btnZapisz.UseVisualStyleBackColor = true;
            this.btnZapisz.Click += new System.EventHandler(this.btnZapisz_Click);
            // 
            // EdytujSkladnikPrzepisu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 190);
            this.Controls.Add(this.btnZapisz);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txbIlosc);
            this.Controls.Add(this.cbJednostki);
            this.Controls.Add(this.cbSkladniki);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EdytujSkladnikPrzepisu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edytuj";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbSkladniki;
        private System.Windows.Forms.ComboBox cbJednostki;
        private System.Windows.Forms.TextBox txbIlosc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnZapisz;
    }
}