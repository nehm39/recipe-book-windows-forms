namespace KsiazkaKucharska
{
    partial class DodajSkladnik
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
            this.label1 = new System.Windows.Forms.Label();
            this.txbNazwa = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbKategorie = new System.Windows.Forms.ComboBox();
            this.txbOpis = new System.Windows.Forms.TextBox();
            this.txbUwagi = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnWybierzZdjecie = new System.Windows.Forms.Button();
            this.btnDodaj = new System.Windows.Forms.Button();
            this.btnKategorie = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.zdjecieSkladnika = new System.Windows.Forms.PictureBox();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zdjecieSkladnika)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nazwa:";
            // 
            // txbNazwa
            // 
            this.txbNazwa.Location = new System.Drawing.Point(62, 10);
            this.txbNazwa.MaxLength = 50;
            this.txbNazwa.Name = "txbNazwa";
            this.txbNazwa.Size = new System.Drawing.Size(355, 20);
            this.txbNazwa.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Kategoria:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Opis:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 270);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Uwagi:";
            // 
            // cbKategorie
            // 
            this.cbKategorie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKategorie.FormattingEnabled = true;
            this.cbKategorie.Location = new System.Drawing.Point(62, 42);
            this.cbKategorie.Name = "cbKategorie";
            this.cbKategorie.Size = new System.Drawing.Size(228, 21);
            this.cbKategorie.TabIndex = 5;
            // 
            // txbOpis
            // 
            this.txbOpis.Location = new System.Drawing.Point(62, 72);
            this.txbOpis.MaxLength = 5000;
            this.txbOpis.Multiline = true;
            this.txbOpis.Name = "txbOpis";
            this.txbOpis.Size = new System.Drawing.Size(355, 180);
            this.txbOpis.TabIndex = 6;
            // 
            // txbUwagi
            // 
            this.txbUwagi.Location = new System.Drawing.Point(62, 267);
            this.txbUwagi.MaxLength = 5000;
            this.txbUwagi.Multiline = true;
            this.txbUwagi.Name = "txbUwagi";
            this.txbUwagi.Size = new System.Drawing.Size(355, 180);
            this.txbUwagi.TabIndex = 7;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Tylko obrazy (*.jpg, *.jpeg, *.png, *.bmp, *.gif) | *.jpg; *.jpeg; *.png; *.bmp; " +
    "*gif";
            // 
            // btnWybierzZdjecie
            // 
            this.btnWybierzZdjecie.Location = new System.Drawing.Point(444, 222);
            this.btnWybierzZdjecie.Name = "btnWybierzZdjecie";
            this.btnWybierzZdjecie.Size = new System.Drawing.Size(237, 23);
            this.btnWybierzZdjecie.TabIndex = 9;
            this.btnWybierzZdjecie.Text = "Przeglądaj...";
            this.btnWybierzZdjecie.UseVisualStyleBackColor = true;
            this.btnWybierzZdjecie.Click += new System.EventHandler(this.btnWybierzZdjecie_Click);
            // 
            // btnDodaj
            // 
            this.btnDodaj.Location = new System.Drawing.Point(12, 462);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(672, 23);
            this.btnDodaj.TabIndex = 11;
            this.btnDodaj.Text = "Dodaj";
            this.btnDodaj.UseVisualStyleBackColor = true;
            this.btnDodaj.Click += new System.EventHandler(this.btnDodaj_Click);
            // 
            // btnKategorie
            // 
            this.btnKategorie.Location = new System.Drawing.Point(296, 40);
            this.btnKategorie.Name = "btnKategorie";
            this.btnKategorie.Size = new System.Drawing.Size(121, 23);
            this.btnKategorie.TabIndex = 12;
            this.btnKategorie.Text = "Zarządzaj kategoriami";
            this.btnKategorie.UseVisualStyleBackColor = true;
            this.btnKategorie.Click += new System.EventHandler(this.btnKategorie_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.zdjecieSkladnika);
            this.groupBox6.Location = new System.Drawing.Point(444, 10);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(237, 203);
            this.groupBox6.TabIndex = 14;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Zdjęcie";
            // 
            // zdjecieSkladnika
            // 
            this.zdjecieSkladnika.Location = new System.Drawing.Point(6, 20);
            this.zdjecieSkladnika.Name = "zdjecieSkladnika";
            this.zdjecieSkladnika.Size = new System.Drawing.Size(225, 177);
            this.zdjecieSkladnika.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.zdjecieSkladnika.TabIndex = 0;
            this.zdjecieSkladnika.TabStop = false;
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(696, 497);
            this.shapeContainer1.TabIndex = 15;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 429;
            this.lineShape1.X2 = 429;
            this.lineShape1.Y1 = 447;
            this.lineShape1.Y2 = 3;
            // 
            // DodajSkladnik
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 497);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.btnKategorie);
            this.Controls.Add(this.btnDodaj);
            this.Controls.Add(this.btnWybierzZdjecie);
            this.Controls.Add(this.txbUwagi);
            this.Controls.Add(this.txbOpis);
            this.Controls.Add(this.cbKategorie);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txbNazwa);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.shapeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "DodajSkladnik";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dodaj nowy składnik";
            this.Activated += new System.EventHandler(this.DodajSkladnik_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DodajSkladnik_FormClosed);
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.zdjecieSkladnika)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbNazwa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbKategorie;
        private System.Windows.Forms.TextBox txbOpis;
        private System.Windows.Forms.TextBox txbUwagi;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnWybierzZdjecie;
        private System.Windows.Forms.Button btnDodaj;
        private System.Windows.Forms.Button btnKategorie;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.PictureBox zdjecieSkladnika;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
    }
}