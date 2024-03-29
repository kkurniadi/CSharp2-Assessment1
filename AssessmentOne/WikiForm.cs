﻿using System;
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
// Date: 22/03/2022
// A program that stores wiki data in a two-dimensional array
namespace AssessmentOne
{
    public partial class WikiForm : Form
    {
        public WikiForm()
        {
            InitializeComponent();
        }
        // 8.1	Create a global 2D string array, use static variables for the dimensions(row, column),
        static int entries = 12;
        static int attributes = 4;
        string[,] wikiData = new string[entries, attributes];
        string defaultFileName = "definitions.dat";
        int pointer = 0;

        // 8.2	Create an ADD button that will store the information from the 4 text boxes into the 2D array,
        // an EDIT button that will modify the information in the array,
        // and a DELETE button that will remove items from the array
        #region AddEditDelete
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
                        DisplayNameCat();
                        StatusStrip.Text = "Entry added";
                    }
                    catch
                    {
                        StatusStrip.Text = "Could not add entry";
                    }
                }
                else
                {
                    StatusStrip.Text = "Please make sure all boxes are filled";
                }
            }
            else
            {
                StatusStrip.Text = "The array is full";
                ClearBoxes();
            }
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
                        StatusStrip.Text = "Entry edited";
                    }
                    else
                        StatusStrip.Text = "Editing cancelled";
                }
                else
                {
                    StatusStrip.Text = "Please make sure all boxes are filled";
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
                    StatusStrip.Text = "Entry deleted";
                }
                else
                    StatusStrip.Text = "Deletion cancelled";
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
        // 8.4	Write the code for a Bubble Sort method to sort the 2D array by Name ascending,
        // ensure you use a separate swap method that passes (by reference) the array element to be swapped
        // (do not use any built-in array methods),
        #region SortSwap
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
            StatusStrip.Text = "Data has been sorted";
        }
        #endregion SortSwap
        // 8.5	Write the code for a Binary Search for the Name in the 2D array
        // and display the information in the other textboxes when found,
        // add suitable feedback if the search in not successful and clear the search textbox
        // (do not use any built-in array methods),
        // A double mouse click in the search text box will clear the search input box,
        #region Search
        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TextBoxSearch.Text))
            {
                int startIndex = -1;
                int finalIndex = pointer;
                bool found = false;
                int foundIndex = -1;
                while (!((finalIndex - startIndex) <= 1))
                {
                    int midIndex = (finalIndex + startIndex) / 2;
                    if (string.Compare(wikiData[midIndex, 0], TextBoxSearch.Text) == 0)
                    {
                        foundIndex = midIndex;
                        found = true;
                        break;
                    }
                    else
                    {
                        if (string.Compare(wikiData[midIndex, 0], TextBoxSearch.Text) == 1)
                            finalIndex = midIndex;
                        else
                            startIndex = midIndex;
                    }
                }
                if (found)
                {
                    ListBoxDisplay.SelectedIndex = foundIndex;
                    DisplayForOne(foundIndex);
                    StatusStrip.Text = "Search successful";
                }
                else
                {
                    StatusStrip.Text = "Not found";
                    ListBoxDisplay.ClearSelected();
                    ClearBoxes();
                    TextBoxSearch.Clear();
                }
            }
            else
            {
                StatusStrip.Text = "Invalid search";
            }
        }
        private void TextBoxSearch_DoubleClick(object sender, EventArgs e)
        {
            TextBoxSearch.Clear();
        }
        #endregion Search
        // 8.6	Create a display method that will show the following information in a List box: Name and Category,
        // 8.7	Create a method so the user can select a definition (Name) from the Listbox
        // and all the information is displayed in the appropriate Textboxes,
        #region Display
        private void DisplayNameCat()
        {
            ListBoxDisplay.Items.Clear();
            for (int x = 0; x < pointer; x++)
            {
                string term = wikiData[x, 0] + "\t" + wikiData[x, 1];
                ListBoxDisplay.Items.Add(term);
            }
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
        private void DisplayForOne(int x)
        {
            ClearBoxes();
            TextBoxName.Text = wikiData[x, 0];
            TextBoxCategory.Text = wikiData[x, 1];
            TextBoxStructure.Text = wikiData[x, 2];
            TextBoxDefinition.Text = wikiData[x, 3];
        }
        #endregion Display
        // 8.8	Create a SAVE button so the information from the 2D array can be written into
        // a binary file called definitions.dat which is sorted by Name,
        // 8.9	Create a LOAD button that will read the information
        // from a binary file called definitions.dat into the 2D array,
        #region FileIO
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveData = new SaveFileDialog
            {
                InitialDirectory = Application.StartupPath,
                Filter = "DAT Files|*.dat",
                Title = "Save file...",
                FileName = defaultFileName,
                DefaultExt = "dat"
            };
            if (saveData.ShowDialog() == DialogResult.OK)
            {
                SaveFile(saveData.FileName);
            }
            else
                StatusStrip.Text = "Cancelled saving the data";
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
                StatusStrip.Text = "Data has been saved";
            }
            catch (IOException ex)
            {
                StatusStrip.Text = ex.ToString();
            }
        }
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
            else
                StatusStrip.Text = "Cancelled file opening";
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
                StatusStrip.Text = "Opened data from file";
            }
            catch (IOException ex)
            {
                StatusStrip.Text = ex.ToString();
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
