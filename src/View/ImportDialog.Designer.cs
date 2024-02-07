using System.Windows.Forms;

namespace OpenARIANA
{
    partial class ImportDialog
    {
        private Button btnLoad;
        private Button btnCancel;
        private ComboBox dropDownWorkSheets;
        private Label lSelectWorksheet;
        private TextBox tFilePath;
        private Label lFilePath;
        private Label lSelectParser;
        private CheckedListBox ParserSelection;
        private CheckBox bAppendToFile;
        private GroupBox group_advancedSettings;

        private bool isAdvancedExpanded = false;

        public ImportDialog(string filePath)
        {
            _filePath = filePath;
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportDialog));
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dropDownWorkSheets = new System.Windows.Forms.ComboBox();
            this.lSelectWorksheet = new System.Windows.Forms.Label();
            this.tFilePath = new System.Windows.Forms.TextBox();
            this.lFilePath = new System.Windows.Forms.Label();
            this.lSelectParser = new System.Windows.Forms.Label();
            this.ParserSelection = new System.Windows.Forms.CheckedListBox();
            this.bAppendToFile = new System.Windows.Forms.CheckBox();
            this.group_advancedSettings = new System.Windows.Forms.GroupBox();
            this.group_advancedSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(165, 215);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(247, 215);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dropDownWorkSheets
            // 
            this.dropDownWorkSheets.FormattingEnabled = true;
            this.dropDownWorkSheets.Location = new System.Drawing.Point(27, 103);
            this.dropDownWorkSheets.Name = "dropDownWorkSheets";
            this.dropDownWorkSheets.Size = new System.Drawing.Size(295, 21);
            this.dropDownWorkSheets.TabIndex = 2;
            // 
            // lSelectWorksheet
            // 
            this.lSelectWorksheet.AutoEllipsis = true;
            this.lSelectWorksheet.AutoSize = true;
            this.lSelectWorksheet.Location = new System.Drawing.Point(24, 87);
            this.lSelectWorksheet.Name = "lSelectWorksheet";
            this.lSelectWorksheet.Size = new System.Drawing.Size(92, 13);
            this.lSelectWorksheet.TabIndex = 3;
            this.lSelectWorksheet.Text = "Select Worksheet";
            // 
            // tFilePath
            // 
            this.tFilePath.Location = new System.Drawing.Point(27, 49);
            this.tFilePath.Name = "tFilePath";
            this.tFilePath.ReadOnly = true;
            this.tFilePath.Size = new System.Drawing.Size(295, 20);
            this.tFilePath.TabIndex = 4;
            // 
            // lFilePath
            // 
            this.lFilePath.AutoEllipsis = true;
            this.lFilePath.AutoSize = true;
            this.lFilePath.Location = new System.Drawing.Point(24, 33);
            this.lFilePath.Name = "lFilePath";
            this.lFilePath.Size = new System.Drawing.Size(49, 13);
            this.lFilePath.TabIndex = 5;
            this.lFilePath.Text = "From File";
            // 
            // lSelectParser
            // 
            this.lSelectParser.AutoEllipsis = true;
            this.lSelectParser.AutoSize = true;
            this.lSelectParser.Location = new System.Drawing.Point(17, 25);
            this.lSelectParser.Name = "lSelectParser";
            this.lSelectParser.Size = new System.Drawing.Size(125, 13);
            this.lSelectParser.TabIndex = 7;
            this.lSelectParser.Text = "Select Custom Processor";
            // 
            // ParserSelection
            // 
            this.ParserSelection.CheckOnClick = true;
            this.ParserSelection.FormattingEnabled = true;
            // include future processors here.
            this.ParserSelection.Items.AddRange(new object[] {
            "Basic Processor",
            "itrust Processor"
            });
            // set default processor
            this.ParserSelection.SetItemChecked(0, true);
            this.ParserSelection.Location = new System.Drawing.Point(20, 41);
            this.ParserSelection.Name = "ParserSelection";
            this.ParserSelection.Size = new System.Drawing.Size(253, 79);
            this.ParserSelection.TabIndex = 8;
            this.ParserSelection.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ParserSelection_ItemCheck);
            // 
            // bAppendToFile
            // 
            this.bAppendToFile.AutoSize = true;
            this.bAppendToFile.Checked = true;
            this.bAppendToFile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.bAppendToFile.Location = new System.Drawing.Point(27, 139);
            this.bAppendToFile.Name = "bAppendToFile";
            this.bAppendToFile.Size = new System.Drawing.Size(91, 17);
            this.bAppendToFile.TabIndex = 9;
            this.bAppendToFile.Text = "Append to file";
            this.bAppendToFile.UseVisualStyleBackColor = true;
            this.bAppendToFile.Click += new System.EventHandler(this.bAppendToFile_Click);
            // 
            // group_advancedSettings
            // 
            this.group_advancedSettings.Controls.Add(this.ParserSelection);
            this.group_advancedSettings.Controls.Add(this.lSelectParser);
            this.group_advancedSettings.Location = new System.Drawing.Point(27, 175);
            this.group_advancedSettings.Name = "group_advancedSettings";
            this.group_advancedSettings.Size = new System.Drawing.Size(295, 23);
            this.group_advancedSettings.TabIndex = 10;
            this.group_advancedSettings.TabStop = false;
            this.group_advancedSettings.Text = "Advanced ⏷";
            this.group_advancedSettings.Click += new System.EventHandler(this.ToggleAdvanced_Click);
            // 
            // ImportDialog
            // 
            this.ClientSize = new System.Drawing.Size(355, 250);
            this.Controls.Add(this.group_advancedSettings);
            this.Controls.Add(this.bAppendToFile);
            this.Controls.Add(this.lFilePath);
            this.Controls.Add(this.tFilePath);
            this.Controls.Add(this.lSelectWorksheet);
            this.Controls.Add(this.dropDownWorkSheets);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLoad);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ImportDialog";
            this.Text = "Load from Excel";
            this.Load += new System.EventHandler(this.ImportDialog_OnLoad);
            this.group_advancedSettings.ResumeLayout(false);
            this.group_advancedSettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void ToggleAdvanced_Click(object sender, System.EventArgs e)
        {
            if (!isAdvancedExpanded)
            {
                this.group_advancedSettings.Size = new System.Drawing.Size(295, 140);
                this.group_advancedSettings.Text = "Advanced ⏶";
                this.ClientSize = new System.Drawing.Size(355, 370);
                this.btnLoad.Location = new System.Drawing.Point(165, 335);
                this.btnCancel.Location = new System.Drawing.Point(247, 335);
                isAdvancedExpanded = true;
            }
            else
            {
                this.group_advancedSettings.Size = new System.Drawing.Size(295, 20);
                this.group_advancedSettings.Text = "Advanced ⏷";
                this.ClientSize = new System.Drawing.Size(355, 250);
                this.btnLoad.Location = new System.Drawing.Point(165, 215);
                this.btnCancel.Location = new System.Drawing.Point(247, 215);
                isAdvancedExpanded = false;
            }
            
        }
    }
}
