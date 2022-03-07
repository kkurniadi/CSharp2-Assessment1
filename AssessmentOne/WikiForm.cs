using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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

        private void SortArray()
        {
            for (int x = 1; x < pointer; x++)
            {
                for (int i = 0; i < pointer - 1; i++)
                {
                    if (!(string.IsNullOrEmpty(wikiData[i + 1, 0])))
                    {
                        if (string.Compare(wikiData[i, 0], wikiData[i + 1, 0]) > 0)
                        {
                            Swap(i);
                        }
                    }
                }
            }
        }

        private void Swap(int i)
        {
            string temp;
            for (int j = 0; j < attributes; j++)
            {
                temp = wikiData[i, j];
                wikiData[i, j] = wikiData[i + 1, j];
                wikiData[i + 1, j] = temp;
            }
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            int startIndex = -1;
            int finalIndex = pointer;
            bool found = false;
            int foundIndex = -1;
            while (!false && !((finalIndex - startIndex) <= 1))
            {
                int newIndex = (finalIndex + startIndex) / 2;
                if (string.Compare(wikiData[newIndex, 0], TextBoxSearch.Text) == 0)
                {
                    foundIndex = newIndex;
                    found = true;
                    break;
                }
                else
                {
                    if (string.Compare(wikiData[newIndex, 0], TextBoxSearch.Text) == 1)
                        finalIndex = newIndex;
                    else
                        startIndex = newIndex;
                }
            }
            if (found)
            {
                ListBoxDisplay.SelectedIndex = foundIndex;
                DisplayForOne(foundIndex);
            }
            else
            {
                MessageBox.Show("Not found");
                ListBoxDisplay.ClearSelected();
                ClearBoxes();
                TextBoxSearch.Clear();
            }
        }

        private void DisplayForOne(int x)
        {
            ClearBoxes();
            TextBoxName.Text = wikiData[x, 0];
            TextBoxCategory.Text = wikiData[x, 1];
            TextBoxStructure.Text = wikiData[x, 2];
            TextBoxDefinition.Text = wikiData[x, 3];
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveData = new SaveFileDialog
            {
                Filter = "DAT Files|*.dat",
                Title = "Save file..."
            };
            saveData.ShowDialog();
            string fileName = saveData.FileName;
            if (saveData.FileName != "")
            {
                SaveFile(fileName);
            }
            else
            {
                SaveFile(defaultFileName);
            }
        }

        private void SaveFile(string saveFileName)
        {
            try
            {
                using (Stream stream = File.Open(saveFileName, FileMode.Create))
                {
                    SortArray();
                    BinaryFormatter bin = new BinaryFormatter();
                    for (int x = 0; x < pointer; x++)
                    {
                        for (int y = 0; y < attributes; y++)
                        {
                            bin.Serialize(stream, wikiData[x, y]);
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
