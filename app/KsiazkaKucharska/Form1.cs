using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Drawing.Printing;

namespace KsiazkaKucharska
{
    public partial class MainForm : Form
    {
        //Connection string
        private string strSqlCon = ConfigurationManager.ConnectionStrings["KsiazkaKucharskaConnectionString"].ConnectionString;
        private bool panelSzukajPrzepisy = false;
        private bool panelSzukajSkladniki = false;
        private PrintDocument printDocument;
        private PrintDialog printDialog;
        private string nazwaZaznaczonegoPrzepisu;

        public MainForm()
        {
            InitializeComponent();
            //Ustawiamy wszystkie checkboxy na 1 pozycję z listy i wywołujemy metody uzupełniające gridview przepisami.
            cbSzukajPrzepis1.SelectedIndex = 1;
            cbSzukajPrzepis2.SelectedIndex = 1;
            cbSzukajPrzepis3.SelectedIndex = 1;
            cbSzukajPrzepis4.SelectedIndex = 1;
            wypiszListePrzepisow(-1);
            cbSzukajSkladnika1.SelectedIndex = 1;
            cbSzukajSkladnika2.SelectedIndex = 1;
            cbSzukajSkladnika3.SelectedIndex = 1;
            wypiszListeSkladnikow(-1);
            

        }

        //Metoda wypełniająca gridview przepisami z bazy danych.
        //Jako parametr przyjmuję ID przepisu, który ma zostać zaznaczony po wypisaniu.
        public void wypiszListePrzepisow(int selectionID)
        {
            SqlConnection conn = new SqlConnection(strSqlCon);
            SqlCommand comm = new SqlCommand("SELECT * from Przepisy ORDER BY NAZWA ASC", conn);
            SqlDataAdapter da = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvListaPrzepisow.Rows.Clear();

            //Dodajemy po kolei przepisy z bazy:
            foreach (DataRow dr in dt.Rows)
            {
                gvListaPrzepisow.Rows.Add(dr["NAZWA"].ToString(), dr["ID_PRZEPISU"].ToString());
            }

            //Jeżeli podane ID jest różne od -1 to znajdujemy odpowiedni przepis i zaznaczamy go.
            if (selectionID != -1)
            {
                int rowIndex = -1;
                foreach (DataGridViewRow row in gvListaPrzepisow.Rows)
                {
                    if (Convert.ToUInt16(row.Cells[1].Value) == selectionID)
                    {
                        rowIndex = row.Index;
                        break;
                    }
                }
                if (rowIndex != -1)
                {
                    gvListaPrzepisow.CurrentCell = gvListaPrzepisow.Rows[rowIndex].Cells[0];
                }
            }
            dataGridView1_SelectionChanged(null, null);
        }

        //Metoda wyszukająca przepisy po zmianie wartości w textboxie wyszukiwania:
        private void txbSzukaj_TextChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(strSqlCon);
            //Znajdujemy w bazie wyniki odpowiadające wartości z textboxa:
            SqlCommand comm = new SqlCommand("SELECT * FROM Przepisy where NAZWA like @txtNazwa", conn);
            comm.Parameters.AddWithValue("txtNazwa", "%" + txbSzukajPrzepisy.Text.ToString() + "%");
            SqlDataAdapter da = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            da.Fill(dt);
            //Czyścimy gridview
            gvListaPrzepisow.Rows.Clear();
            //Wypisujemy wszystkie wyniki:
            foreach (DataRow dr in dt.Rows)
            {
                gvListaPrzepisow.Rows.Add(dr["NAZWA"].ToString(), dr["ID_PRZEPISU"].ToString());
            }
        }

