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
    public partial class EdytujSkladnikPrzepisu : Form
    {
        private DodajPrzepis callForm = null;
        private int rowIndex;
        private string nazwaEdytowanego;
        private string strSqlCon = ConfigurationManager.ConnectionStrings["KsiazkaKucharskaConnectionString"].ConnectionString;

        public EdytujSkladnikPrzepisu(Form cForm, int rowIndex, string nazwa, string ilosc, string jednostka)
        {
            InitializeComponent();
            wypelnijSkladniki();
            cbSkladniki.SelectedItem = nazwa;
            wypelnijJednostki();
            cbJednostki.SelectedItem = jednostka;
            txbIlosc.Text = ilosc;
            this.rowIndex = rowIndex;
            nazwaEdytowanego = nazwa;
            callForm = cForm as DodajPrzepis;
        }

        //Metoda wypełniająca chceckboxa listą dostępnych składników.
        private void wypelnijSkladniki()
        {
            cbSkladniki.Items.Clear();
            SqlConnection conn = new SqlConnection(strSqlCon);
            conn.Open();
            SqlCommand comm = new SqlCommand("SELECT NAZWA FROM Skladniki ORDER BY NAZWA ASC", conn);
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                cbSkladniki.Items.Add(reader.GetString(0));
            }
            reader.Close();
            conn.Close();
        }

        //Metoda wypełniająca chceckboxa listą dostępnych jednostek.
        private void wypelnijJednostki()
        {
            cbJednostki.Items.Clear();
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

        //Metoda sprawdza czy wprowadzone dane z klawiatury są cyframi lub kropkami.
        //Sprawdza także czy w textboxie nie ma już kropki.
        private void txbIlosc_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (ch == 46 && txbIlosc.Text.IndexOf('.') > 0)
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

        private void btnZapisz_Click(object sender, EventArgs e)
        {
            //Jeżeli taki składnik nie istnieje już w bazie (i nie jest to składnik edytowany) to aktualizujemy rekord w bazie.
            string nazwa = cbSkladniki.SelectedItem.ToString();
            if (!callForm.skladnikIstnieje(nazwa) || nazwa.Equals(nazwaEdytowanego))
            {
                string ilosc = txbIlosc.Text.ToString();
                string jednostka;
                try
                {
                    jednostka = cbJednostki.SelectedItem.ToString();
                }
                catch (NullReferenceException)
                {
                    jednostka = "";
                }
                SqlConnection conn = new SqlConnection(strSqlCon);
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT ID_SKLADNIKA FROM Skladniki WHERE NAZWA = '" + nazwa + "'", conn);
                int idSkladnika = (int)comm.ExecuteScalar();
                callForm.edytujSkladnik(rowIndex, nazwa, ilosc, jednostka, idSkladnika);
                this.FindForm().Close();
            }
            //W przeciwnym wypadku wyświetlamy błąd.
            else
            {
                MessageBox.Show("Taki składnik już został dodany do tego przepisu.",
                "Błąd",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }
        }
    }
}
