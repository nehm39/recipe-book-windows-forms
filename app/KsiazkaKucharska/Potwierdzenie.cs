using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KsiazkaKucharska
{
    public partial class Potwierdzenie : Form
    {
        public Potwierdzenie()
        {
            InitializeComponent();
        }
        public Potwierdzenie(string tekst)
        {
            InitializeComponent();
            label1.Text = tekst;
        }
    }
}