        //Metoda wywoływana przy zmianie zaznaczonego przepisu na gridview z przepisami:
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(strSqlCon);
            conn.Open();
            try
            {
                //Sczytujemy ID zaznaczonego przepisu.
                int i = Convert.ToUInt16(gvListaPrzepisow.CurrentRow.Cells["ID"].Value.ToString());
                //Znajdujemy w bazie przepis o danym ID.
                SqlCommand comm = new SqlCommand("SELECT * FROM Przepisy where ID_PRZEPISU = " + i, conn);
                SqlDataAdapter da = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataRow dr = dt.Rows[0];
                //Znajdujemy nazwę kategorii wybranego przepisu i wyświetlamy w labelce.
                int idKategorii = Convert.ToUInt16(dr["ID_KATEGORII"]);
                SqlCommand commNazwaKategorii = new SqlCommand("SELECT P_NAZWA FROM Kategorie_Przepisy where P_ID_KATEGORII = " + idKategorii, conn);
                string nazwa = (string)commNazwaKategorii.ExecuteScalar();
                txbPrzepisyKategoria.Text = nazwa;
                //Pobieramy wszystkie dane o przepisie i ustawiamy w odpowiednich labelkach.
                nazwaZaznaczonegoPrzepisu = dr["NAZWA"].ToString();
                string iloscPorcji = dr["ILOSC_PORCJI"].ToString();
                iloscPorcji = iloscPorcji.Replace(",", ".");
                txbIloscPorcji.Text = iloscPorcji;
                int czasM = Convert.ToUInt16(dr["CZAS_PRZYRZADZENIA"].ToString());
                int godziny = czasM / 60;
                txbCzasH.Text = godziny.ToString();
                int minuty = czasM - godziny * 60;
                txbCzasM.Text = minuty.ToString();
                txbOpis.Text = dr["OPIS"].ToString();
                txbNotatki.Text = dr["NOTATKI"].ToString();
                //Pobieramy zdjęcie z bazy i ustawiamy w PictureBox.
                byte[] zdj = new byte[0];
                zdj = (byte[])dr["ZDJECIE"];
                zdjeciePotrawy.Image = null;
                if (zdj != null && zdj.Length > 0)
                {
                    MemoryStream stream = new MemoryStream(zdj);
                    zdjeciePotrawy.Image = Image.FromStream(stream);
                }
                //Wywołujemy metodę wypisującą składniki dla danego przepisu.
                znajdzSkladniki(i, conn);

            }
            catch
            {
                //
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        //Metoda znajdująca składnika dla danego przepisu:
        private void znajdzSkladniki(int i, SqlConnection conn)
        {
            //Znajdujemy wszystkie składniki dla danego przepisu.
            SqlCommand comm = new SqlCommand("SELECT * FROM Przepisy_Skladniki where L_ID_PRZEPISU = " + i, conn);
            SqlDataAdapter da = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvSkladniki.Rows.Clear();
            //Dla każdego znalezionego składnika pobieramy dane i dodajemy nowe pozycji na gridview.
            foreach (DataRow dr in dt.Rows)
            {
                int x = Convert.ToInt32(dr["L_ID_SKLADNIKA"].ToString());
                SqlCommand commNazwa = new SqlCommand("SELECT NAZWA FROM Skladniki where ID_SKLADNIKA = " + x, conn);
                string nazwa = (string)commNazwa.ExecuteScalar();
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
                gvSkladniki.Rows.Add(nazwa, ilosc, jednostka, x);
            }
        }

        //Metoda wywoływana w momencie wciśnięcia przycisku odpowiedzialnego za rozwijanie/zwijanie wyszukiwarki
        private void btnPrzepisySzukanie_Click(object sender, EventArgs e)
        {
            //Jeżeli wyszukiwarka jest ukryta:
            if (panelSzukajPrzepisy == false)
            {
                //Zmieniamy tekst przycisku.
                btnPrzepisySzukanie.Text = "Zwiń zaawansowane wyszukiwanie";
                //Deaktywujemy labelkę i textbox szybkiego wyszukiwania oraz czyścimy textbox.
                lblSzybkieWyszukiwaniePrzepisy.Enabled = false;
                txbSzukajPrzepisy.Clear();
                txbSzukajPrzepisy.Enabled = false;
                //Pokazujemy panel.
                panelWyszukiwaniePrzepisow.Visible = true;
                panelSzukajPrzepisy = true;
                SqlConnection conn = new SqlConnection(strSqlCon);
                conn.Open();
                //Pobieramy wszystkie unikalne wartości ilości porcji oraz umieszczamy je w checkboxie.
                SqlCommand comm = new SqlCommand("SELECT DISTINCT ILOSC_PORCJI FROM Przepisy ORDER BY ILOSC_PORCJI ASC", conn);
                SqlDataReader reader = comm.ExecuteReader();
                cbSzukajPrzepisuIloscPorcji.Items.Clear();
                cbSzukajPrzepisuIloscPorcji.Items.Add("");
                while (reader.Read())
                {
                    string ilosc = reader.GetDouble(0).ToString();
                    //Zamieniamy przecinki na kropki, żeby uniknąć problemów przy pobieraniu z sqla wartości z przecinkami.
                    ilosc = ilosc.Replace(",", ".");
                    cbSzukajPrzepisuIloscPorcji.Items.Add(ilosc);
                }
                //Pobieramy wszystkie nazwy kategorii przepisów oraz umieszczamy je w checkboxie.
                comm = new SqlCommand("SELECT P_NAZWA FROM Kategorie_Przepisy ORDER BY P_NAZWA ASC", conn);
                reader.Close();
                reader = comm.ExecuteReader();
                cbSzukajPrzepisuKategorie.Items.Clear();
                cbSzukajPrzepisuKategorie.Items.Add("");
                while (reader.Read())
                {
                    cbSzukajPrzepisuKategorie.Items.Add(reader.GetString(0));
                }
                reader.Close();
                conn.Close();
            }
            //W przypadku, gdy wyszukiwarka była rozwinięta
            else
            {
                //Zmieniamy tekst przycisku.
                btnPrzepisySzukanie.Text = "Rozwiń zaawansowane wyszukiwanie";
                //Aktywujemy labelkę i textbox szybkiego wyszukiwania.
                lblSzybkieWyszukiwaniePrzepisy.Enabled = true;
                txbSzukajPrzepisy.Enabled = true;
                //Chowamy panel.
                panelWyszukiwaniePrzepisow.Visible = false;
                panelSzukajPrzepisy = false;
                //Pobieramy ID zaznaczonego przepisu.
                int selection = Convert.ToUInt16(gvListaPrzepisow.CurrentRow.Cells["ID"].Value.ToString());
                //Czyścimy gridview.
                gvListaPrzepisow.Rows.Clear();
                //Wypisujemy przepisy zaznaczając ponownie wcześniej zaznaczony przepis.
                wypiszListePrzepisow(selection);
            }
        }

        //Metoda wywoływana w momencie wciśnięcia przycisku zaawansowanego wyszukiwania:
        private void btnSzukajPrzepisy_Click(object sender, EventArgs e)
        {
            string query = null;
            //Sprawdzamy po kolei czy w danych textboxach została wpisana jakaś wartość, jeżeli tak to dajemy odpowiednie kryteria do zapytania.
            if (!(String.IsNullOrWhiteSpace(txbSzukajPrzepisuNazwa.Text)))
            {
                query += "NAZWA LIKE '%" + txbSzukajPrzepisuNazwa.Text + "%'";
            }
            if (!(String.IsNullOrWhiteSpace(txbSzukajPrzepisuOpis.Text)))
            {
                if (query != null)
                {
                    if (cbSzukajPrzepis1.SelectedIndex == 0) query += " OR ";
                    else query += " AND ";
                }
                query += "OPIS LIKE '%" + txbSzukajPrzepisuOpis.Text + "%'";
            }
            if (!(String.IsNullOrWhiteSpace(txbSzukajPrzepisuNotatki.Text)))
            {
                if (query != null)
                {
                    if (cbSzukajPrzepis2.SelectedIndex == 0) query += " OR ";
                    else query += " AND ";
                }
                query += "NOTATKI LIKE '%" + txbSzukajPrzepisuNotatki.Text + "%'";
            }
            //Jeżeli została wybrana jakaś wartość w checboxach i nie jest to wartość "pusta" to dodajemy kryteria do zapytania.
            if (cbSzukajPrzepisuKategorie.SelectedItem != null)
            {
                if (!String.IsNullOrWhiteSpace(cbSzukajPrzepisuKategorie.SelectedItem.ToString()))
                {
                    if (query != null)
                    {
                        if (cbSzukajPrzepis3.SelectedIndex == 0) query += " OR ";
                        else query += " AND ";
                    }
                    query += "P_NAZWA LIKE '" + cbSzukajPrzepisuKategorie.SelectedItem.ToString() + "'";
                }
            }
            if (cbSzukajPrzepisuIloscPorcji.SelectedItem != null)
            {
                if (!String.IsNullOrWhiteSpace(cbSzukajPrzepisuIloscPorcji.SelectedItem.ToString()))
                {
                    if (query != null)
                    {
                        if (cbSzukajPrzepis4.SelectedIndex == 0) query += " OR ";
                        else query += " AND ";
                    }
                    query += "ILOSC_PORCJI = " + cbSzukajPrzepisuIloscPorcji.SelectedItem;
                }
            }
            //Jeżeli zapytanie zostało utworzone to wywołujemy je i dodajemy wyniki do gridview.
            if (query != null)
            {
                SqlConnection conn = new SqlConnection(strSqlCon);
                conn.Open();
                SqlCommand comm = new SqlCommand("select NAZWA, ID_PRZEPISU from Przepisy p INNER JOIN Kategorie_Przepisy k on p.ID_KATEGORII = k.P_ID_KATEGORII WHERE " + query + " ORDER BY NAZWA ASC", conn);
                SqlDataReader reader = comm.ExecuteReader();
                gvListaPrzepisow.Rows.Clear();
                while (reader.Read())
                {
                    gvListaPrzepisow.Rows.Add(reader.GetString(0), reader.GetInt32(1).ToString());
                }
                reader.Close();
                conn.Close();
            }
            //Jeżeli zapytanie jest puste to pobieramy ID zaznaczonego przepisu, wyświetlamy wszystkie przepisy oraz ponownie zaznaczamy wcześniej zaznaczony przepis.
            else
            {
                int selection = Convert.ToUInt16(gvListaPrzepisow.CurrentRow.Cells["ID"].Value.ToString());
                gvListaPrzepisow.Rows.Clear();
                wypiszListePrzepisow(selection);
            }
        }

        //Metoda wywoływana w momencie przyciśnięcia myszki na gridview ze składniki danego przepisu.
        private void gvSkladniki_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                //Jeżeli wciśnięty przycisk to prawy przycisk myszy to wyświetlamy menu kontektsowe.
                if (e.Button == MouseButtons.Right)
                {
                    DataGridViewCell selectedCell = (sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex];
                    this.gvSkladniki.CurrentCell = selectedCell;
                    var pozycjaMyszki = gvSkladniki.PointToClient(Cursor.Position);
                    this.menuSkladnikiDlaPrzepisu.Show(gvSkladniki, pozycjaMyszki);
                }
            }
        }

        //Metoda wywoływana w momencie wybrania opcji "Informacje" z menu kontekstowane składników dla danego przepisu.
        private void informacjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Zmienamy aktualną zakładką na zakładkę ze składnikami.
            tabControl1.SelectedIndex = 1;
            //Pobieramy ID wybranego wcześniej składnika oraz zaznaczamy go na gridview.
            int selection = Convert.ToUInt16(gvSkladniki.CurrentRow.Cells["S_ID"].Value.ToString());
            wypiszListeSkladnikow(selection);
        }

        //Metoda wywoływana w momencie wciśnięcia guzika usuwania przepisu.
        private void btnUsunPrzepis_Click(object sender, EventArgs e)
        {
            
            //Otwieramy formę z potwierdzeniem operacji i jeżeli został wcisnięty przycisk "Tak" to usuwamy zaznaczony przepis oraz wszystkie powiązania z nim w bazie.
            //Po wykkonaniu operacji zaznaczamy przepis, który był o 1 pozycję wyżej od usuniętego.
            Potwierdzenie potwierdz = new Potwierdzenie();
            if (potwierdz.ShowDialog() == DialogResult.Yes)
            {
                int index = gvListaPrzepisow.CurrentRow.Cells["ID"].RowIndex;
                int selection = Convert.ToUInt16(gvListaPrzepisow.CurrentRow.Cells["ID"].Value.ToString());
                SqlConnection conn = new SqlConnection(strSqlCon);
                conn.Open();
                SqlCommand usunSkladniki = new SqlCommand("DELETE from Przepisy_Skladniki where L_ID_PRZEPISU = " + selection, conn);
                usunSkladniki.ExecuteNonQuery();
                SqlCommand usunPrzepis = new SqlCommand("DELETE from Przepisy where ID_PRZEPISU = " + selection, conn);
                usunPrzepis.ExecuteNonQuery();
                conn.Close();
                wypiszListePrzepisow(-1);
                if (index == 0) index++; 
                gvListaPrzepisow.CurrentCell = gvListaPrzepisow.Rows[index - 1].Cells[0];
            }
            potwierdz = null;
        }

        //Metoda wywoływana w momencie wciśnięcia guzika dodawania przepisu.
        //Wywołujemy formę dodawania przepisu używając do tego odpowiedniego konstruktora.
        private void btnDodajPrzepis_Click(object sender, EventArgs e)
        {
            int selection = Convert.ToUInt16(gvListaPrzepisow.CurrentRow.Cells["ID"].Value);
            DodajPrzepis dodaj = new DodajPrzepis(this, selection);
            dodaj.ShowDialog();
        }

        //Metoda wywoływana w momencie wciśnięcia guzika edytowania przepisu.
        //Wywołujemy formę dodawania przepisu używając do tego odpowiedniego konstruktora (w tym przypadku konstruktor służący do edycji przepisu).
        private void btnEdytujPrzepis_Click(object sender, EventArgs e)
        {
            int selection = Convert.ToUInt16(gvListaPrzepisow.CurrentRow.Cells["ID"].Value);
            DodajPrzepis edytuj = new DodajPrzepis(this, selection, true);
            edytuj.ShowDialog();
        }

        //Metoda wywoływana w momencie wciśnięcia guzika drukowania przepisu.
        private void btnDrukuj_Click(object sender, EventArgs e)
        {
            //Drukujemy przepis.
            printDocument = new PrintDocument();
            printDialog = new PrintDialog();

            printDialog.Document = printDocument;
            printDialog.PrinterSettings = printDocument.PrinterSettings;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
                printDocument.Print();
            }
        }

        //Metoda wywoływana w momencie drukowania przepisu.
        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            //Tworzymy pustą zmienną "składniki".
            string skladniki = "";
            //Znajdujemy po kolei wszystkie składniki dla danego przepisu oraz dodajemy do zmiennej w odpowiedniej formie.
            foreach (DataGridViewRow dgv in gvSkladniki.Rows)
            {
                if (String.IsNullOrWhiteSpace(dgv.Cells[1].Value.ToString()) && String.IsNullOrWhiteSpace(dgv.Cells[2].Value.ToString()))
                {
                    skladniki += dgv.Cells[0].Value.ToString() + "\n";
                }
                else
                {
                    skladniki += dgv.Cells[0].Value.ToString() + " - " + dgv.Cells[1].Value.ToString() + " " + dgv.Cells[2].Value.ToString() + "\n";
                }
            }
            //Tworzymy string, który wydrukujemy oraz dodajemy do niego wszystkie dane.
            string tekstDoWydrukowania = "Nazwa: {0}\n\nKategoria: {1}\n\nIlość porcji: {2}\n\nCzas przyrządzenia: {3}\n\nOpis: \n{4}\n\nNotatki: \n{5}\n\nSkładniki: \n{6}";
            string czas = txbCzasH.Text.ToString() + " h " + txbCzasM.Text.ToString() + " m";
            string tekst = string.Format(tekstDoWydrukowania, nazwaZaznaczonegoPrzepisu, txbPrzepisyKategoria.Text, txbIloscPorcji.Text, czas, txbOpis.Text, txbNotatki.Text, skladniki);
            //Drukujemy tekst używając czcionki Arial.
            using (Font font = new Font("Arial", 10))
            {
                g.DrawString(tekst, font, Brushes.Black, 50, 50);
            }
        }


        /* TAB SKŁADNIKI */

        //Metoda wypełniająca gridview składnikami z bazy danych.
        //Jako parametr przyjmuje ID składnika, który ma zostać zaznaczony po wypisaniu.
        public void wypiszListeSkladnikow(int selectionID)
        {
            SqlConnection conn = new SqlConnection(strSqlCon);
            SqlCommand comm2 = new SqlCommand("SELECT * from Skladniki ORDER BY NAZWA ASC", conn);
            SqlDataAdapter da = new SqlDataAdapter(comm2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvListaSkladnikow.Rows.Clear();

            //Dodajemy po kolei składniki z bazy:
            foreach (DataRow dr in dt.Rows)
            {
                gvListaSkladnikow.Rows.Add(dr["NAZWA"].ToString(), dr["ID_SKLADNIKA"].ToString());
            }

            //Jeżeli podane ID jest różne od -1 to znajdujemy odpowiedni składnik i zaznaczamy go.
            if (selectionID != -1)
            {
                int rowIndex = -1;
                foreach (DataGridViewRow row in gvListaSkladnikow.Rows)
                {
                    if (Convert.ToUInt16(row.Cells[1].Value) == selectionID)
                    {
                        rowIndex = row.Index;
                        break;
                    }
                }
                if (rowIndex != -1)
                {
                    gvListaSkladnikow.CurrentCell = gvListaSkladnikow.Rows[rowIndex].Cells[0];
                }
            }
            gvListaSkladnikow_SelectionChanged(null, null);
        }

        //Metoda wywoływana przy zmianie zaznaczonego składnika na gridview ze składnikami:
        private void gvListaSkladnikow_SelectionChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(strSqlCon);
            try
            {
                //Sczytujemy ID zaznaczonego składnika.
                int i = Convert.ToUInt16(gvListaSkladnikow.CurrentRow.Cells["LS_ID"].Value.ToString());
                conn.Open();
                //Wywołujemy metodę wypisującą przepisy dla danego składnika.
                znajdzPrzepisy(i, conn);
                //Znajdujemy w bazie składnik o danym ID.
                SqlCommand comm = new SqlCommand("SELECT * FROM Skladniki where ID_SKLADNIKA = " + i, conn);
                SqlDataAdapter da = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataRow dr = dt.Rows[0];
                //Znajdujemy nazwę kategorii wybranego składnika i wyświetlamy w labelce.
                int idKategorii = Convert.ToUInt16(dr["ID_KATEGORII"]);
                SqlCommand commNazwaKategorii = new SqlCommand("SELECT S_NAZWA FROM Kategorie_Skladniki where S_ID_KATEGORII = " + idKategorii, conn);
                //Pobieramy wszystkie dane o składniku i ustawiamy w odpowiednich labelkach.
                string nazwa = (string)commNazwaKategorii.ExecuteScalar();
                txbSkladnikiKategoria.Text = nazwa;
                txbSkladnikOpis.Text = dr["OPIS"].ToString();
                txbSkladnikUwagi.Text = dr["UWAGI"].ToString();
                zdjecieSkladnika.Image = null;
                //Pobieramy zdjęcie z bazy i ustawiamy w PictureBox.
                byte[] zdj = new byte[0];
                zdj = (byte[])dr["ZDJECIE"];
                zdjecieSkladnika.Image = null;
                if (zdj != null && zdj.Length > 0)
                {
                    MemoryStream stream = new MemoryStream(zdj);
                    zdjecieSkladnika.Image = Image.FromStream(stream);
                }
            }
            catch
            {
                //
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        //Metoda znajdująca przepisy dla danego składnika:
        private void znajdzPrzepisy(int i, SqlConnection conn)
        {
            //Znajdujemy wszystkie przepisy dla danego składnika.
            SqlCommand comm = new SqlCommand("SELECT * FROM Przepisy_Skladniki where L_ID_SKLADNIKA = " + i, conn);
            SqlDataAdapter da = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvPrzepisyDlaSkladnika.Rows.Clear();
            //Dla każdego znalezionego przepisu pobieramy dane i dodajemy nowe pozycje na gridview.
            foreach (DataRow dr in dt.Rows)
            {
                int x = Convert.ToUInt16(dr["L_ID_PRZEPISU"]);
                SqlCommand commNazwa = new SqlCommand("SELECT NAZWA FROM Przepisy where ID_PRZEPISU = " + x, conn);
                string nazwa = (string)commNazwa.ExecuteScalar();
                gvPrzepisyDlaSkladnika.Rows.Add(nazwa, x.ToString());
            }
        }

        private void btnSzukanieSkladnika_Click(object sender, EventArgs e)
        {
            if (panelSzukajSkladniki == false)
            {
                btnSzukanieSkladnika.Text = "Zwiń zaawansowane wyszukiwanie";
                lblSzybkieWyszukiwanieSkladniki.Enabled = false;
                txbSzukajSkladniki.Clear();
                txbSzukajSkladniki.Enabled = false;
                panelWyszukiwanieSkladnikow.Visible = true;
                panelSzukajSkladniki = true;
                SqlConnection conn = new SqlConnection(strSqlCon);
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT S_NAZWA FROM Kategorie_Skladniki ORDER BY S_NAZWA ASC", conn);
                SqlDataReader reader = comm.ExecuteReader();
                cbSzukajSkladnikaKategorie.Items.Clear();
                cbSzukajSkladnikaKategorie.Items.Add("");
                while (reader.Read())
                {
                    cbSzukajSkladnikaKategorie.Items.Add(reader.GetString(0));
                }
                reader.Close();
                conn.Close();
            }
            else
            {
                btnSzukanieSkladnika.Text = "Rozwiń zaawansowane wyszukiwanie";
                lblSzybkieWyszukiwanieSkladniki.Enabled = true;
                txbSzukajSkladniki.Enabled = true;
                panelWyszukiwanieSkladnikow.Visible = false;
                panelSzukajSkladniki = false;
                int selection = Convert.ToUInt16(gvListaSkladnikow.CurrentRow.Cells["LS_ID"].Value);
                gvListaSkladnikow.Rows.Clear();
                wypiszListeSkladnikow(selection);
            }
        }

        //Metoda wyszukająca składniki po zmianie wartości w textboxie wyszukiwania:
        private void txbSzukajSkladniki_TextChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(strSqlCon);
            //Znajdujemy w bazie wyniki odpowiadające wartości z textboxa:
            SqlCommand comm = new SqlCommand("SELECT * FROM Skladniki where NAZWA like @txtNazwa", conn);
            comm.Parameters.AddWithValue("txtNazwa", "%" + txbSzukajSkladniki.Text.ToString() + "%");
            SqlDataAdapter da = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            da.Fill(dt);
            //Czyścimy gridview.
            gvListaSkladnikow.Rows.Clear();
            //Wypisujemy wszystkie wyniki:
            foreach (DataRow dr in dt.Rows)
            {
                gvListaSkladnikow.Rows.Add(dr["NAZWA"].ToString(), dr["ID_SKLADNIKA"].ToString());
            }
        }

        private void btnSzukajSkladnikow_Click(object sender, EventArgs e)
        {
            string query = null;
            if (!(String.IsNullOrWhiteSpace(txbSzukajSkladnikaNazwa.Text)))
            {
                query += "NAZWA LIKE '%" + txbSzukajSkladnikaNazwa.Text + "%'";
            }
            if (!(String.IsNullOrWhiteSpace(txbSzukajSkladnikaOpis.Text)))
            {
                if (query != null)
                {
                    if (cbSzukajSkladnika1.SelectedIndex == 0) query += " OR ";
                    else query += " AND ";
                }
                query += "OPIS LIKE '%" + txbSzukajSkladnikaOpis.Text + "%'";
            }
            if (!(String.IsNullOrWhiteSpace(txbSzukajSkladnikaUwagi.Text)))
            {
                if (query != null)
                {
                    if (cbSzukajSkladnika2.SelectedIndex == 0) query += " OR ";
                    else query += " AND ";
                }
                query += "UWAGI LIKE '%" + txbSzukajSkladnikaUwagi.Text + "%'";
            }
            if (cbSzukajSkladnikaKategorie.SelectedItem != null)
            {
                if (!String.IsNullOrWhiteSpace(cbSzukajSkladnikaKategorie.SelectedItem.ToString()))
                {
                    if (query != null)
                    {
                        if (cbSzukajSkladnika3.SelectedIndex == 0) query += " OR ";
                        else query += " AND ";
                    }
                    query += "S_NAZWA = '" + cbSzukajSkladnikaKategorie.SelectedItem.ToString() + "'";
                }
            }
            if (query != null)
            {
                SqlConnection conn = new SqlConnection(strSqlCon);
                conn.Open();
                SqlCommand comm = new SqlCommand("select NAZWA, ID_SKLADNIKA from Skladniki s INNER JOIN Kategorie_Skladniki k on s.ID_KATEGORII = k.S_ID_KATEGORII WHERE " + query + " ORDER BY NAZWA ASC", conn);
                SqlDataReader reader = comm.ExecuteReader();
                gvListaSkladnikow.Rows.Clear();
                while (reader.Read())
                {
                    gvListaSkladnikow.Rows.Add(reader.GetString(0), reader.GetInt32(1).ToString());
                }
                reader.Close();
                conn.Close();
            }
            else
            {
                int selection = Convert.ToUInt16(gvListaSkladnikow.CurrentRow.Cells["LS_ID"].Value);
                gvListaSkladnikow.Rows.Clear();
                wypiszListeSkladnikow(selection);
            }
        }

        private void gvPrzepisyDlaSkladnika_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    DataGridViewCell selectedCell = (sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex];
                    this.gvPrzepisyDlaSkladnika.CurrentCell = selectedCell;
                    var pozycjaMyszki = gvPrzepisyDlaSkladnika.PointToClient(Cursor.Position);
                    this.menuPrzepisyDlaSkladnika.Show(gvPrzepisyDlaSkladnika, pozycjaMyszki);
                }
            }
        }

        private void menuPrzepisyDlaSkladnikaPokaz_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            int selection = Convert.ToUInt16(gvPrzepisyDlaSkladnika.CurrentRow.Cells["SP_ID"].Value.ToString());
            wypiszListePrzepisow(selection);
        }

        private void btnDodajSkladnik_Click(object sender, EventArgs e)
        {
            int selection = Convert.ToUInt16(gvListaSkladnikow.CurrentRow.Cells["LS_ID"].Value);
            DodajSkladnik nowySkladnik = new DodajSkladnik(this, selection);
            nowySkladnik.ShowDialog();
        }

        //Metoda usuwająca zaznaczony składnik
        private void btnUsunSkladnik_Click(object sender, EventArgs e)
        {
            //Sczytujemy ID zaznaczonego składnika
            int selection = Convert.ToUInt16(gvListaSkladnikow.CurrentRow.Cells["LS_ID"].Value.ToString());
            SqlConnection conn = new SqlConnection(strSqlCon);
            conn.Open();
            //Sprawdzamy w bazie czy nie występują jakieś powiązania ze składnikiem
            SqlCommand cmdLiczba = new SqlCommand("SELECT COUNT(L_ID_SKLADNIKA) from Przepisy_Skladniki where L_ID_SKLADNIKA = " + selection, conn);
            int liczba = (int)cmdLiczba.ExecuteScalar();
            //Jeżeli liczba powiązań jest większa od 0 to wyświetlamy messagebox z odpowiednim komunikatem
            if (liczba > 0)
            {
                MessageBox.Show("Nie możesz usunąć składnika, który jest używany. Najpierw musisz usunąć wszelkie powiązania.",
                "Błąd",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }
            //Jeżeli liczba powiązań jest równa 0 to wyświetlamy formę z prośbą o potwierdzenie
            else if (liczba == 0)
            {
                Potwierdzenie potwierdz = new Potwierdzenie("Jesteś pewien, że chcesz usunąć ten składnik?");


                if (potwierdz.ShowDialog() == DialogResult.Yes)
                {
                    int index = gvListaSkladnikow.CurrentRow.Cells["LS_ID"].RowIndex;
                    SqlCommand comm = new SqlCommand("DELETE FROM Skladniki WHERE ID_SKLADNIKA = " + selection, conn);
                    comm.ExecuteNonQuery();
                    wypiszListeSkladnikow(-1);
                    if (index == 0) index++;
                    gvListaSkladnikow.CurrentCell = gvListaSkladnikow.Rows[index - 1].Cells[0];
                    gvListaSkladnikow_SelectionChanged(null, null);
                }
                potwierdz = null;
            }
            conn.Close();

        }

        //Po kliknięciu na button odczytujemy id zaznaczone składnika oraz otwieramy formę edytującą dany składnik
        private void btnEdytujSkladnik_Click(object sender, EventArgs e)
        {
            int selection = Convert.ToUInt16(gvListaSkladnikow.CurrentRow.Cells["LS_ID"].Value);
            DodajSkladnik edytujSkladnik = new DodajSkladnik(this, selection, true);
            edytujSkladnik.ShowDialog();
        }

        //Po kliknięciu na button odczytujemy nazwę zaznaczonego składnika oraz otwieramy przeglądarkę z wyszukiwaniem w google
        private void btnSzukajGoogle_Click(object sender, EventArgs e)
        {
            string selection = gvListaSkladnikow.CurrentRow.Cells["LS_NAZWA"].Value.ToString();
            System.Diagnostics.Process.Start("http://www.google.pl/search?q=" + selection + "&ie=utf-8&oe=utf-8");
        }

        //Po kliknięciu na button odczytujemy nazwę zaznaczonego składnika oraz otwieramy przeglądarkę z wyszukiwaniew na Alma24
        private void btnAlma_Click(object sender, EventArgs e)
        {
            string selection = gvListaSkladnikow.CurrentRow.Cells["LS_NAZWA"].Value.ToString();
            System.Diagnostics.Process.Start("http://alma24.pl/szukaj?search=" + selection);
        }

        /* RESZTA */
        
        #region ogolne
        //Po aktywacji formy pobieramy ID z aktualnie zaznaczonej pozycji a następnie odświeżamy listę elementów 
        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                int selection = Convert.ToUInt16(gvListaPrzepisow.CurrentRow.Cells["ID"].Value);
                wypiszListePrzepisow(selection);
            }
            else
            {
                int selection = Convert.ToUInt16(gvListaSkladnikow.CurrentRow.Cells["LS_ID"].Value);
                wypiszListeSkladnikow(selection);
            }
        }

        //Otwieranie formy zarządzającej kategoriami po kliknięciu w menu
        private void zarządzajKategoriamiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZarzadzanieKategoriami kategorie = new ZarzadzanieKategoriami(null, false);
            kategorie.ShowDialog();
        }

        //Otwieranie formy zarządzającej jednostkami po kliknięciu w menu
        private void zarządzajJednostkamiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZarzadzanieJednostkami jednostki = new ZarzadzanieJednostkami();
            jednostki.ShowDialog();
        }

        //Otwieranie formy z informacji po kliknięciu w menu
        private void informacjeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Informacje informacje = new Informacje();
            informacje.ShowDialog();
        }

        //Zamykanie aplikacji z menu
        private void zamknijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion 

    }
}
