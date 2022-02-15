namespace AssessmentOne
{
    partial class WikiForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ButtonAdd = new System.Windows.Forms.Button();
            this.ButtonEdit = new System.Windows.Forms.Button();
            this.ButtonDelete = new System.Windows.Forms.Button();
            this.TextBoxName = new System.Windows.Forms.TextBox();
            this.TextBoxCategory = new System.Windows.Forms.TextBox();
            this.TextBoxStructure = new System.Windows.Forms.TextBox();
            this.TextBoxDefinition = new System.Windows.Forms.TextBox();
            this.TextBoxSearch = new System.Windows.Forms.TextBox();
            this.ListBoxDisplay = new System.Windows.Forms.ListBox();
            this.ButtonOpen = new System.Windows.Forms.Button();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.ButtonSearch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ButtonAdd
            // 
            this.ButtonAdd.Location = new System.Drawing.Point(18, 23);
            this.ButtonAdd.Name = "ButtonAdd";
            this.ButtonAdd.Size = new System.Drawing.Size(75, 23);
            this.ButtonAdd.TabIndex = 0;
            this.ButtonAdd.Text = "Add";
            this.ButtonAdd.UseVisualStyleBackColor = true;
            this.ButtonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // ButtonEdit
            // 
            this.ButtonEdit.Location = new System.Drawing.Point(99, 23);
            this.ButtonEdit.Name = "ButtonEdit";
            this.ButtonEdit.Size = new System.Drawing.Size(75, 23);
            this.ButtonEdit.TabIndex = 1;
            this.ButtonEdit.Text = "Edit";
            this.ButtonEdit.UseVisualStyleBackColor = true;
            this.ButtonEdit.Click += new System.EventHandler(this.ButtonEdit_Click);
            // 
            // ButtonDelete
            // 
            this.ButtonDelete.Location = new System.Drawing.Point(180, 23);
            this.ButtonDelete.Name = "ButtonDelete";
            this.ButtonDelete.Size = new System.Drawing.Size(75, 23);
            this.ButtonDelete.TabIndex = 2;
            this.ButtonDelete.Text = "Delete";
            this.ButtonDelete.UseVisualStyleBackColor = true;
            // 
            // TextBoxName
            // 
            this.TextBoxName.Location = new System.Drawing.Point(18, 68);
            this.TextBoxName.Name = "TextBoxName";
            this.TextBoxName.PlaceholderText = "Name";
            this.TextBoxName.Size = new System.Drawing.Size(237, 23);
            this.TextBoxName.TabIndex = 3;
            // 
            // TextBoxCategory
            // 
            this.TextBoxCategory.Location = new System.Drawing.Point(18, 106);
            this.TextBoxCategory.Name = "TextBoxCategory";
            this.TextBoxCategory.PlaceholderText = "Category";
            this.TextBoxCategory.Size = new System.Drawing.Size(237, 23);
            this.TextBoxCategory.TabIndex = 4;
            // 
            // TextBoxStructure
            // 
            this.TextBoxStructure.Location = new System.Drawing.Point(18, 146);
            this.TextBoxStructure.Name = "TextBoxStructure";
            this.TextBoxStructure.PlaceholderText = "Structure";
            this.TextBoxStructure.Size = new System.Drawing.Size(237, 23);
            this.TextBoxStructure.TabIndex = 5;
            // 
            // TextBoxDefinition
            // 
            this.TextBoxDefinition.Location = new System.Drawing.Point(18, 184);
            this.TextBoxDefinition.Multiline = true;
            this.TextBoxDefinition.Name = "TextBoxDefinition";
            this.TextBoxDefinition.PlaceholderText = "Definition";
            this.TextBoxDefinition.Size = new System.Drawing.Size(237, 132);
            this.TextBoxDefinition.TabIndex = 6;
            // 
            // TextBoxSearch
            // 
            this.TextBoxSearch.Location = new System.Drawing.Point(302, 24);
            this.TextBoxSearch.Name = "TextBoxSearch";
            this.TextBoxSearch.Size = new System.Drawing.Size(190, 23);
            this.TextBoxSearch.TabIndex = 7;
            this.TextBoxSearch.DoubleClick += new System.EventHandler(this.TextBoxSearch_DoubleClick);
            // 
            // ListBoxDisplay
            // 
            this.ListBoxDisplay.FormattingEnabled = true;
            this.ListBoxDisplay.ItemHeight = 15;
            this.ListBoxDisplay.Location = new System.Drawing.Point(286, 68);
            this.ListBoxDisplay.Name = "ListBoxDisplay";
            this.ListBoxDisplay.Size = new System.Drawing.Size(287, 214);
            this.ListBoxDisplay.TabIndex = 8;
            this.ListBoxDisplay.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListBoxDisplay_MouseClick);
            // 
            // ButtonOpen
            // 
            this.ButtonOpen.Location = new System.Drawing.Point(286, 293);
            this.ButtonOpen.Name = "ButtonOpen";
            this.ButtonOpen.Size = new System.Drawing.Size(75, 23);
            this.ButtonOpen.TabIndex = 9;
            this.ButtonOpen.Text = "Open";
            this.ButtonOpen.UseVisualStyleBackColor = true;
            this.ButtonOpen.Click += new System.EventHandler(this.ButtonOpen_Click);
            // 
            // ButtonSave
            // 
            this.ButtonSave.Location = new System.Drawing.Point(367, 293);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(75, 23);
            this.ButtonSave.TabIndex = 10;
            this.ButtonSave.Text = "Save";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // ButtonSearch
            // 
            this.ButtonSearch.Location = new System.Drawing.Point(498, 24);
            this.ButtonSearch.Name = "ButtonSearch";
            this.ButtonSearch.Size = new System.Drawing.Size(75, 23);
            this.ButtonSearch.TabIndex = 11;
            this.ButtonSearch.Text = "Search";
            this.ButtonSearch.UseVisualStyleBackColor = true;
            this.ButtonSearch.Click += new System.EventHandler(this.ButtonSearch_Click);
            // 
            // WikiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 342);
            this.Controls.Add(this.ButtonSearch);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.ButtonOpen);
            this.Controls.Add(this.ListBoxDisplay);
            this.Controls.Add(this.TextBoxSearch);
            this.Controls.Add(this.TextBoxDefinition);
            this.Controls.Add(this.TextBoxStructure);
            this.Controls.Add(this.TextBoxCategory);
            this.Controls.Add(this.TextBoxName);
            this.Controls.Add(this.ButtonDelete);
            this.Controls.Add(this.ButtonEdit);
            this.Controls.Add(this.ButtonAdd);
            this.Name = "WikiForm";
            this.Text = "Data Structures Wiki";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button ButtonAdd;
        private Button ButtonEdit;
        private Button ButtonDelete;
        private TextBox TextBoxName;
        private TextBox TextBoxCategory;
        private TextBox TextBoxStructure;
        private TextBox TextBoxDefinition;
        private TextBox TextBoxSearch;
        private ListBox ListBoxDisplay;
        private Button ButtonOpen;
        private Button ButtonSave;
        private Button ButtonSearch;
    }
}