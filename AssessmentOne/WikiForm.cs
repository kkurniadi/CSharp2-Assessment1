using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// Kirsten Kurniadi, ID: 30045816
namespace AssessmentOne
{
    public partial class WikiForm : Form
    {
        public WikiForm()
        {
            InitializeComponent();
        }
        static int entries = 12;
        static int attributes = 4;
        string defaultFileName = "definitions.dat";
        int pointer = 0;
        string[,] wikiData = new string[entries, attributes];

        private void DisplayNameCat()
        {
            ListBoxDisplay.Items.Clear();
            for (int x = 0; x < pointer; x++)
            {
                string term = wikiData[x, 0] + "\t" + wikiData[x, 1];
                ListBoxDisplay.Items.Add(term);
            }
        }
    }
}
