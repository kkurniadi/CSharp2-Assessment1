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
// Kirsten Kurniadi, ID: 30045816, 8/03/2022
// A program that stores wiki data in a two-dimensional array
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
        // 8.1	Create a global 2D string array, use static variables for the dimensions(row, column),
        string[,] wikiData = new string[entries, attributes];
        string defaultFileName = "definitions.dat";
        int pointer = 0;
        
        #region AddEditDelete
        // 8.2	Create ADD, EDIT, DELETE buttons that will store the information from the 4 text boxes into the 2D array,
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddTerm();
            TextBoxName.Focus();
        }
        private void AddTerm()
        {
            if (pointer < entries)
            {
                if (BoxesFilled())
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
            else
            {
                MessageBox.Show("The array is full");
                ClearBoxes();
            }
            DisplayNameCat();
        }
        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            if (ListBoxDisplay.SelectedItem != null)
            {
                if (BoxesFilled())
                {
                    var result = MessageBox.Show("Are you sure you want to edit?", "Edit Entry",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        int index = ListBoxDisplay.SelectedIndex;
                        wikiData[index, 0] = TextBoxName.Text;
                        wikiData[index, 1] = TextBoxCategory.Text;
                        wikiData[index, 2] = TextBoxStructure.Text;
                        wikiData[index, 3] = TextBoxDefinition.Text;
                        DisplayNameCat();
                        ClearBoxes();
                    }
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
        #endregion AddEditDelete
        // 8.3	Create a CLEAR method to clear the four text boxes so a new definition can be added,
        private void ClearBoxes()
        {
            TextBoxName.Clear();
            TextBoxCategory.Clear();
            TextBoxStructure.Clear();
            TextBoxDefinition.Clear();
        }
        #region SortSwap
        // 8.4	Write the code for a Bubble Sort method to sort the 2D array by Name ascending,
        // ensure you use a separate swap method that passes (by reference) the array element to be swapped
        // (do not use any built-in array methods),
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
        private void ListBoxDisplay_DoubleClick(object sender, EventArgs e)
        {
            ClearBoxes();
            SortArray();
            DisplayNameCat();
        }
        #endregion SortSwap
        #region Search
        // 8.5	Write the code for a Binary Search for the Name in the 2D array
        // and display the information in the other textboxes when found,
        // add suitable feedback if the search in not successful and clear the search textbox
        // (do not use any built-in array methods),
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
        // A double mouse click in the search text box will clear the search input box,
        private void TextBoxSearch_DoubleClick(object sender, EventArgs e)
        {
            TextBoxSearch.Clear();
        }
        #endregion Search
        #region Display
        // 8.6	Create a display method that will show the following information in a List box: Name and Category,
        private void DisplayNameCat()
        {
            ListBoxDisplay.Items.Clear();
            for (int x = 0; x < pointer; x++)
            {
                string term = wikiData[x, 0] + "\t" + wikiData[x, 1];
                ListBoxDisplay.Items.Add(term);
            }
        }
        // 8.7	Create a method so the user can select a definition (Name) from the Listbox
        // and all the information is displayed in the appropriate Textboxes,
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
        private void DisplayForOne(int x)
        {
            ClearBoxes();
            TextBoxName.Text = wikiData[x, 0];
            TextBoxCategory.Text = wikiData[x, 1];
            TextBoxStructure.Text = wikiData[x, 2];
            TextBoxDefinition.Text = wikiData[x, 3];
        }
        #endregion Display
        #region FileIO
        // 8.8	Create a SAVE button so the information from the 2D array can be written into
        // a binary file called definitions.dat which is sorted by Name,
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveData = new SaveFileDialog
            {
                Filter = "DAT Files|*.dat",
                Title = "Save file...",
                InitialDirectory = Application.StartupPath,
                DefaultExt = "dat"
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
        // 8.9	Create a LOAD button that will read the information
        // from a binary file called definitions.dat into the 2D array,
        private void ButtonOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openData = new OpenFileDialog
            {
                InitialDirectory = Application.StartupPath,
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
            int ptr = 0;
            try
            {
                using (Stream stream = File.Open(openFileName, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    while (stream.Position < stream.Length)
                    {
                        for (int x = 0; x < entries; x++)
                        {
                            for (int y = 0; y < attributes; y++)
                            {
                                wikiData[x, y] = (string)bin.Deserialize(stream);
                            }
                            ptr++;
                        }
                    }
                    pointer = ptr;
                    DisplayNameCat();
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion FileIO
        private bool BoxesFilled()
        {
            if (!string.IsNullOrWhiteSpace(TextBoxName.Text)
                && !string.IsNullOrWhiteSpace(TextBoxCategory.Text)
                && !string.IsNullOrWhiteSpace(TextBoxStructure.Text)
                && !string.IsNullOrWhiteSpace(TextBoxDefinition.Text))
            {
                return true;
            }
            return false;
        }
    }
}
