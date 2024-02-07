using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;


namespace OpenARIANA
{
    public partial class SettingsMenu : Form
    {
        public SettingsMenu()
        {
            InitializeComponent();
            InitializeDataTab();
            InitializeGeneralTab();
        }

        private void InitializeDataTab()
        {
            ProfileManager profileManager = OpenARIANA.ProfileManager.Instance;

            tProfilePath.Text = profileManager.CentralMappingFilePath;
            tSearch.Text = profileManager.TagPattern;

            List<string> profiles = profileManager.GetAllProfileNames();
            foreach (string profile in profiles)
            {
                cProfiles.Items.Add(profile);
            }
        }

        private void CloseDialog()
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void InitializeGeneralTab()
        {
            string logFilePath = Utilities.Logger.LogFilePath;
            tLoggerPath.Text = logFilePath;
            cVerbosity.SelectedIndex = (int)Utilities.Logger.Verbosity;
            textBox5.Text = "OpenARIANA v" + GetAssemblyVersion();
        }

        private static string GetAssemblyVersion()
        {
            // Get the version of the current assembly (i.e., your add-in)
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            // Convert it to a string, if necessary
            string informationalVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;

            string versionString = informationalVersion.ToString();
            return versionString;
        }

        private void SaveDataTab()
        {
            try
            {
                Properties.Settings.Default.TagPattern = tSearch.Text;

                Properties.Settings.Default.Save();

                OpenARIANA.ProfileManager.Instance.UpdateProfileManagerSettings();

                MessageBox.Show("Data Transformation settings saved.", "Process Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Invalid verbosity level.");
            }

        }

        private void SaveGeneralTab()
        {
            try
            {
                // currently only verbosity level to be saved
                Properties.Settings.Default.VerbosityLevel = cVerbosity.SelectedIndex;
                Properties.Settings.Default.Save();

                // update the logger
                Utilities.Logger.UpdateLoggerSettings();
                MessageBox.Show("General settings saved.", "Process Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Invalid verbosity level.");
            }
        }

        // Save currently active tab.
        private void btnSave_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    SaveDataTab();
                    break;
                case 1:
                    SaveGeneralTab();
                    break;
                default:
                    MessageBox.Show("Nothing has been saved.");
                    break;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CloseDialog();
        }

        private void btnOpenLogs_Click(object sender, EventArgs e)
        {
            string logFilePath = Utilities.Logger.LogFilePath;
            try
            {
                Process.Start(logFilePath);
            }
            catch (Exception ex)
            {
                // Handle the exception
                MessageBox.Show("Unable to open the log file. " + ex.Message);
            }
        }

        private void btnFileBrowser_Click(object sender, EventArgs e)
        {
            // Open the folder browser dialog
            DialogResult result = folderBrowserDialog.ShowDialog();

            // If the user selected a folder, update settings, move folder content to new path, and update text box
            if (result == DialogResult.OK)
            {
                string selectedPath = folderBrowserDialog.SelectedPath;

                Properties.Settings.Default.CustomProfileDir = selectedPath;

                OpenARIANA.ProfileManager.Instance.ChangeProfileDirectory(selectedPath);
                OpenARIANA.ProfileManager.Instance.UpdateProfileManagerSettings();
                tProfilePath.Text = selectedPath;
            }
        }

        private void btnManageProfile_Click(object sender, EventArgs e)
        {
            string profileName = cProfiles.SelectedItem.ToString();

            ProfileManager profileManager = OpenARIANA.ProfileManager.Instance;

            string _filePath = profileManager.GetProfileFilePath(profileName);

            try
            {
                Process.Start(_filePath);
            }
            catch (Exception ex)
            {
                // Handle the exception
                MessageBox.Show("Unable to open the log file. " + ex.Message);
            }
        }
    }
}
