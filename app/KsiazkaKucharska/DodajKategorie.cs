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
    public partial class DodajKategorie : Form
    {
        private bool isEdit;
        private bool isPrzepis;
        private string nazwa = null;
        private ZarzadzanieKategoriami callForm = null;
        private string strSqlCon = ConfigurationManager.ConnectionStrings["KsiazkaKucharskaConnectionString"].ConnectionString;

        //Konstuktor służący do dodawania kategorii.
        public DodajKategorie(Form cForm, bool przepis)
        {
            callForm = cForm as ZarzadzanieKategoriami;
            InitializeComponent();
            isEdit = false;
            isPrzepis = przepis;
        }

        //Konstuktor służący do edytowania kategorii.
        public DodajKategorie(Form cForm, string nazwa, bool przepis)
        {
            callForm = cForm as ZarzadzanieKategoriami;
            InitializeComponent();
            this.Text = "Edytuj kategorię";
            btnZapisz.Text = "Zapisz";
            txbNazwa.Text = nazwa;
            isEdit = true;
            isPrzepis = przepis;
            this.nazwa = nazwa;
        }

        private void btnZapisz_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(strSqlCon);
            conn.Open();
            //Sprawdzamy czy textbox nie jest pusty.
            if (!(String.IsNullOrWhiteSpace(txbNazwa.Text.ToString())))
            {
                //Jeżeli edytowana kategoria to kategoria przepisu.
                if (isPrzepis)
                {
                    //Sprawdzamy czy w bazie nie ma już takiej kategorii przepisu.
                    //Jeżeli nie ma to wartość idPrzepisu wyniesie -1.
                    SqlCommand spr = new SqlCommand("SELECT P_ID_KATEGORII FROM Kategorie_Przepisy WHERE P_NAZWA = '" + txbNazwa.Text.ToString() + "'", conn);
                    int idPrzepisu;
                    try
                    {
                        idPrzepisu = (int)spr.ExecuteScalar();
                    }
                    catch
                    {
                        idPrzepisu = -1;
                    }
                    //Jeżeli kategoria jest edytowana:
                    if (isEdit)
                    {
                        //Sprawdzamy czy w bazie nie ma takiej kategorii przepisu, a jeżeli jest to czy jest to kategoria, którą właśnie edytujemy.
                        if ((idPrzepisu == -1) || (idPrzepisu != -1 && txbNazwa.Text.ToString().Equals(nazwa)))
                        {
                            //Edytujemy kategorię oraz odświeżamy listę na formie ZarzadanieKategoriami.
                            SqlCommand cmd = new SqlCommand("UPDATE Kategorie_Przepisy SET P_NAZWA = '" + txbNazwa.Text.ToString() + "' WHERE P_NAZWA = '" + nazwa + "'", conn);
                            cmd.ExecuteNonQuery();
                            this.callForm.wypelnijKategoriePrzepisow(txbNazwa.Text.ToString());
                            this.FindForm().Close();
                        }
                        else duplikat();
                    }
                    //Jeżeli kategoria jest dodawana:
                    else
                    {
                        if (idPrzepisu == -1)
                        {
                            //Dodajemy nową kategorię oraz odświeżamy listę na formie ZarzadanieKategoriami.
                            SqlCommand cmd = new SqlCommand("INSERT into Kategorie_Przepisy VALUES ('" + txbNazwa.Text.ToString() + "'); SELECT CAST(scope_identity() AS int)", conn);
                            int noweId = (int)cmd.ExecuteScalar();
                            SqlCommand cmdNazwaKategorii = new SqlCommand("SELECT P_NAZWA FROM Kategorie_Przepisy WHERE P_ID_KATEGORII = " + noweId, conn);
                            string nowaNazwa = (string)cmdNazwaKategorii.ExecuteScalar();
                            this.callForm.wypelnijKategoriePrzepisow(nowaNazwa);
                            this.FindForm().Close();
                        }
                        else duplikat();
                    }
                }
                else
                {
                    //Sprawdzamy czy w bazie nie ma już takiej kategorii składnika.
                    //Jeżeli nie ma to wartość idSkladnika wyniesie -1.
                    SqlCommand spr = new SqlCommand("SELECT S_ID_KATEGORII FROM Kategorie_Skladniki WHERE S_NAZWA = '" + txbNazwa.Text.ToString() + "'", conn);
                    int idSkladnika;
                    try
                    {
                        idSkladnika = (int)spr.ExecuteScalar();
                    }
                    catch
                    {
                        idSkladnika = -1;
                    }
                    if (isEdit)
                    {
                        //Sprawdzamy czy w bazie nie ma takiej kategorii składnika, a jeżeli jest to czy jest to kategoria, którą właśnie edytujemy.
                        if ((idSkladnika == -1) || (idSkladnika != -1 && txbNazwa.Text.ToString().Equals(nazwa)))
                        {
                            SqlCommand cmd = new SqlCommand("UPDATE Kategorie_Skladniki SET S_NAZWA = '" + txbNazwa.Text.ToString() + "' WHERE S_NAZWA = '" + nazwa + "'", conn);
                            cmd.ExecuteNonQuery();
                            this.callForm.wypelnijKategorieSkladnikow(txbNazwa.Text.ToString());
                            this.FindForm().Close();
                        }
                        else duplikat();
                    }
                    else
                    {
                        if (idSkladnika == -1)
                        {
                            SqlCommand cmd = new SqlCommand("INSERT into Kategorie_Skladniki VALUES ('" + txbNazwa.Text.ToString() + "'); SELECT CAST(scope_identity() AS int)", conn);
                            int noweId = (int)cmd.ExecuteScalar();
                            SqlCommand cmdNazwaKategorii = new SqlCommand("SELECT S_NAZWA FROM Kategorie_Skladniki WHERE S_ID_KATEGORII = " + noweId, conn);
                            string nowaNazwa = (string)cmdNazwaKategorii.ExecuteScalar();
                            this.callForm.wypelnijKategorieSkladnikow(nowaNazwa);
                            this.FindForm().Close();
                        }
                        else duplikat();
                    }
                }

            }
            else
            {
                MessageBox.Show("Pole z nazwą nie może być puste.",
                "Błąd",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }
            conn.Close();
        }

        private void duplikat()
        {
                MessageBox.Show("Istnieje już taka kategoria.",
                "Błąd",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
        }
    }
}
