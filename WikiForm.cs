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
        string fileName = "definitions.dat";
        int pointer = 0;
        string[,] wikiData = new string[entries, attributes];

        private void DisplayNameCat()
        {
            ListBoxDisplay.Items.Clear();
            for (int x = 0; x < entries; x++)
            {
                string term = wikiData[x, 0] + "\t" + wikiData[x, 1];
                ListBoxDisplay.Items.Add(term);
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddTerm();
            ClearBoxes();
        }

        private void AddTerm()
        {
            if (pointer < entries)
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
                    MessageBox.Show("Nobody wants any more data structures to define");
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
                DisplayInfo(foundIndex);
            }
            else
            {
                MessageBox.Show("Not found");
                TextBoxSearch.Clear();
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (BinaryWriter bw = new BinaryWriter(new FileStream(fileName, FileMode.Create)))
                {
                    SortArray();
                    for (int x = 0; x < pointer; x++)
                    {
                        for (int y = 0; y < attributes; y++)
                        {
                            bw.Write(wikiData[x, y]);
                        }
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
            int x = 0;
            try
            {
                using (BinaryReader br = new BinaryReader(new FileStream(fileName, FileMode.Open)))
                {
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
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void DisplayInfo(int x)
        {
            ClearBoxes();
            TextBoxName.Text = wikiData[x, 0];
            TextBoxCategory.Text = wikiData[x, 1];
            TextBoxStructure.Text = wikiData[x, 2];
            TextBoxDefinition.Text = wikiData[x, 3];
        }
    }
}
