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
        int pointer = 0;
        string[,] wikiData = new string[entries, attributes];

        private void DisplayData()
        {
            listBoxDisplay.Items.Clear();
            for (int x = 0; x < entries; x++)
            {
                string term = wikiData[x, 0];
                listBoxDisplay.Items.Add(term);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {

        }

        private void AddTerm()
        {
            if (pointer < entries)
            {
                try
                {
                    wikiData[pointer, 0] = textBoxName.Text;
                    wikiData[pointer, 1] = textBoxCategory.Text;
                    wikiData[pointer, 2] = textBoxStructure.Text;
                    wikiData[pointer, 3] = textBoxDefinition.Text;
                    pointer++;
                }
                catch
                {
                    MessageBox.Show("Nobody wants any more data structures to define");
                }
            }
        }
    }
}
