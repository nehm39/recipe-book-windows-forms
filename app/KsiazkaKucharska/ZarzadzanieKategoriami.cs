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
    public partial class ZarzadzanieKategoriami : Form
    {
        private string strSqlCon = ConfigurationManager.ConnectionStrings["KsiazkaKucharskaConnectionString"].ConnectionString;
        private bool isPrzepis;

        public ZarzadzanieKategoriami(string kategoria, bool przepis)
        {
            InitializeComponent();
            //Zaznaczamy odpowiedni rodzaj kategorii.
            if (przepis)
            {
                cbRodzajeKategorii.SelectedIndex = 1;
            }
            else
            {
                cbRodzajeKategorii.SelectedIndex = 0;
            }

            //Jeżeli została przekazana jakaś kategoria to zaznaczamy ją.
            if (kategoria != null)
            {
                cbListaKategorii.SelectedItem = kategoria;
            }
        }

        //Metoda wypełniająca checkbox kategoriami przepisów.
        public void wypelnijKategoriePrzepisow(string select)
        {
            //Czyścimy obecną listę.
            cbListaKategorii.Items.Clear();
            SqlConnection conn = new SqlConnection(strSqlCon);
            conn.Open();
            SqlCommand comm = new SqlCommand("SELECT DISTINCT P_NAZWA FROM Kategorie_Przepisy ORDER BY P_NAZWA ASC", conn);
            SqlDataReader reader = comm.ExecuteReader();
            //Wypełniamy checkboxa listą kategorii.
            while (reader.Read())
            {
                cbListaKategorii.Items.Add(reader.GetString(0));
            }
            reader.Close();
            conn.Close();
            //Jeżeli została przekazana kategoria to ją zaznaczamy.
            if (select != null) cbListaKategorii.SelectedItem = select;
            //W przeciwnym wypadku wybieramy pierwszą pozycję.
            else cbListaKategorii.SelectedIndex = 0;
        }

        //Metoda wypełniająca checkbox kategoriami składników.
        public void wypelnijKategorieSkladnikow(string select)
        {
            //Czyścimy obecną listę.
            cbListaKategorii.Items.Clear();
            SqlConnection conn = new SqlConnection(strSqlCon);
            conn.Open();
            SqlCommand comm = new SqlCommand("SELECT DISTINCT S_NAZWA FROM Kategorie_Skladniki ORDER BY S_NAZWA ASC", conn);
            SqlDataReader reader = comm.ExecuteReader();
            //Wypełniamy checkboxa listą kategorii.
            while (reader.Read())
            {
                cbListaKategorii.Items.Add(reader.GetString(0));
            }
            reader.Close();
            conn.Close();
            //Jeżeli została przekazana kategoria to ją zaznaczamy.
            if (select != null) cbListaKategorii.SelectedItem = select;
            //W przeciwnym wypadku wybieramy pierwszą pozycję.
            else cbListaKategorii.SelectedIndex = 0;
        }

        private void cbRodzajeKategorii_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Jeżeli zostałą wybrana kategoria składników to wywołujemy odpowiednią metodę.
            if (cbRodzajeKategorii.SelectedIndex == 0)
            {
                wypelnijKategorieSkladnikow(null);
                isPrzepis = false;
            }
            //Jeżeli zostałą wybrana kategoria przepisów to wywołujemy odpowiednią metodę.
            else
            {
                wypelnijKategoriePrzepisow(null);
                isPrzepis = true;
            }
            //Zaznaczamy pierwszą kategorię.
            cbListaKategorii.SelectedIndex = 0;
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            //Wyświetlamy formę dodającą nową kategorię.
            DodajKategorie dodajKategorie = new DodajKategorie(this, isPrzepis);
            dodajKategorie.ShowDialog();
        }

        private void btnUsun_Click(object sender, EventArgs e)
        {
            string selectionString = cbListaKategorii.SelectedItem.ToString();
            SqlConnection conn = new SqlConnection(strSqlCon);
            conn.Open();
            //Znajdujemy w bazie zaznaczoną kategorię i pobieramy liczbę powiązań w bazie.
            SqlCommand findId;
            if (isPrzepis) findId = new SqlCommand("SELECT P_ID_KATEGORII from Kategorie_Przepisy WHERE P_NAZWA = '" + selectionString + "'", conn);
            else findId = new SqlCommand("SELECT S_ID_KATEGORII from Kategorie_Skladniki WHERE S_NAZWA = '" + selectionString + "'", conn);
            int selection = (int)findId.ExecuteScalar();
            SqlCommand cmdLiczba;
            if (isPrzepis) cmdLiczba = new SqlCommand("SELECT COUNT(ID_KATEGORII) from Przepisy where ID_KATEGORII = " + selection, conn);
            else cmdLiczba = new SqlCommand("SELECT COUNT(ID_KATEGORII) from Skladniki where ID_KATEGORII = " + selection, conn);
            int liczba = (int)cmdLiczba.ExecuteScalar();
            //Jeżeli liczba powiązań jest większa od 0 to wyświetlamy błąd.
            if (liczba > 0)
            {
                MessageBox.Show("Nie możesz usunąć kategorii, która jest używana. Najpierw musisz usunąć wszelkie powiązania.",
                "Błąd",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }
            else if (liczba == 0)
            {
                //Usuwamy zaznaczoną kategorię.
                SqlCommand comm;
                if (isPrzepis) comm = new SqlCommand("DELETE FROM Kategorie_Przepisy WHERE P_ID_KATEGORII = " + selection, conn);
                else comm = new SqlCommand("DELETE FROM Kategorie_Skladniki WHERE S_ID_KATEGORII = " + selection, conn);
                comm.ExecuteNonQuery();
                //Wypełniamy ponownie listę kategorii.
                if (isPrzepis) wypelnijKategoriePrzepisow(null);
                else wypelnijKategorieSkladnikow(null);
            }
            conn.Close();
        }

        private void btnEdytuj_Click(object sender, EventArgs e)
        {
            //Wywołujemy formę edytującą zaznaczoną kategorię.
            string selected = cbListaKategorii.SelectedItem.ToString();
            DodajKategorie edytujKategorie = new DodajKategorie(this, selected, isPrzepis);
            edytujKategorie.ShowDialog();
        }
    }
}
