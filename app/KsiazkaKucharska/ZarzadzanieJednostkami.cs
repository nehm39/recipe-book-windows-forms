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
    public partial class ZarzadzanieJednostkami : Form
    {
        private string strSqlCon = ConfigurationManager.ConnectionStrings["KsiazkaKucharskaConnectionString"].ConnectionString;
        public ZarzadzanieJednostkami()
        {
            InitializeComponent();
            wypelnijListe(0, null);
        }

        //Metoda wypełniająca checkboxa z kategoriami.
        public void wypelnijListe(int index, string item)
        {
            //Czyścimy obecną listę.
            cbListaJednostek.Items.Clear();
            SqlConnection conn = new SqlConnection(strSqlCon);
            conn.Open();
            SqlCommand comm = new SqlCommand("SELECT NAZWA FROM Jednostki ORDER BY NAZWA ASC", conn);
            SqlDataReader reader = comm.ExecuteReader();
            //Wypełniamy checkboxa listą jednostek.
            while (reader.Read())
            {
                cbListaJednostek.Items.Add(reader.GetString(0));
            }
            //Jeżeli została przekazana jednostka to ją zaznaczamy.
            if (item != null) cbListaJednostek.SelectedItem = item;
            //W przeciwnym wypadku zaznaczamy przekazany index.
            else cbListaJednostek.SelectedIndex = index;
            conn.Close();
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            DodajJednostke dodaj = new DodajJednostke(this);
            dodaj.ShowDialog();
        }

        private void btnEdytuj_Click(object sender, EventArgs e)
        {
            string selection = cbListaJednostek.SelectedItem.ToString();
            DodajJednostke edytuj = new DodajJednostke(this, selection);
            edytuj.ShowDialog();
        }

        private void btnUsun_Click(object sender, EventArgs e)
        {
            int selected = cbListaJednostek.SelectedIndex;
            string selection = cbListaJednostek.SelectedItem.ToString();
            SqlConnection conn = new SqlConnection(strSqlCon);
            conn.Open();
            SqlCommand findId = new SqlCommand("SELECT ID_JEDNOSTKI from Jednostki WHERE NAZWA = '" + selection + "'", conn);
            int id = (int)findId.ExecuteScalar();
            //Sprawdzamy czy jednostka, którą chcemy usunąć ma jakieś powiązania w bazie.
            SqlCommand cmdLiczba = new SqlCommand("SELECT COUNT(ID_JEDNOSTKI) from Przepisy_Skladniki where ID_JEDNOSTKI = " + id, conn);
            int liczba = (int)cmdLiczba.ExecuteScalar();
            //Jeżeli liczba powiązań jest większa niż 0 to wyświetlamy błąd.
            if (liczba > 0)
            {
                MessageBox.Show("Nie możesz usunąć jednostki, która jest używana. Najpierw musisz usunąć wszelkie powiązania.",
                "Błąd",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }
            else
            {
                //Usuwamy jednostkę i odświeżamy listę zaznaczająć element o 1 wyżej na liście.
                SqlCommand cmdDelete = new SqlCommand("DELETE FROM Jednostki WHERE ID_JEDNOSTKI = " + id, conn);
                cmdDelete.ExecuteNonQuery();
                if (selected == 0) selected++;
                wypelnijListe(selected-1, null);
            }
            conn.Close();
        }
    }
}
