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

        private void DisplayData()
        {
            listBoxDisplay.Items.Clear();
            for (int x = 0; x < entries; x++)
            {
                string term = wikiData[x, 0] + "\t" + wikiData[x, 1];
                listBoxDisplay.Items.Add(term);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
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
            DisplayData();
        }

        private void ClearBoxes()
        {
            textBoxName.Clear();
            textBoxCategory.Clear();
            textBoxStructure.Clear();
            textBoxDefinition.Clear();
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

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            int startIndex = -1;
            int finalIndex = pointer;
            bool found = false;
            int foundIndex = -1;
            while (!false && !((finalIndex - startIndex) <= 1))
            {
                int newIndex = (finalIndex + startIndex) / 2;
                if (string.Compare(wikiData[newIndex, 0], textBoxSearch.Text) == 0)
                {
                    foundIndex = newIndex;
                    found = true;
                    break;
                }
                else
                {
                    if (string.Compare(wikiData[newIndex, 0], textBoxSearch.Text) == 1)
                        finalIndex = newIndex;
                    else
                        startIndex = newIndex;
                }
            }
            if (found)
            {
                textBoxName.Text = wikiData[foundIndex, 0];
                textBoxCategory.Text = wikiData[foundIndex, 1];
                textBoxStructure.Text = wikiData[foundIndex, 2];
                textBoxDefinition.Text = wikiData[foundIndex, 3];
            }
            else
            {
                MessageBox.Show("Not found");
                textBoxSearch.Clear();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (BinaryWriter bw = new BinaryWriter(new FileStream(fileName, FileMode.Create)))
                {
                    SortArray();
                    for (int y = 0; y < pointer; y++)
                    {
                        for (int x = 0; x < attributes; x++)
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

        private void buttonOpen_Click(object sender, EventArgs e)
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
                    DisplayData();
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
