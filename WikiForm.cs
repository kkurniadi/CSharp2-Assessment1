using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            ClearBoxes();
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
            for (int i = 0; i < pointer - 1; i++)
            {
                if (!(string.IsNullOrEmpty(wikiData[i + 1, 0])))
                {
                    if (string.Compare(wikiData[i, 0], wikiData[i + 1, 0]) == 1)
                    {
                        Swap(i);
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
                DisplayForOne(foundIndex);
            }
            else
            {
                MessageBox.Show("Not found");
                TextBoxSearch.Clear();
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveData = new()
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
                using BinaryWriter bw = new(new FileStream(saveFileName, FileMode.Create));
                SortArray();
                for (int x = 0; x < pointer; x++)
                {
                    for (int y = 0; y < attributes; y++)
                    {
                        bw.Write(wikiData[x, y]);
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ButtonOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openData = new()
            {
                Filter = "DAT Files|*.dat",
                Title = "Open file..."
            };
            if (openData.ShowDialog() == DialogResult.OK)
            {
                OpenFile(openData.FileName);
            }
        }

        private void OpenFile(string openFileName)
        {
            int x = 0;
            try
            {
                using BinaryReader br = new(new FileStream(openFileName, FileMode.Open));
                while (br.BaseStream.Position != br.BaseStream.Length)
                {
                    for (int y = 0; y < attributes; y++)
                    {
                        wikiData[x, y] = br.ReadString();
                    }
                    x++;
                }
                pointer = x;
                DisplayNameCat();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString());
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

        private void ListBoxDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                ListBoxDisplay.SetSelected(ListBoxDisplay.SelectedIndex, true);
                DisplayForOne(ListBoxDisplay.SelectedIndex);
            }
            catch
            {
                return;
            }
        }

        private void TextBoxSearch_DoubleClick(object sender, EventArgs e)
        {
            TextBoxSearch.Clear();
        }

        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            if (ListBoxDisplay.SelectedItem != null)
            {
                if (!string.IsNullOrEmpty(TextBoxName.Text) && !string.IsNullOrEmpty(TextBoxCategory.Text)
                    && !string.IsNullOrEmpty(TextBoxStructure.Text) && !string.IsNullOrEmpty(TextBoxCategory.Text))
                {
                    int index = ListBoxDisplay.SelectedIndex;
                    wikiData[index, 0] = TextBoxName.Text;
                    wikiData[index, 1] = TextBoxCategory.Text;
                    wikiData[index, 2] = TextBoxStructure.Text;
                    wikiData[index, 3] = TextBoxDefinition.Text;
                    DisplayNameCat();
                    ClearBoxes();
                }
                else
                {
                    MessageBox.Show("Please make sure all boxes are filled");
                }
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (ListBoxDisplay.SelectedItem != null)
            {
                int index = ListBoxDisplay.SelectedIndex;
                DialogResult deleteEntry = MessageBox.Show("Are you sure you want to delete?", "Confirmation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (deleteEntry == DialogResult.Yes)
                {
                    for (int i = index + 1; i < pointer; i++)
                    {
                        wikiData[i - 1, 0] = wikiData[i, 0];
                        wikiData[i - 1, 1] = wikiData[i, 1];
                        wikiData[i - 1, 2] = wikiData[i, 2];
                        wikiData[i - 1, 3] = wikiData[i, 3];
                    }
                    pointer--;
                    DisplayNameCat();
                    ClearBoxes();
                }
            }
        }
    }
}
