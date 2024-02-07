using Microsoft.Office.Core;
using Microsoft.Office.Tools.Ribbon;

namespace OpenARIANA
{
    partial class OpenARIANARibbonMenu : RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public OpenARIANARibbonMenu()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenARIANARibbonMenu));
            this.toolbar = this.Factory.CreateRibbonTab();
            this.data_group = this.Factory.CreateRibbonGroup();
            this.sbtnLoadData = this.Factory.CreateRibbonSplitButton();
            this.btnLoadExcel = this.Factory.CreateRibbonButton();
            this.sbtnExportData = this.Factory.CreateRibbonSplitButton();
            this.btnExportAsExcel = this.Factory.CreateRibbonButton();
            this.btnReplaceTags = this.Factory.CreateRibbonButton();
            this.audit_group = this.Factory.CreateRibbonGroup();
            this.button1 = this.Factory.CreateRibbonButton();
            this.button2 = this.Factory.CreateRibbonButton();
            this.settings_group = this.Factory.CreateRibbonGroup();
            this.btnSettings = this.Factory.CreateRibbonButton();
            this.toolbar.SuspendLayout();
            this.data_group.SuspendLayout();
            this.audit_group.SuspendLayout();
            this.settings_group.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolbar
            // 
            this.toolbar.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.toolbar.Groups.Add(this.data_group);
            this.toolbar.Groups.Add(this.audit_group);
            this.toolbar.Groups.Add(this.settings_group);
            this.toolbar.Label = "OpenARIANA";
            this.toolbar.Name = "toolbar";
            // 
            // data_group
            // 
            this.data_group.Items.Add(this.sbtnLoadData);
            this.data_group.Items.Add(this.sbtnExportData);
            this.data_group.Items.Add(this.btnReplaceTags);
            this.data_group.Label = "Get && Transform Data";
            this.data_group.Name = "data_group";
            // 
            // sbtnLoadData
            // 
            this.sbtnLoadData.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.sbtnLoadData.Image = ((System.Drawing.Image)(resources.GetObject("sbtnLoadData.Image")));
            this.sbtnLoadData.Items.Add(this.btnLoadExcel);
            this.sbtnLoadData.Label = "Load";
            this.sbtnLoadData.Name = "sbtnLoadData";
            this.sbtnLoadData.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnLoadData_Click);
            // 
            // btnLoadExcel
            // 
            this.btnLoadExcel.Label = "From Excel Workbook";
            this.btnLoadExcel.Name = "btnLoadExcel";
            this.btnLoadExcel.ShowImage = true;
            this.btnLoadExcel.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnLoadData_Click);
            // 
            // sbtnExportData
            // 
            this.sbtnExportData.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.sbtnExportData.Enabled = false;
            this.sbtnExportData.Image = ((System.Drawing.Image)(resources.GetObject("sbtnExportData.Image")));
            this.sbtnExportData.Items.Add(this.btnExportAsExcel);
            this.sbtnExportData.Label = "Export";
            this.sbtnExportData.Name = "sbtnExportData";
            this.sbtnExportData.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnExport_Click);
            // 
            // btnExportAsExcel
            // 
            this.btnExportAsExcel.Label = "as Excel Workbook";
            this.btnExportAsExcel.Name = "btnExportAsExcel";
            this.btnExportAsExcel.ShowImage = true;
            // 
            // btnReplaceTags
            // 
            this.btnReplaceTags.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnReplaceTags.Image = ((System.Drawing.Image)(resources.GetObject("btnReplaceTags.Image")));
            this.btnReplaceTags.Label = "Replace Tags";
            this.btnReplaceTags.Name = "btnReplaceTags";
            this.btnReplaceTags.ShowImage = true;
            this.btnReplaceTags.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnReplaceTags_Click);
            // 
            // audit_group
            // 
            this.audit_group.Items.Add(this.button1);
            this.audit_group.Items.Add(this.button2);
            this.audit_group.Label = "Operational Modes";
            this.audit_group.Name = "audit_group";
            // 
            // button1
            // 
            this.button1.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button1.Enabled = false;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Label = "Audit";
            this.button1.Name = "button1";
            this.button1.ShowImage = true;
            // 
            // button2
            // 
            this.button2.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button2.Enabled = false;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Label = "Policy";
            this.button2.Name = "button2";
            this.button2.ShowImage = true;
            // 
            // settings_group
            // 
            this.settings_group.Items.Add(this.btnSettings);
            this.settings_group.Label = "Settings";
            this.settings_group.Name = "settings_group";
            // 
            // btnSettings
            // 
            this.btnSettings.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnSettings.Image")));
            this.btnSettings.Label = "Settings";
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.ShowImage = true;
            this.btnSettings.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSettings_Click);
            // 
            // OpenARIANARibbonMenu
            // 
            this.Name = "OpenARIANARibbonMenu";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.toolbar);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.OpenARIANARibbonMenu_Load);
            this.toolbar.ResumeLayout(false);
            this.toolbar.PerformLayout();
            this.data_group.ResumeLayout(false);
            this.data_group.PerformLayout();
            this.audit_group.ResumeLayout(false);
            this.audit_group.PerformLayout();
            this.settings_group.ResumeLayout(false);
            this.settings_group.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal RibbonTab toolbar;
        internal RibbonButton btnReplaceTags;
        internal RibbonGroup data_group;
        internal RibbonGroup audit_group;

        internal RibbonButton btnSettings;
        internal RibbonGroup settings_group;
        internal RibbonSplitButton sbtnLoadData;
        internal RibbonSplitButton sbtnExportData;
        internal RibbonButton btnLoadExcel;
        internal RibbonButton button1;
        internal RibbonButton button2;
        internal RibbonButton btnExportAsExcel;
    }

    partial class ThisRibbonCollection
    {
        internal OpenARIANARibbonMenu OpenARIANARibbonMenu
        {
            get { return this.GetRibbon<OpenARIANARibbonMenu>(); }
        }
    }
}
