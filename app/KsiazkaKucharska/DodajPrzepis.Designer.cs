namespace KsiazkaKucharska
{
    partial class DodajPrzepis
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
            this.txbOpis = new System.Windows.Forms.TextBox();
            this.zdjeciePrzepisu = new System.Windows.Forms.PictureBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnKategorie = new System.Windows.Forms.Button();
            this.btnDodaj = new System.Windows.Forms.Button();
            this.btnWybierzZdjecie = new System.Windows.Forms.Button();
            this.txbNotatki = new System.Windows.Forms.TextBox();
            this.cbKategorie = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txbNazwa = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gvListaSkladnikow = new System.Windows.Forms.DataGridView();
            this.DP_Nazwa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DP_Ilosc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DP_Jednostka = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DP_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DP_Edytuj = new System.Windows.Forms.DataGridViewButtonColumn();
            this.DP_Usun = new System.Windows.Forms.DataGridViewButtonColumn();
            this.txbIloscPorcji = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txbCzasM = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txbCzasH = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cbNazwySkladnikow = new System.Windows.Forms.ComboBox();
            this.cbJednostki = new System.Windows.Forms.ComboBox();
            this.txbSkladnikiIlosc = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnDodajSkladnik = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.zdjeciePrzepisu)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvListaSkladnikow)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txbOpis
            // 
            this.txbOpis.Location = new System.Drawing.Point(66, 74);
            this.txbOpis.MaxLength = 5000;
            this.txbOpis.Multiline = true;
            this.txbOpis.Name = "txbOpis";
            this.txbOpis.Size = new System.Drawing.Size(355, 141);
            this.txbOpis.TabIndex = 21;
            // 
            // zdjeciePrzepisu
            // 
            this.zdjeciePrzepisu.Location = new System.Drawing.Point(6, 19);
            this.zdjeciePrzepisu.Name = "zdjeciePrzepisu";
            this.zdjeciePrzepisu.Size = new System.Drawing.Size(225, 177);
            this.zdjeciePrzepisu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.zdjeciePrzepisu.TabIndex = 0;
            this.zdjeciePrzepisu.TabStop = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.zdjeciePrzepisu);
            this.groupBox6.Location = new System.Drawing.Point(446, 79);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(237, 203);
            this.groupBox6.TabIndex = 26;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Zdjęcie";
            // 
            // btnKategorie
            // 
            this.btnKategorie.Location = new System.Drawing.Point(300, 42);
            this.btnKategorie.Name = "btnKategorie";
            this.btnKategorie.Size = new System.Drawing.Size(121, 23);
            this.btnKategorie.TabIndex = 25;
            this.btnKategorie.Text = "Zarządzaj kategoriami";
            this.btnKategorie.UseVisualStyleBackColor = true;
            this.btnKategorie.Click += new System.EventHandler(this.btnKategorie_Click);
            // 
            // btnDodaj
            // 
            this.btnDodaj.Location = new System.Drawing.Point(11, 615);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(672, 23);
            this.btnDodaj.TabIndex = 24;
            this.btnDodaj.Text = "Zapisz";
            this.btnDodaj.UseVisualStyleBackColor = true;
            this.btnDodaj.Click += new System.EventHandler(this.btnDodaj_Click);
            // 
            // btnWybierzZdjecie
            // 
            this.btnWybierzZdjecie.Location = new System.Drawing.Point(446, 291);
            this.btnWybierzZdjecie.Name = "btnWybierzZdjecie";
            this.btnWybierzZdjecie.Size = new System.Drawing.Size(237, 23);
            this.btnWybierzZdjecie.TabIndex = 23;
            this.btnWybierzZdjecie.Text = "Przeglądaj...";
            this.btnWybierzZdjecie.UseVisualStyleBackColor = true;
            this.btnWybierzZdjecie.Click += new System.EventHandler(this.btnWybierzZdjecie_Click);
            // 
            // txbNotatki
            // 
            this.txbNotatki.Location = new System.Drawing.Point(66, 226);
            this.txbNotatki.MaxLength = 5000;
            this.txbNotatki.Multiline = true;
            this.txbNotatki.Name = "txbNotatki";
            this.txbNotatki.Size = new System.Drawing.Size(355, 141);
            this.txbNotatki.TabIndex = 22;
            // 
            // cbKategorie
            // 
            this.cbKategorie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKategorie.FormattingEnabled = true;
            this.cbKategorie.Location = new System.Drawing.Point(66, 44);
            this.cbKategorie.Name = "cbKategorie";
            this.cbKategorie.Size = new System.Drawing.Size(228, 21);
            this.cbKategorie.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 229);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Uwagi:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Opis:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Kategoria:";
            // 
            // txbNazwa
            // 
            this.txbNazwa.Location = new System.Drawing.Point(66, 12);
            this.txbNazwa.MaxLength = 50;
            this.txbNazwa.Name = "txbNazwa";
            this.txbNazwa.Size = new System.Drawing.Size(355, 20);
            this.txbNazwa.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Nazwa:";
            // 
            // gvListaSkladnikow
            // 
            this.gvListaSkladnikow.AllowUserToAddRows = false;
            this.gvListaSkladnikow.AllowUserToDeleteRows = false;
            this.gvListaSkladnikow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvListaSkladnikow.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DP_Nazwa,
            this.DP_Ilosc,
            this.DP_Jednostka,
            this.DP_ID,
            this.DP_Edytuj,
            this.DP_Usun});
            this.gvListaSkladnikow.Location = new System.Drawing.Point(6, 19);
            this.gvListaSkladnikow.Name = "gvListaSkladnikow";
            this.gvListaSkladnikow.ReadOnly = true;
            this.gvListaSkladnikow.Size = new System.Drawing.Size(660, 150);
            this.gvListaSkladnikow.TabIndex = 27;
            this.gvListaSkladnikow.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvListaSkladnikow_CellClick);
            // 
            // DP_Nazwa
            // 
            this.DP_Nazwa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DP_Nazwa.FillWeight = 150F;
            this.DP_Nazwa.HeaderText = "Nazwa";
            this.DP_Nazwa.Name = "DP_Nazwa";
            this.DP_Nazwa.ReadOnly = true;
            this.DP_Nazwa.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // DP_Ilosc
            // 
            this.DP_Ilosc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DP_Ilosc.HeaderText = "Ilość";
            this.DP_Ilosc.Name = "DP_Ilosc";
            this.DP_Ilosc.ReadOnly = true;
            // 
            // DP_Jednostka
            // 
            this.DP_Jednostka.HeaderText = "Jednostka";
            this.DP_Jednostka.Name = "DP_Jednostka";
            this.DP_Jednostka.ReadOnly = true;
            this.DP_Jednostka.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // DP_ID
            // 
            this.DP_ID.HeaderText = "ID";
            this.DP_ID.Name = "DP_ID";
            this.DP_ID.ReadOnly = true;
            this.DP_ID.Visible = false;
            // 
            // DP_Edytuj
            // 
            this.DP_Edytuj.HeaderText = "";
            this.DP_Edytuj.Name = "DP_Edytuj";
            this.DP_Edytuj.ReadOnly = true;
            this.DP_Edytuj.Text = "Edytuj";
            this.DP_Edytuj.UseColumnTextForButtonValue = true;
            this.DP_Edytuj.Width = 75;
            // 
            // DP_Usun
            // 
            this.DP_Usun.HeaderText = "";
            this.DP_Usun.Name = "DP_Usun";
            this.DP_Usun.ReadOnly = true;
            this.DP_Usun.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DP_Usun.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.DP_Usun.Text = "Usuń";
            this.DP_Usun.UseColumnTextForButtonValue = true;
            this.DP_Usun.Width = 75;
            // 
            // txbIloscPorcji
            // 
            this.txbIloscPorcji.Location = new System.Drawing.Point(563, 12);
            this.txbIloscPorcji.Name = "txbIloscPorcji";
            this.txbIloscPorcji.Size = new System.Drawing.Size(120, 20);
            this.txbIloscPorcji.TabIndex = 37;
            this.txbIloscPorcji.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbDouble_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(645, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(15, 13);
            this.label8.TabIndex = 36;
            this.label8.Text = "m";
            // 
            // txbCzasM
            // 
            this.txbCzasM.Location = new System.Drawing.Point(613, 49);
            this.txbCzasM.Name = "txbCzasM";
            this.txbCzasM.Size = new System.Drawing.Size(26, 20);
            this.txbCzasM.TabIndex = 35;
            this.txbCzasM.TextChanged += new System.EventHandler(this.txbCzasM_TextChanged);
            this.txbCzasM.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbInt_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(594, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "h";
            // 
            // txbCzasH
            // 
            this.txbCzasH.Location = new System.Drawing.Point(563, 49);
            this.txbCzasH.Name = "txbCzasH";
            this.txbCzasH.Size = new System.Drawing.Size(25, 20);
            this.txbCzasH.TabIndex = 33;
            this.txbCzasH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbInt_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(447, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Czas przyrządzenia:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(447, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "Ilość porcji:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Tylko obrazy (*.jpg, *.jpeg, *.png, *.bmp, *.gif) | *.jpg; *.jpeg; *.png; *.bmp; " +
    "*gif";
            // 
            // cbNazwySkladnikow
            // 
            this.cbNazwySkladnikow.FormattingEnabled = true;
            this.cbNazwySkladnikow.Location = new System.Drawing.Point(6, 191);
            this.cbNazwySkladnikow.Name = "cbNazwySkladnikow";
            this.cbNazwySkladnikow.Size = new System.Drawing.Size(165, 21);
            this.cbNazwySkladnikow.TabIndex = 39;
            // 
            // cbJednostki
            // 
            this.cbJednostki.FormattingEnabled = true;
            this.cbJednostki.Location = new System.Drawing.Point(258, 191);
            this.cbJednostki.Name = "cbJednostki";
            this.cbJednostki.Size = new System.Drawing.Size(121, 21);
            this.cbJednostki.TabIndex = 40;
            // 
            // txbSkladnikiIlosc
            // 
            this.txbSkladnikiIlosc.Location = new System.Drawing.Point(177, 192);
            this.txbSkladnikiIlosc.Name = "txbSkladnikiIlosc";
            this.txbSkladnikiIlosc.Size = new System.Drawing.Size(75, 20);
            this.txbSkladnikiIlosc.TabIndex = 41;
            this.txbSkladnikiIlosc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbSkladnikiIlosc_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 175);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 13);
            this.label10.TabIndex = 42;
            this.label10.Text = "Nazwa:";
            // 
            // btnDodajSkladnik
            // 
            this.btnDodajSkladnik.Location = new System.Drawing.Point(385, 189);
            this.btnDodajSkladnik.Name = "btnDodajSkladnik";
            this.btnDodajSkladnik.Size = new System.Drawing.Size(281, 23);
            this.btnDodajSkladnik.TabIndex = 43;
            this.btnDodajSkladnik.Text = "Dodaj";
            this.btnDodajSkladnik.UseVisualStyleBackColor = true;
            this.btnDodajSkladnik.Click += new System.EventHandler(this.btnDodajSkladnik_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.btnDodajSkladnik);
            this.groupBox1.Controls.Add(this.gvListaSkladnikow);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cbNazwySkladnikow);
            this.groupBox1.Controls.Add(this.txbSkladnikiIlosc);
            this.groupBox1.Controls.Add(this.cbJednostki);
            this.groupBox1.Location = new System.Drawing.Point(11, 377);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(672, 222);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lista składników";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(258, 174);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 13);
            this.label11.TabIndex = 45;
            this.label11.Text = "Jednostka:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(177, 174);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 44;
            this.label9.Text = "Ilość:";
            // 
            // DodajPrzepis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 646);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txbIloscPorcji);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txbCzasM);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txbCzasH);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txbOpis);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.btnKategorie);
            this.Controls.Add(this.btnDodaj);
            this.Controls.Add(this.btnWybierzZdjecie);
            this.Controls.Add(this.txbNotatki);
            this.Controls.Add(this.cbKategorie);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txbNazwa);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "DodajPrzepis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dodaj przepis";
            this.Activated += new System.EventHandler(this.DodajPrzepis_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.zdjeciePrzepisu)).EndInit();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvListaSkladnikow)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbOpis;
        private System.Windows.Forms.PictureBox zdjeciePrzepisu;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnKategorie;
        private System.Windows.Forms.Button btnDodaj;
        private System.Windows.Forms.Button btnWybierzZdjecie;
        private System.Windows.Forms.TextBox txbNotatki;
        private System.Windows.Forms.ComboBox cbKategorie;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbNazwa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gvListaSkladnikow;
        private System.Windows.Forms.TextBox txbIloscPorcji;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txbCzasM;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txbCzasH;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ComboBox cbNazwySkladnikow;
        private System.Windows.Forms.ComboBox cbJednostki;
        private System.Windows.Forms.TextBox txbSkladnikiIlosc;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnDodajSkladnik;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridViewTextBoxColumn DP_Nazwa;
        private System.Windows.Forms.DataGridViewTextBoxColumn DP_Ilosc;
        private System.Windows.Forms.DataGridViewTextBoxColumn DP_Jednostka;
        private System.Windows.Forms.DataGridViewTextBoxColumn DP_ID;
        private System.Windows.Forms.DataGridViewButtonColumn DP_Edytuj;
        private System.Windows.Forms.DataGridViewButtonColumn DP_Usun;
    }
}