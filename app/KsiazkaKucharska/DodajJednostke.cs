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
    public partial class DodajJednostke : Form
    {
        private ZarzadzanieJednostkami callForm = null;
        private bool isEdit;
        private string nazwa;
        private string strSqlCon = ConfigurationManager.ConnectionStrings["KsiazkaKucharskaConnectionString"].ConnectionString;

        //Konstuktor służący do dodawania jednostki.
        public DodajJednostke(Form cForm)
        {
            callForm = cForm as ZarzadzanieJednostkami;
            isEdit = false;
            InitializeComponent();
        }

        //Konstuktor służący do edytowania jednostki.
        public DodajJednostke(Form cForm, string nazwa)
        {
            callForm = cForm as ZarzadzanieJednostkami;
            isEdit = true;
            InitializeComponent();
            txbNazwa.Text = nazwa;
            this.nazwa = nazwa;
            this.Text = "Edytuj jednostkę";
            this.btnZapisz.Text = "Zapisz";
        }

        private void btnZapisz_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(strSqlCon);
            conn.Open();
            //Sprawdzamy czy textbox nie jest pusty.
            if (!(String.IsNullOrWhiteSpace(txbNazwa.Text.ToString())))
            {
                //Sprawdzamy czy w bazie nie ma już takiego składnika.
                //Jeżeli nie ma to wartość idSkladnika wyniesie -1.
                SqlCommand spr = new SqlCommand("SELECT ID_JEDNOSTKI FROM Jednostki WHERE NAZWA = '" + txbNazwa.Text.ToString() + "'", conn);
                int idSkladnika;
                try
                {
                    idSkladnika = (int)spr.ExecuteScalar();
                }
                catch
                {
                    idSkladnika = -1;
                }
                //Sprawdzamy czy w bazie nie ma tego składnika, a jeżeli jest to czy jest to składnik, który właśnie edytujemy.
                if (idSkladnika == -1 || (idSkladnika != -1 && txbNazwa.Text.ToString().Equals(nazwa)))
                {
                    //Jeżeli składnik jest edytowany:
                    if (isEdit)
                    {
                        //Edytujemy nazwę składnika oraz odświeżamy listę na formie ZarzadanieJednostkami.
                        SqlCommand cmd = new SqlCommand("UPDATE Jednostki SET NAZWA = '" + txbNazwa.Text.ToString() + "' WHERE NAZWA = '" + nazwa + "'", conn);
                        cmd.ExecuteNonQuery();
                        this.callForm.wypelnijListe(0, txbNazwa.Text.ToString());
                    }
                    //Jeżeli składnik jest dodawany:
                    else
                    {
                        //Dodajemy nowy składnik oraz odświeżamy listę na formie ZarzadanieJednostkami.
                        SqlCommand cmd = new SqlCommand("INSERT into Jednostki VALUES ('" + txbNazwa.Text.ToString() + "'); SELECT CAST(scope_identity() AS int)", conn);
                        int noweId = (int)cmd.ExecuteScalar();
                        SqlCommand cmdNazwaJednostki = new SqlCommand("SELECT NAZWA FROM Jednostki WHERE ID_JEDNOSTKI = " + noweId, conn);
                        string nowaNazwa = (string)cmdNazwaJednostki.ExecuteScalar();
                        this.callForm.wypelnijListe(0, nowaNazwa);
                    }
                    this.FindForm().Close();
                }
                else
                {
                    MessageBox.Show("Istnieje już taka jednostka.",
                    "Błąd",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
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
        }
    }
}
