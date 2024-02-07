using Microsoft.Office.Tools.Ribbon;
using System.Windows.Forms;


namespace OpenARIANA
{
    public partial class OpenARIANARibbonMenu
    {
        private void OpenARIANARibbonMenu_Load(object sender, RibbonUIEventArgs e)
        {

        }
        private void btnLoadData_Click(object sender, RibbonControlEventArgs e)
        {
            string filter = "All files(*.*) | *.*";
            RibbonControl control = sender as RibbonControl;
            if (control != null)
            {
                string controlId = control.Id;
                switch (controlId)
                {
                    // add other supported formats in future.
                    case "sbtnLoadData":
                        filter = "(*xlsm;*.xlsx;*.xlsb;*.xls)|*xlsm;*.xlsx;*.xlsb;*.xls";
                        break;
                    default:
                        break;
                }
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = filter;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                ShowImportDialog(selectedFilePath); // Proceed to show the ImportDialog
            }
        }
        private void ShowImportDialog(string filePath)
        {
            ImportDialog importDialog = new ImportDialog(filePath);
            importDialog.OpenDialog();
        }


        private void btnExport_Click(object sender, RibbonControlEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "(*xlsm;*.xlsx;*.xlsb;*.xls)|*xlsm;*.xlsx;*.xlsb;*.xls";
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.AddExtension = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // hook up to a WriterFactory (see Processors). Set up Chain of responsibility.
                string exportPath = saveFileDialog.FileName;
            }
        }

        private void btnReplaceTags_Click(object sender, RibbonControlEventArgs e)
        {
            var activeWindow = Globals.ThisAddIn.Application.ActiveWindow;
            if (activeWindow != null)
            {
                // Check if the TaskPane is already initialized for the active window
                if (!Globals.ThisAddIn.IsTaskPaneInitializedForWindow(activeWindow))
                {
                    // Initialize the TaskPane if not already done
                    Globals.ThisAddIn.InitializeTaskPane(activeWindow);
                }

                // Toggle the TaskPane visibility
                Globals.ThisAddIn.ToggleCustomPane();
            }
        }

        private void btnSettings_Click(object sender, RibbonControlEventArgs e)
        {
            SettingsMenu settingsMenu = new SettingsMenu();
            settingsMenu.ShowDialog();
        }
    }
}
