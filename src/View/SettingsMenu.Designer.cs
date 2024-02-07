using System;
using System.Windows.Forms;

namespace OpenARIANA
{
    partial class SettingsMenu
    {
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsMenu));
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.Logger = new System.Windows.Forms.GroupBox();
            this.btnOpenLogs = new System.Windows.Forms.Button();
            this.tLoggerPath = new System.Windows.Forms.TextBox();
            this.lLoggerPath = new System.Windows.Forms.Label();
            this.cVerbosity = new System.Windows.Forms.ComboBox();
            this.lVerbosity = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ImportExport = new System.Windows.Forms.GroupBox();
            this.cStyleMappings = new System.Windows.Forms.ComboBox();
            this.lStyleMapping = new System.Windows.Forms.Label();
            this.ProfileManager = new System.Windows.Forms.GroupBox();
            this.btnFileBrowser = new System.Windows.Forms.Button();
            this.tSearch = new System.Windows.Forms.TextBox();
            this.cProfiles = new System.Windows.Forms.ComboBox();
            this.lProfiles = new System.Windows.Forms.Label();
            this.btnManageProfile = new System.Windows.Forms.Button();
            this.tProfilePath = new System.Windows.Forms.TextBox();
            this.lProfilePath = new System.Windows.Forms.Label();
            this.SearchModes = new System.Windows.Forms.GroupBox();
            this.chMatchCase = new System.Windows.Forms.CheckBox();
            this.chRegEx = new System.Windows.Forms.CheckBox();
            this.chNormal = new System.Windows.Forms.CheckBox();
            this.lSearch = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4.SuspendLayout();
            this.Logger.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.ImportExport.SuspendLayout();
            this.ProfileManager.SuspendLayout();
            this.SearchModes.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(538, 367);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(619, 367);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.textBox5);
            this.tabPage4.Controls.Add(this.Logger);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(678, 330);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "General";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // textBox5
            // 
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.textBox5.Location = new System.Drawing.Point(368, 312);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(306, 13);
            this.textBox5.TabIndex = 1;
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Logger
            // 
            this.Logger.Controls.Add(this.btnOpenLogs);
            this.Logger.Controls.Add(this.tLoggerPath);
            this.Logger.Controls.Add(this.lLoggerPath);
            this.Logger.Controls.Add(this.cVerbosity);
            this.Logger.Controls.Add(this.lVerbosity);
            this.Logger.Location = new System.Drawing.Point(6, 6);
            this.Logger.Name = "Logger";
            this.Logger.Size = new System.Drawing.Size(666, 100);
            this.Logger.TabIndex = 0;
            this.Logger.TabStop = false;
            this.Logger.Text = "Logger";
            // 
            // btnOpenLogs
            // 
            this.btnOpenLogs.Location = new System.Drawing.Point(589, 43);
            this.btnOpenLogs.Name = "btnOpenLogs";
            this.btnOpenLogs.Size = new System.Drawing.Size(71, 23);
            this.btnOpenLogs.TabIndex = 3;
            this.btnOpenLogs.Text = "Show Logs";
            this.btnOpenLogs.UseVisualStyleBackColor = true;
            this.btnOpenLogs.Click += new System.EventHandler(this.btnOpenLogs_Click);
            // 
            // tLoggerPath
            // 
            this.tLoggerPath.Enabled = false;
            this.tLoggerPath.Location = new System.Drawing.Point(75, 16);
            this.tLoggerPath.Name = "tLoggerPath";
            this.tLoggerPath.Size = new System.Drawing.Size(585, 20);
            this.tLoggerPath.TabIndex = 1;
            // 
            // lLoggerPath
            // 
            this.lLoggerPath.AutoSize = true;
            this.lLoggerPath.Location = new System.Drawing.Point(6, 19);
            this.lLoggerPath.Name = "lLoggerPath";
            this.lLoggerPath.Size = new System.Drawing.Size(29, 13);
            this.lLoggerPath.TabIndex = 2;
            this.lLoggerPath.Text = "Path";
            // 
            // cVerbosity
            // 
            this.cVerbosity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cVerbosity.FormattingEnabled = true;
            this.cVerbosity.Items.AddRange(new object[] {
            "ErrorsOnly",
            "ErrorsAndWarning",
            "All"});
            this.cVerbosity.Location = new System.Drawing.Point(75, 43);
            this.cVerbosity.Name = "cVerbosity";
            this.cVerbosity.Size = new System.Drawing.Size(175, 21);
            this.cVerbosity.TabIndex = 1;
            // 
            // lVerbosity
            // 
            this.lVerbosity.AutoSize = true;
            this.lVerbosity.Location = new System.Drawing.Point(6, 46);
            this.lVerbosity.Name = "lVerbosity";
            this.lVerbosity.Size = new System.Drawing.Size(50, 13);
            this.lVerbosity.TabIndex = 0;
            this.lVerbosity.Text = "Verbosity";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ImportExport);
            this.tabPage1.Controls.Add(this.ProfileManager);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(678, 330);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Data Transformation";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ImportExport
            // 
            this.ImportExport.Controls.Add(this.cStyleMappings);
            this.ImportExport.Controls.Add(this.lStyleMapping);
            this.ImportExport.Enabled = false;
            this.ImportExport.Location = new System.Drawing.Point(6, 207);
            this.ImportExport.Name = "ImportExport";
            this.ImportExport.Size = new System.Drawing.Size(666, 113);
            this.ImportExport.TabIndex = 1;
            this.ImportExport.TabStop = false;
            this.ImportExport.Text = "Import/Export";
            // 
            // cStyleMappings
            // 
            this.cStyleMappings.FormattingEnabled = true;
            this.cStyleMappings.Location = new System.Drawing.Point(135, 15);
            this.cStyleMappings.Name = "cStyleMappings";
            this.cStyleMappings.Size = new System.Drawing.Size(522, 21);
            this.cStyleMappings.TabIndex = 7;
            // 
            // lStyleMapping
            // 
            this.lStyleMapping.AutoSize = true;
            this.lStyleMapping.Location = new System.Drawing.Point(12, 18);
            this.lStyleMapping.Name = "lStyleMapping";
            this.lStyleMapping.Size = new System.Drawing.Size(74, 13);
            this.lStyleMapping.TabIndex = 6;
            this.lStyleMapping.Text = "Style Mapping";
            // 
            // ProfileManager
            // 
            this.ProfileManager.Controls.Add(this.btnFileBrowser);
            this.ProfileManager.Controls.Add(this.tSearch);
            this.ProfileManager.Controls.Add(this.cProfiles);
            this.ProfileManager.Controls.Add(this.lProfiles);
            this.ProfileManager.Controls.Add(this.btnManageProfile);
            this.ProfileManager.Controls.Add(this.tProfilePath);
            this.ProfileManager.Controls.Add(this.lProfilePath);
            this.ProfileManager.Controls.Add(this.SearchModes);
            this.ProfileManager.Controls.Add(this.lSearch);
            this.ProfileManager.Location = new System.Drawing.Point(6, 6);
            this.ProfileManager.Name = "ProfileManager";
            this.ProfileManager.Size = new System.Drawing.Size(666, 195);
            this.ProfileManager.TabIndex = 0;
            this.ProfileManager.TabStop = false;
            this.ProfileManager.Text = "Profile Manager";
            // 
            // btnFileBrowser
            // 
            this.btnFileBrowser.Image = global::OpenARIANA.Properties.Resources.OpenFolderIcon;
            this.btnFileBrowser.Location = new System.Drawing.Point(629, 106);
            this.btnFileBrowser.Name = "btnFileBrowser";
            this.btnFileBrowser.Size = new System.Drawing.Size(31, 23);
            this.btnFileBrowser.TabIndex = 3;
            this.btnFileBrowser.UseVisualStyleBackColor = true;
            this.btnFileBrowser.Click += new System.EventHandler(this.btnFileBrowser_Click);
            // 
            // tSearch
            // 
            this.tSearch.Location = new System.Drawing.Point(141, 34);
            this.tSearch.Name = "tSearch";
            this.tSearch.Size = new System.Drawing.Size(264, 20);
            this.tSearch.TabIndex = 10;
            // 
            // cProfiles
            // 
            this.cProfiles.FormattingEnabled = true;
            this.cProfiles.Location = new System.Drawing.Point(141, 136);
            this.cProfiles.Name = "cProfiles";
            this.cProfiles.Size = new System.Drawing.Size(519, 21);
            this.cProfiles.TabIndex = 9;
            // 
            // lProfiles
            // 
            this.lProfiles.AutoSize = true;
            this.lProfiles.Location = new System.Drawing.Point(18, 139);
            this.lProfiles.Name = "lProfiles";
            this.lProfiles.Size = new System.Drawing.Size(41, 13);
            this.lProfiles.TabIndex = 8;
            this.lProfiles.Text = "Profiles";
            // 
            // btnManageProfile
            // 
            this.btnManageProfile.Location = new System.Drawing.Point(516, 166);
            this.btnManageProfile.Name = "btnManageProfile";
            this.btnManageProfile.Size = new System.Drawing.Size(144, 23);
            this.btnManageProfile.TabIndex = 6;
            this.btnManageProfile.Text = "Manage Profile";
            this.btnManageProfile.UseVisualStyleBackColor = true;
            this.btnManageProfile.Click += new System.EventHandler(this.btnManageProfile_Click);
            // 
            // tProfilePath
            // 
            this.tProfilePath.Enabled = false;
            this.tProfilePath.Location = new System.Drawing.Point(141, 108);
            this.tProfilePath.Name = "tProfilePath";
            this.tProfilePath.Size = new System.Drawing.Size(482, 20);
            this.tProfilePath.TabIndex = 5;
            // 
            // lProfilePath
            // 
            this.lProfilePath.AutoSize = true;
            this.lProfilePath.Location = new System.Drawing.Point(18, 111);
            this.lProfilePath.Name = "lProfilePath";
            this.lProfilePath.Size = new System.Drawing.Size(81, 13);
            this.lProfilePath.TabIndex = 4;
            this.lProfilePath.Text = "Profile Directory";
            // 
            // SearchModes
            // 
            this.SearchModes.Controls.Add(this.chMatchCase);
            this.SearchModes.Controls.Add(this.chRegEx);
            this.SearchModes.Controls.Add(this.chNormal);
            this.SearchModes.Location = new System.Drawing.Point(411, 19);
            this.SearchModes.Name = "SearchModes";
            this.SearchModes.Size = new System.Drawing.Size(249, 69);
            this.SearchModes.TabIndex = 2;
            this.SearchModes.TabStop = false;
            this.SearchModes.Text = "Search Mode";
            // 
            // chMatchCase
            // 
            this.chMatchCase.AutoSize = true;
            this.chMatchCase.Location = new System.Drawing.Point(160, 19);
            this.chMatchCase.Name = "chMatchCase";
            this.chMatchCase.Size = new System.Drawing.Size(83, 17);
            this.chMatchCase.TabIndex = 2;
            this.chMatchCase.Text = "Match Case";
            this.chMatchCase.UseVisualStyleBackColor = true;
            // 
            // chRegEx
            // 
            this.chRegEx.AutoSize = true;
            this.chRegEx.Checked = true;
            this.chRegEx.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chRegEx.Location = new System.Drawing.Point(6, 42);
            this.chRegEx.Name = "chRegEx";
            this.chRegEx.Size = new System.Drawing.Size(58, 17);
            this.chRegEx.TabIndex = 1;
            this.chRegEx.Text = "RegEx";
            this.chRegEx.UseVisualStyleBackColor = true;
            // 
            // chNormal
            // 
            this.chNormal.AutoSize = true;
            this.chNormal.Location = new System.Drawing.Point(6, 19);
            this.chNormal.Name = "chNormal";
            this.chNormal.Size = new System.Drawing.Size(59, 17);
            this.chNormal.TabIndex = 0;
            this.chNormal.Text = "Normal";
            this.chNormal.UseVisualStyleBackColor = true;
            // 
            // lSearch
            // 
            this.lSearch.AutoSize = true;
            this.lSearch.Location = new System.Drawing.Point(18, 37);
            this.lSearch.Name = "lSearch";
            this.lSearch.Size = new System.Drawing.Size(56, 13);
            this.lSearch.TabIndex = 0;
            this.lSearch.Text = "Find what:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(686, 356);
            this.tabControl1.TabIndex = 0;
            // 
            // SettingsMenu
            // 
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(710, 398);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsMenu";
            this.Text = "OpenARIANA Settings";
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.Logger.ResumeLayout(false);
            this.Logger.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.ImportExport.ResumeLayout(false);
            this.ImportExport.PerformLayout();
            this.ProfileManager.ResumeLayout(false);
            this.ProfileManager.PerformLayout();
            this.SearchModes.ResumeLayout(false);
            this.SearchModes.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        private Button btnSave;
        private Button btnCancel;
        private FolderBrowserDialog folderBrowserDialog;
        private TabPage tabPage4;
        private TextBox textBox5;
        private GroupBox Logger;
        private Button btnOpenLogs;
        private TextBox tLoggerPath;
        private Label lLoggerPath;
        protected ComboBox cVerbosity;
        private Label lVerbosity;
        private TabPage tabPage1;
        private GroupBox ImportExport;
        private ComboBox cStyleMappings;
        private Label lStyleMapping;
        private GroupBox ProfileManager;
        private Button btnFileBrowser;
        private TextBox tSearch;
        private ComboBox cProfiles;
        private Label lProfiles;
        private Button btnManageProfile;
        private TextBox tProfilePath;
        private Label lProfilePath;
        private GroupBox SearchModes;
        private CheckBox chMatchCase;
        private CheckBox chRegEx;
        private CheckBox chNormal;
        private Label lSearch;
        private TabControl tabControl1;
    }
}