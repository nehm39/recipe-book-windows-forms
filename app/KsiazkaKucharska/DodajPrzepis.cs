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
    public partial class DodajPrzepis : Form
    {
        private string sciezka = null;
        private MainForm mainForm = null;
        private int mainFormSelection;
        private bool isEdit = false;
        private string nazwaEdytowanegoPrzepisu;
        private string strSqlCon = ConfigurationManager.ConnectionStrings["KsiazkaKucharskaConnectionString"].ConnectionString;


        //Konstruktor służący do dodawania przepisu.
        public DodajPrzepis(Form cForm, int selectedId)
        {
            mainForm = cForm as MainForm;
            mainFormSelection = selectedId;
            InitializeComponent();
            wypelnijKategorie();
            nazwySkladnikow();
            nazwyJednostek();
            }

        //Konstruktor służący do edytowania przepisu.
        public DodajPrzepis(Form cForm, int selectedId, bool edit)
        {
            mainForm = cForm as MainForm;
            mainFormSelection = selectedId;
            isEdit = true;
            InitializeComponent();
            wypelnijKategorie();
            this.Text = "Edytuj przepis";
            btnWybierzZdjecie.Text = "Zmień zdjęcie";
            btnDodaj.Text = "Zapisz";
            nazwySkladnikow();
            nazwyJednostek();

            SqlConnection conn = new SqlConnection(strSqlCon);
            conn.Open();
            SqlCommand comm = new SqlCommand("SELECT * FROM Przepisy WHERE ID_PRZEPISU = " + selectedId, conn);
            SqlDataReader reader = comm.ExecuteReader();
            int idKategorii = -1;
            while (reader.Read())
            {
                txbNazwa.Text = reader.GetString(1);
                nazwaEdytowanegoPrzepisu = reader.GetString(1);
                idKategorii = reader.GetInt32(2);
                txbOpis.Text = reader.GetString(3);
                txbNotatki.Text = reader.GetString(4);
                string ilosc = reader.GetDouble(5).ToString();
                ilosc = ilosc.Replace(",", ".");
                txbIloscPorcji.Text = ilosc.ToString();
                int czas = reader.GetInt32(6);
                int godziny = czas / 60;
                txbCzasH.Text = godziny.ToString();
                int minuty = czas - godziny * 60;
                txbCzasM.Text = minuty.ToString();
                byte[] zdj = new byte[0];
                zdj = (byte[])reader.GetValue(7);
                if (zdj != null && zdj.Length > 0)
                {
                    MemoryStream stream = new MemoryStream(zdj);
                    zdjeciePrzepisu.Image = Image.FromStream(stream);
                }
            }
            reader.Close();
            SqlCommand cmdNazwaKategorii = new SqlCommand("SELECT P_NAZWA from Kategorie_Przepisy WHERE P_ID_KATEGORII = " + idKategorii, conn);
            string nazwa = (string)cmdNazwaKategorii.ExecuteScalar();
            cbKategorie.SelectedItem = nazwa;

            SqlCommand cmdSkladniki = new SqlCommand("SELECT * FROM Przepisy_Skladniki where L_ID_PRZEPISU = " + selectedId, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmdSkladniki);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                int x = Convert.ToInt32(dr["L_ID_SKLADNIKA"].ToString());
                SqlCommand commNazwa = new SqlCommand("SELECT NAZWA FROM Skladniki where ID_SKLADNIKA = " + x, conn);
                string nazwaSkladnika = (string)commNazwa.ExecuteScalar();
                string jednostka;
                try
                {
                    int idJednostki = Convert.ToUInt16(dr["ID_JEDNOSTKI"]);
                    SqlCommand commJednostka = new SqlCommand("SELECT NAZWA FROM Jednostki where ID_JEDNOSTKI = " + idJednostki, conn);
                    jednostka = (string)commJednostka.ExecuteScalar();
                }
                catch
                {
                    jednostka = "";
                }
                string ilosc = dr["ILOSC"].ToString();
                ilosc = ilosc.Replace(",", ".");
                gvListaSkladnikow.Rows.Add(nazwaSkladnika, ilosc, jednostka, x);
            }
            conn.Close();
            
        }

        //Metoda dodaje wszystkie składniki do checkboxa.
        private void nazwySkladnikow()
        {
            cbJednostki.Items.Clear();
            SqlConnection conn = new SqlConnection(strSqlCon);
            conn.Open();
            SqlCommand comm = new SqlCommand("SELECT NAZWA FROM Skladniki ORDER BY NAZWA ASC" , conn);
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                cbNazwySkladnikow.Items.Add(reader.GetString(0));
            }
            reader.Close();
            conn.Close();
            cbNazwySkladnikow.SelectedIndex = 0;
        }

        //Metoda dodaje wszystkie jednostki do checkboxa.
        private void nazwyJednostek()
        {
            cbJednostki.Items.Clear();
            cbJednostki.Items.Add("");
            cbJednostki.SelectedIndex = 0;
            SqlConnection conn = new SqlConnection(strSqlCon);
            conn.Open();
            SqlCommand comm = new SqlCommand("SELECT NAZWA FROM Jednostki ORDER BY NAZWA ASC", conn);
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                cbJednostki.Items.Add(reader.GetString(0));
            }
            reader.Close();
            conn.Close();
        }

        //Metoda dodaje wszystkie kategorie do checkboxa.
        private void wypelnijKategorie()
        {
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
            SqlCommand comm = new SqlCommand("SELECT DISTINCT P_NAZWA FROM Kategorie_Przepisy ORDER BY P_NAZWA ASC", conn);
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                cbKategorie.Items.Add(reader.GetString(0));
            }
            reader.Close();
            conn.Close();
            if (selected != null) cbKategorie.SelectedItem = selected;
        }

        //Metoda sprawdzająca czy wpisywane wartości są wartościami double.
        private void txbDouble_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (ch == 46 && txbIloscPorcji.Text.IndexOf('.') > 0)
            {
                e.Handled = true;
                return;
            }

            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
                return;
            }
        }

        //Metoda sprawdzająca czy wpisywane wartości są wartościami int.
        private void txbInt_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
                return;
            }
        }

        //Metoda sprawdza czy wartość wprowadzona w minutach nie jest większa niż 59.
        private void txbCzasM_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txbCzasM.Text.ToString()))
            {
                if (Convert.ToUInt16(txbCzasM.Text.ToString()) > 59)
                {
                    txbCzasM.Text = "59";
                }
            }
        }

        
        private void btnKategorie_Click(object sender, EventArgs e)
        {
            //Po kliknięciu na przycisk otwieramy formę zarządzającą kategoriami.
            string kategoria;
            try
            {
                kategoria = cbKategorie.SelectedItem.ToString();
            }
            catch (NullReferenceException)
            {
                kategoria = null;
            }
            ZarzadzanieKategoriami kategorie = new ZarzadzanieKategoriami(kategoria, true);
            kategorie.ShowDialog();
        }

        private void btnWybierzZdjecie_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                zdjeciePrzepisu.Image = Image.FromFile(openFileDialog1.FileName);
                sciezka = openFileDialog1.FileName;
            }
        }


        private void btnDodajSkladnik_Click(object sender, EventArgs e)
        {
            //Sprawdzamy czy składnik już nie istnieje oraz dodajemy go do listy.
            string nazwaSkladnika = cbNazwySkladnikow.SelectedItem.ToString();
            bool spr = false;
            foreach (DataGridViewRow dg in gvListaSkladnikow.Rows)
            {
                if (dg.Cells[0].Value.ToString().Equals(nazwaSkladnika)) spr = true;
            }
            if (spr == false)
            {
                SqlConnection conn = new SqlConnection(strSqlCon);
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT ID_SKLADNIKA FROM Skladniki WHERE NAZWA = '" + nazwaSkladnika + "'", conn);
                int idSkladnika = (int)comm.ExecuteScalar();
                string jednostka;
                try
                {
                    jednostka = cbJednostki.SelectedItem.ToString();
                }
                catch
                {
                    jednostka = "";
                }
                gvListaSkladnikow.Rows.Add(nazwaSkladnika, txbSkladnikiIlosc.Text.ToString(), jednostka, idSkladnika);
            }
            else
            {
                MessageBox.Show("Taki składnik już został dodany do tego przepisu.",
                "Błąd",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }
        }

        //Sprawdzanie czy wartość jest wartością double w textboxie SkladnikiIlosc.
        private void txbSkladnikiIlosc_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (ch == 46 && txbSkladnikiIlosc.Text.IndexOf('.') > 0)
            {
                e.Handled = true;
                return;
            }

            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
                return;
            }
        }


        private void gvListaSkladnikow_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //W przypadku kliknięcia na przycisk "Edytuj składnik" wyświetlamy nową formę.
            if (e.ColumnIndex == 4)
            {
                string nazwa = gvListaSkladnikow.Rows[e.RowIndex].Cells[0].Value.ToString();
                string ilosc = gvListaSkladnikow.Rows[e.RowIndex].Cells[1].Value.ToString();
                string jednostka = gvListaSkladnikow.Rows[e.RowIndex].Cells[2].Value.ToString();
                int id = Convert.ToInt32(gvListaSkladnikow.Rows[e.RowIndex].Cells[3].Value);
                EdytujSkladnikPrzepisu edytuj = new EdytujSkladnikPrzepisu(this, e.RowIndex, nazwa, ilosc, jednostka);
                edytuj.ShowDialog();
            }
            //W przypadku kliknięcia na przycisk "Usuń składnik" usuwamy składnik z listy.
            if (e.ColumnIndex == 5)
            {
                gvListaSkladnikow.Rows.RemoveAt(e.RowIndex);
            }
        }

        //Po aktywacji formy wywołujemy metodę wypełniającą checkboxa z kategoriami.
        private void DodajPrzepis_Activated(object sender, EventArgs e)
        {
            wypelnijKategorie();
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(strSqlCon);
            conn.Open();
            //Próbujemy pobrać id przepisu o podanej nazwie.
            SqlCommand cmdNazwa = new SqlCommand("SELECT ID_PRZEPISU FROM Przepisy WHERE NAZWA = '" + txbNazwa.Text.ToString() + "'", conn);
            int idPrzepisu;
            try
            {
                idPrzepisu = (int)cmdNazwa.ExecuteScalar();
            }
            catch
            {
                idPrzepisu = -1;
            }
            //Jeżeli pole z nazwą lub kategoria jest pusta to wyświetlamy błąd.
            if ((String.IsNullOrWhiteSpace(txbNazwa.Text.ToString())) || cbKategorie.SelectedItem == null)
            {
                MessageBox.Show("Pole z nazwą oraz kategoria nie mogą być puste.",
                "Błąd",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }
            //Jeżeli nie zostały dodane żadne składniki to wyświetlamy błąd.
            else if (gvListaSkladnikow.Rows.Count == 0)
            {
                MessageBox.Show("Musisz dodać jakieś składniki.",
                "Błąd",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }
            //Jeżeli nie został wprowadzony opis to wyświetlamy błąd.
            else if (String.IsNullOrWhiteSpace(txbOpis.Text.ToString()))
            {
                MessageBox.Show("Musisz dodać opis przepisu.",
                "Błąd",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }
            //Jeżeli przepis o takiej nazwie już istnieje (i nie jest to przepis, który edytujemy) to wyświetlamy błąd.
            else if ((idPrzepisu != -1 && isEdit == false) || (isEdit == true && idPrzepisu != -1 && (!txbNazwa.Text.ToString().Equals(nazwaEdytowanegoPrzepisu))))
            {
                MessageBox.Show("Istnieje już przepis o takiej nazwie.",
                "Błąd",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }
            else
            {
                //Wywołujemy odpowiednie zapytania do bazy.
                SqlCommand cmd;
                if (isEdit) cmd = new SqlCommand("UPDATE Przepisy SET NAZWA = @nazwa, ID_KATEGORII = @kategoria, OPIS = @opis, NOTATKI = @notatki, ILOSC_PORCJI = @iloscPorcji, CZAS_PRZYRZADZENIA = @czasPrzyrzadzenia" + ((!(String.IsNullOrWhiteSpace(sciezka))) ? ", ZDJECIE = @zdjecie" : "") + " WHERE ID_PRZEPISU = " + mainFormSelection, conn);
                else cmd = new SqlCommand("INSERT into Przepisy VALUES (@nazwa, @kategoria, @opis, @notatki, @iloscPorcji, @czasPrzyrzadzenia " + ((!(String.IsNullOrWhiteSpace(sciezka))) ? ", @zdjecie" : ", ''") + "); SELECT CAST(scope_identity() AS int)", conn);
                cmd.Parameters.AddWithValue("@nazwa", txbNazwa.Text);
                string kategoria = cbKategorie.SelectedItem.ToString();
                SqlCommand cmdKategoria = new SqlCommand("SELECT P_ID_KATEGORII FROM Kategorie_Przepisy WHERE P_NAZWA LIKE '" + kategoria + "'", conn);
                int idKategorii = (int)cmdKategoria.ExecuteScalar();
                cmd.Parameters.AddWithValue("@kategoria", idKategorii);
                cmd.Parameters.AddWithValue("@opis", txbOpis.Text);
                cmd.Parameters.AddWithValue("@notatki", txbNotatki.Text);
                cmd.Parameters.AddWithValue("@iloscPorcji", txbIloscPorcji.Text);
                int minuty;
                if (String.IsNullOrWhiteSpace(txbCzasM.Text.ToString())) minuty = 0;
                else minuty = Convert.ToUInt16(txbCzasM.Text.ToString());
                int godziny;
                if (String.IsNullOrWhiteSpace(txbCzasH.Text.ToString())) godziny = 0;
                else godziny = Convert.ToUInt16(txbCzasH.Text.ToString());
                int czas = minuty * 60 + godziny;
                cmd.Parameters.AddWithValue("@czasPrzyrzadzenia", czas);
                if (!(String.IsNullOrWhiteSpace(sciezka)))
                {
                    byte[] image = File.ReadAllBytes(sciezka);
                    cmd.Parameters.AddWithValue("@zdjecie", image);
                }
                if (isEdit)
                {
                    cmd.ExecuteNonQuery();
                    SqlCommand cmdCzyszczenie = new SqlCommand("DELETE FROM Przepisy_Skladniki WHERE L_ID_PRZEPISU = " + mainFormSelection, conn);
                    cmdCzyszczenie.ExecuteNonQuery();
                    foreach (DataGridViewRow dgv in gvListaSkladnikow.Rows)
                    {
                        int idJednostki;
                        try
                        {
                            SqlCommand cmdIdJednostki = new SqlCommand("SELECT ID_JEDNOSTKI FROM Jednostki WHERE NAZWA = '" + dgv.Cells[2].Value.ToString() + "'", conn);
                            idJednostki = (int)cmdIdJednostki.ExecuteScalar();
                        }
                        catch
                        {
                            idJednostki = -1;
                        }
                        SqlCommand cmdLink = new SqlCommand("INSERT INTO Przepisy_Skladniki VALUES (" + mainFormSelection + ", " + Convert.ToUInt16(dgv.Cells[3].Value) + ((!(String.IsNullOrWhiteSpace(dgv.Cells[1].Value.ToString()))) ? ", @iloscPorcji, " : ", null, ") + ((idJednostki != -1) ? "" + idJednostki : "null") + ")", conn);
                        cmdLink.Parameters.AddWithValue("@iloscPorcji", dgv.Cells[1].Value);
                        cmdLink.ExecuteNonQuery();
                    }
                    //Odświeżamy listę w głównej formie i zaznaczamy edytowany przepis.
                    this.mainForm.wypiszListePrzepisow(mainFormSelection);
                }
                else
                {
                    int noweId = (int)cmd.ExecuteScalar();
                    foreach (DataGridViewRow dgv in gvListaSkladnikow.Rows)
                    {
                        int idJednostki;
                        try
                        {
                            SqlCommand cmdIdJednostki = new SqlCommand("SELECT ID_JEDNOSTKI FROM Jednostki WHERE NAZWA = '" + dgv.Cells[2].Value.ToString() + "'", conn);
                            idJednostki = (int)cmdIdJednostki.ExecuteScalar();
                        }
                        catch
                        {
                            idJednostki = -1;
                        }
                        SqlCommand cmdLink = new SqlCommand("INSERT INTO Przepisy_Skladniki VALUES (" + noweId + ", " + Convert.ToUInt16(dgv.Cells[3].Value) + ((!(String.IsNullOrWhiteSpace(dgv.Cells[1].Value.ToString()))) ? ", @iloscPorcji, " : ", null, ") + ((idJednostki != -1) ? "" + idJednostki : "null") + ")", conn);
                        cmdLink.Parameters.AddWithValue("@iloscPorcji", dgv.Cells[1].Value);  
                        cmdLink.ExecuteNonQuery();
                    }
                    //Odświeżamy listę w głównej formie i zaznaczamy przepis, który właśnie dodaliśmy.
                    this.mainForm.wypiszListePrzepisow(noweId);
                }
                this.FindForm().Close();
            }
            conn.Close();
        }

        //Publiczna metoda do edytowania składnika na liście.
        public void edytujSkladnik(int rowIndex, string nazwa, string ilosc, string jednostka, int id)
        {
            gvListaSkladnikow.Rows[rowIndex].Cells[0].Value = nazwa;
            gvListaSkladnikow.Rows[rowIndex].Cells[1].Value = ilosc;
            gvListaSkladnikow.Rows[rowIndex].Cells[2].Value = jednostka;
            gvListaSkladnikow.Rows[rowIndex].Cells[3].Value = id.ToString();
        }

        //Publiczna metoda do sprawdzania czy składnik istnieje już na liście.
        public bool skladnikIstnieje(string nazwa)
        {
            foreach (DataGridViewRow dgv in gvListaSkladnikow.Rows)
            {
                if (dgv.Cells[0].Value.ToString().Equals(nazwa)) return true;
            }
            return false;
        }

    }
}
