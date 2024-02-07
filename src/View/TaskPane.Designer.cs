using System.Windows.Forms;

namespace OpenARIANA
{
    partial class TagTaskPane
    {
        private Button btnFindTags;
        private Button btnApplyProfile;
        private ComboBox dProfileSelection;
        private Label lSelectProfile;
        private DataGridView gTagReplacementList;
        private DataGridViewTextBoxColumn TagColumn;
        private DataGridViewTextBoxColumn ReplacementColumn;
        private Button btnSettings;
        private Button btnReplaceFromGrid;
        private Button btnSaveProfile;
        private Button btnDeleteProfile;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TagTaskPane));
            this.btnApplyProfile = new System.Windows.Forms.Button();
            this.btnFindTags = new System.Windows.Forms.Button();
            this.dProfileSelection = new System.Windows.Forms.ComboBox();
            this.lSelectProfile = new System.Windows.Forms.Label();
            this.gTagReplacementList = new System.Windows.Forms.DataGridView();
            this.Tag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Replacement = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnReplaceFromGrid = new System.Windows.Forms.Button();
            this.btnSaveProfile = new System.Windows.Forms.Button();
            this.btnDeleteProfile = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gTagReplacementList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnApplyProfile
            // 
            this.btnApplyProfile.Location = new System.Drawing.Point(14, 567);
            this.btnApplyProfile.Name = "btnApplyProfile";
            this.btnApplyProfile.Size = new System.Drawing.Size(66, 24);
            this.btnApplyProfile.TabIndex = 4;
            this.btnApplyProfile.Text = "Apply";
            this.btnApplyProfile.UseVisualStyleBackColor = true;
            this.btnApplyProfile.Click += new System.EventHandler(this.btnApplyProfile_Click);
            // 
            // btnFindTags
            // 
            this.btnFindTags.Location = new System.Drawing.Point(14, 3);
            this.btnFindTags.Name = "btnFindTags";
            this.btnFindTags.Size = new System.Drawing.Size(66, 24);
            this.btnFindTags.TabIndex = 7;
            this.btnFindTags.Text = "Find";
            this.btnFindTags.UseVisualStyleBackColor = true;
            this.btnFindTags.Click += new System.EventHandler(this.btnFindTags_Click);
            // 
            // dProfileSelection
            // 
            this.dProfileSelection.FormattingEnabled = true;
            this.dProfileSelection.Location = new System.Drawing.Point(14, 540);
            this.dProfileSelection.Name = "dProfileSelection";
            this.dProfileSelection.Size = new System.Drawing.Size(219, 21);
            this.dProfileSelection.TabIndex = 19;
            // 
            // lSelectProfile
            // 
            this.lSelectProfile.AutoSize = true;
            this.lSelectProfile.Location = new System.Drawing.Point(164, 521);
            this.lSelectProfile.Name = "lSelectProfile";
            this.lSelectProfile.Size = new System.Drawing.Size(69, 13);
            this.lSelectProfile.TabIndex = 20;
            this.lSelectProfile.Text = "Select Profile";
            // 
            // gTagReplacementList
            // 
            this.gTagReplacementList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gTagReplacementList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gTagReplacementList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gTagReplacementList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gTagReplacementList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Tag,
            this.Replacement});
            this.gTagReplacementList.Location = new System.Drawing.Point(13, 33);
            this.gTagReplacementList.Name = "gTagReplacementList";
            this.gTagReplacementList.RowHeadersVisible = false;
            this.gTagReplacementList.Size = new System.Drawing.Size(220, 473);
            this.gTagReplacementList.TabIndex = 23;
            // 
            // Tag
            // 
            this.Tag.HeaderText = "Tag";
            this.Tag.Name = "Tag";
            // 
            // Replacement
            // 
            this.Replacement.HeaderText = "Replacement";
            this.Replacement.Name = "Replacement";
            // 
            // btnReplaceFromGrid
            // 
            this.btnReplaceFromGrid.Location = new System.Drawing.Point(13, 510);
            this.btnReplaceFromGrid.Name = "btnReplaceFromGrid";
            this.btnReplaceFromGrid.Size = new System.Drawing.Size(66, 24);
            this.btnReplaceFromGrid.TabIndex = 24;
            this.btnReplaceFromGrid.Text = "Replace";
            this.btnReplaceFromGrid.UseVisualStyleBackColor = true;
            this.btnReplaceFromGrid.Click += new System.EventHandler(this.btnReplaceFromGrid_Click);
            // 
            // btnSaveProfile
            // 
            this.btnSaveProfile.Location = new System.Drawing.Point(133, 567);
            this.btnSaveProfile.Name = "btnSaveProfile";
            this.btnSaveProfile.Size = new System.Drawing.Size(47, 24);
            this.btnSaveProfile.TabIndex = 25;
            this.btnSaveProfile.Text = "Save";
            this.btnSaveProfile.UseVisualStyleBackColor = true;
            this.btnSaveProfile.Click += new System.EventHandler(this.btnSaveProfile_Click);
            // 
            // btnDeleteProfile
            // 
            this.btnDeleteProfile.Location = new System.Drawing.Point(186, 567);
            this.btnDeleteProfile.Name = "btnDeleteProfile";
            this.btnDeleteProfile.Size = new System.Drawing.Size(47, 24);
            this.btnDeleteProfile.TabIndex = 26;
            this.btnDeleteProfile.Text = "Delete";
            this.btnDeleteProfile.UseVisualStyleBackColor = true;
            this.btnDeleteProfile.Click += new System.EventHandler(this.btnDeleteProfile_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.SystemColors.MenuBar;
            this.btnSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnSettings.Image")));
            this.btnSettings.Location = new System.Drawing.Point(207, 3);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(26, 24);
            this.btnSettings.TabIndex = 27;
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.SystemColors.MenuBar;
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(175, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(26, 24);
            this.btnRefresh.TabIndex = 28;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // TagTaskPane
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Pane;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.MenuBar;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnDeleteProfile);
            this.Controls.Add(this.lSelectProfile);
            this.Controls.Add(this.dProfileSelection);
            this.Controls.Add(this.btnSaveProfile);
            this.Controls.Add(this.btnApplyProfile);
            this.Controls.Add(this.btnReplaceFromGrid);
            this.Controls.Add(this.gTagReplacementList);
            this.Controls.Add(this.btnFindTags);
            this.MaximumSize = new System.Drawing.Size(350, 9999);
            this.MinimumSize = new System.Drawing.Size(250, 800);
            this.Name = "TagTaskPane";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.Size = new System.Drawing.Size(250, 800);
            ((System.ComponentModel.ISupportInitialize)(this.gTagReplacementList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private DataGridViewTextBoxColumn Tag;
        private DataGridViewTextBoxColumn Replacement;
        private Button btnRefresh;
    }
}
