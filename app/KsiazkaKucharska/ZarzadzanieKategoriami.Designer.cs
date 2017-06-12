namespace KsiazkaKucharska
{
    partial class ZarzadzanieKategoriami
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
            this.cbRodzajeKategorii = new System.Windows.Forms.ComboBox();
            this.cbListaKategorii = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEdytuj = new System.Windows.Forms.Button();
            this.btnUsun = new System.Windows.Forms.Button();
            this.btnDodaj = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbRodzajeKategorii
            // 
            this.cbRodzajeKategorii.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRodzajeKategorii.FormattingEnabled = true;
            this.cbRodzajeKategorii.Items.AddRange(new object[] {
            "Kategorie składników",
            "Kategorie przepisów"});
            this.cbRodzajeKategorii.Location = new System.Drawing.Point(15, 25);
            this.cbRodzajeKategorii.Name = "cbRodzajeKategorii";
            this.cbRodzajeKategorii.Size = new System.Drawing.Size(259, 21);
            this.cbRodzajeKategorii.TabIndex = 0;
            this.cbRodzajeKategorii.SelectedIndexChanged += new System.EventHandler(this.cbRodzajeKategorii_SelectedIndexChanged);
            // 
            // cbListaKategorii
            // 
            this.cbListaKategorii.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbListaKategorii.FormattingEnabled = true;
            this.cbListaKategorii.Location = new System.Drawing.Point(15, 67);
            this.cbListaKategorii.Name = "cbListaKategorii";
            this.cbListaKategorii.Size = new System.Drawing.Size(257, 21);
            this.cbListaKategorii.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Wybierz kategorię:";
            // 
            // btnEdytuj
            // 
            this.btnEdytuj.Location = new System.Drawing.Point(15, 133);
            this.btnEdytuj.Name = "btnEdytuj";
            this.btnEdytuj.Size = new System.Drawing.Size(259, 23);
            this.btnEdytuj.TabIndex = 3;
            this.btnEdytuj.Text = "Edytuj zaznaczoną kategorię";
            this.btnEdytuj.UseVisualStyleBackColor = true;
            this.btnEdytuj.Click += new System.EventHandler(this.btnEdytuj_Click);
            // 
            // btnUsun
            // 
            this.btnUsun.Location = new System.Drawing.Point(15, 162);
            this.btnUsun.Name = "btnUsun";
            this.btnUsun.Size = new System.Drawing.Size(259, 23);
            this.btnUsun.TabIndex = 4;
            this.btnUsun.Text = "Usuń zaznaczoną kategorię";
            this.btnUsun.UseVisualStyleBackColor = true;
            this.btnUsun.Click += new System.EventHandler(this.btnUsun_Click);
            // 
            // btnDodaj
            // 
            this.btnDodaj.Location = new System.Drawing.Point(15, 104);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(259, 23);
            this.btnDodaj.TabIndex = 5;
            this.btnDodaj.Text = "Dodaj nową kategorię dla zaznaczonego rodzaju";
            this.btnDodaj.UseVisualStyleBackColor = true;
            this.btnDodaj.Click += new System.EventHandler(this.btnDodaj_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Wybierz rodzaj kategorii:";
            // 
            // ZarzadzanieKategoriami
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 201);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDodaj);
            this.Controls.Add(this.btnUsun);
            this.Controls.Add(this.btnEdytuj);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbListaKategorii);
            this.Controls.Add(this.cbRodzajeKategorii);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ZarzadzanieKategoriami";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Zarządzaj kategoriami";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbRodzajeKategorii;
        private System.Windows.Forms.ComboBox cbListaKategorii;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEdytuj;
        private System.Windows.Forms.Button btnUsun;
        private System.Windows.Forms.Button btnDodaj;
        private System.Windows.Forms.Label label2;
    }
}