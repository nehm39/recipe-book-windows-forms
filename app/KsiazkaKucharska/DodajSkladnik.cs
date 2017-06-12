using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace KsiazkaKucharska
{
    public partial class DodajSkladnik : Form
    {
        string sciezka = null;
        private MainForm mainForm = null;
        int mainFormSelection;
        bool isEdit = false;
        bool added = false;
        string nazwaEdytowanegoSkladnika = null;
        string strSqlCon = ConfigurationManager.ConnectionStrings["KsiazkaKucharskaConnectionString"].ConnectionString;

        //Konstruktor służący do dodawania składnika.
        public DodajSkladnik(Form cForm, int selectedId)
        {
            mainForm = cForm as MainForm;
            mainFormSelection = selectedId;
            InitializeComponent();
            wypelnijKategorie();
        }

        //Konstruktor służący do edytowania składnika.
        public DodajSkladnik(Form cForm, int selectedId, bool edit)
        {
            isEdit = true;
            mainForm = cForm as MainForm;
            mainFormSelection = selectedId;
            InitializeComponent();
            wypelnijKategorie();
            this.Text = "Edytuj składnik";
            btnWybierzZdjecie.Text = "Zmień zdjęcie";
            btnDodaj.Text = "Zapisz";
            SqlConnection conn = new SqlConnection(strSqlCon);
            conn.Open();
            SqlCommand comm = new SqlCommand("SELECT * FROM Skladniki WHERE ID_SKLADNIKA = " + selectedId, conn);
            SqlDataReader reader = comm.ExecuteReader();
            int idKategorii = -1;
            while (reader.Read())
            {
                txbNazwa.Text = reader.GetString(1);
                nazwaEdytowanegoSkladnika = reader.GetString(1);
                idKategorii = reader.GetInt32(2);
                txbOpis.Text = reader.GetString(3);
                txbUwagi.Text = reader.GetString(4);
                byte[] zdj = new byte[0];
                zdj = (byte[])reader.GetValue(5);
                if (zdj != null && zdj.Length > 0)
                {
                    MemoryStream stream = new MemoryStream(zdj);
                    zdjecieSkladnika.Image = Image.FromStream(stream);
                }
            }
            reader.Close();
            SqlCommand cmdNazwaKategorii = new SqlCommand("SELECT S_NAZWA from Kategorie_Skladniki WHERE S_ID_KATEGORII = " + idKategorii, conn);
            string nazwa = (string)cmdNazwaKategorii.ExecuteScalar();
            cbKategorie.SelectedItem = nazwa;
            conn.Close();
        }


        private void wypelnijKategorie()
        {
            //Wypełniamy checkboxa listą kategorii.
            //Jeżeli wcześniej była zaznaczona jakaś kategoria to na koniec zaznaczamy ją ponownie.
            string selected;
            try
            {
                selected = cbKategorie.SelectedItem.ToString();
            }
            catch (NullReferenceException)
            {
                selected = null;
            }
            cbKategorie.Items.Clear();
            SqlConnection conn = new SqlConnection(strSqlCon);
            conn.Open();
            SqlCommand comm = new SqlCommand("SELECT DISTINCT S_NAZWA FROM Kategorie_Skladniki ORDER BY S_NAZWA ASC", conn);
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                cbKategorie.Items.Add(reader.GetString(0));
            }
            reader.Close();
            conn.Close();
            if (selected != null) cbKategorie.SelectedItem = selected;
        }

        private void btnWybierzZdjecie_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                zdjecieSkladnika.Image = Image.FromFile(openFileDialog1.FileName);
                sciezka = openFileDialog1.FileName;
            }
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(strSqlCon);
            conn.Open();
            SqlCommand cmdNazwa = new SqlCommand("SELECT ID_SKLADNIKA FROM SKLADNIKI WHERE NAZWA = '" + txbNazwa.Text.ToString() + "'", conn);
            int idSkladnika;
            try
            {
                idSkladnika = (int)cmdNazwa.ExecuteScalar();
            }
            catch
            {
                idSkladnika = -1;
            }
            if ((String.IsNullOrWhiteSpace(txbNazwa.Text.ToString())) || cbKategorie.SelectedItem == null)
            {
                MessageBox.Show("Pole z nazwą oraz kategoria nie mogą być puste.",
                "Błąd",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }
            else if ((idSkladnika != -1 && isEdit == false) || (isEdit == true && idSkladnika != -1 && (!txbNazwa.Text.ToString().Equals(nazwaEdytowanegoSkladnika))))
            {
                MessageBox.Show("Istnieje już składnik o takiej nazwie.",
                "Błąd",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }
            else
            {

                try
                {
                    SqlCommand cmd;
                    if (isEdit) cmd = new SqlCommand("UPDATE Skladniki SET NAZWA = @nazwa, ID_KATEGORII = @kategoria, OPIS = @opis, UWAGI = @uwagi" + ((!(String.IsNullOrWhiteSpace(sciezka))) ? ", ZDJECIE = @zdjecie" : "") + " WHERE ID_SKLADNIKA = " + mainFormSelection, conn);
                    else cmd = new SqlCommand("INSERT into Skladniki VALUES (@nazwa, @kategoria, @opis, @uwagi " + ((!(String.IsNullOrWhiteSpace(sciezka))) ? ", @zdjecie" : ", ''") + "); SELECT CAST(scope_identity() AS int)", conn);
                    cmd.Parameters.AddWithValue("@nazwa", txbNazwa.Text);
                    string kategoria = cbKategorie.SelectedItem.ToString();
                    SqlCommand cmdKategoria = new SqlCommand("SELECT S_ID_KATEGORII FROM Kategorie_Skladniki WHERE S_NAZWA LIKE '" + kategoria + "'", conn);
                    int idKategorii = (int)cmdKategoria.ExecuteScalar();
                    cmd.Parameters.AddWithValue("@kategoria", idKategorii);
                    cmd.Parameters.AddWithValue("@opis", txbOpis.Text);
                    cmd.Parameters.AddWithValue("@uwagi", txbUwagi.Text);
                    if (!(String.IsNullOrWhiteSpace(sciezka)))
                    {
                        byte[] image = File.ReadAllBytes(sciezka);
                        cmd.Parameters.AddWithValue("@zdjecie", image);
                    }
                    if (isEdit)
                    {
                        cmd.ExecuteNonQuery();
                        this.mainForm.wypiszListeSkladnikow(mainFormSelection);
                    }
                    else
                    {
                        int noweId = (int)cmd.ExecuteScalar();
                        this.mainForm.wypiszListeSkladnikow(noweId);
                    }
                    added = true;
                    this.FindForm().Close();

                }
                catch
                {
                    MessageBox.Show("Wystąpił błąd podczas zapisywania składnika. Spróbuj ponownie.",
                    "Błąd",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);

                }
                finally
                {
                    conn.Close();
                }
            }
        }


        private void btnKategorie_Click(object sender, EventArgs e)
        {
            //Otwieramy formę zarządzania kategoriami.
            //Jeżeli jakaś kategoria została wybrana w checkboxie to przekazujemy ją do konstruktora formy.
            string kategoria;
            try
            {
                kategoria = cbKategorie.SelectedItem.ToString();
            }
            catch (NullReferenceException)
            {
                kategoria = null;
            }
            ZarzadzanieKategoriami kategorie = new ZarzadzanieKategoriami(kategoria, false);
            kategorie.ShowDialog();
        }

        //Po aktywacji formy wywołujemy metodę wypełniającą checbkoxa z kategorami.
        private void DodajSkladnik_Activated(object sender, EventArgs e)
        {
            wypelnijKategorie();
        }

        //Po zamknięciu tej formy, odświeżamy listę składników w głównej formie.
        private void DodajSkladnik_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!added) this.mainForm.wypiszListeSkladnikow(mainFormSelection);
        }
    }
}
