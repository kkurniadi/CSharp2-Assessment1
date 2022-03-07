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

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddTerm();
            TextBoxName.Focus();
        }

        private void AddTerm()
        {
            if (pointer < entries)
            {
                if (!string.IsNullOrEmpty(TextBoxName.Text) && !string.IsNullOrEmpty(TextBoxCategory.Text)
                    && !string.IsNullOrEmpty(TextBoxStructure.Text) && !string.IsNullOrEmpty(TextBoxDefinition.Text))
                {
                    try
                    {
                        wikiData[pointer, 0] = TextBoxName.Text;
                        wikiData[pointer, 1] = TextBoxCategory.Text;
                        wikiData[pointer, 2] = TextBoxStructure.Text;
                        wikiData[pointer, 3] = TextBoxDefinition.Text;
                        pointer++;
                        ClearBoxes();
                    }
                    catch
                    {
                        MessageBox.Show("Could not add entry");
                    }
                }
                else
                {
                    MessageBox.Show("Please make sure all boxes are filled");
                }
            }
            DisplayNameCat();
        }

        private void ClearBoxes()
        {
            TextBoxName.Clear();
            TextBoxCategory.Clear();
            TextBoxStructure.Clear();
            TextBoxDefinition.Clear();
        }
    }
}
