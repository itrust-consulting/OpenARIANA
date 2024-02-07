using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace OpenARIANA
{

    public partial class ImportDialog : Form
    {
        private string _filePath;
        private bool appendToFile = true;

        public void OpenDialog()
        {

            tFilePath.Text = _filePath;
            if (IsExcelfile(_filePath))
            {
                //Update the dropdown with worksheet labels
                List<string> worksheetLabels = GetExcelWorksheetsLabels(_filePath);

                dropDownWorkSheets.Items.Clear();
                foreach (string label in worksheetLabels)
                {
                    dropDownWorkSheets.Items.Add(label);
                }

                // Set the selected index to the first worksheet (if available)
                if (worksheetLabels.Count > 0)
                {
                    dropDownWorkSheets.SelectedIndex = 0;
                }
            }
            else
            {
                dropDownWorkSheets.Enabled = false;
            }

            this.ShowDialog();
        }
        private void CloseDialog()
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void LoadData()
        {

            List<string> excelProcessors = new List<string> { "Basic Processor", "itrust Processor" };

            Word.Application app = Globals.ThisAddIn.Application;
            app.UndoRecord.StartCustomRecord("Load Data");

            if (!appendToFile)
            {
                ClearActiveDocument();
                Utilities.Logger.LogInfo($"Cleared active document.");
            }

            string selectedProcessor = GetParserName();

            if (!excelProcessors.Contains(selectedProcessor))
            {
                try
                {
                    Processor.IProcessor processor = Processor.ProcessorFactory.GetProcessor(selectedProcessor, _filePath, "");

                    Utilities.Logger.LogInfo($"Processor {selectedProcessor} initialilzed. Processing data ...");
                    processor.Process(_filePath, "");
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("Selected Processor not found. Please choose a different Processor.",
                        "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Utilities.Logger.LogError($"Processor {selectedProcessor} not found.");
                    return;
                }
            }
            else
            {
                if (!IsExcelfile(_filePath))
                {
                    MessageBox.Show("Input file must be an Excel Workbook. To import from other file formats please use the 'InteropProcessor (experimental)' processor.");
                    Utilities.Logger.LogError($"Invalid file format. File cannot be processed.");
                    app.UndoRecord.EndCustomRecord();
                    return;
                }

                Processor.IProcessor processor = Processor.ProcessorFactory.GetProcessor(selectedProcessor, _filePath, "");
                Utilities.Logger.LogInfo($"Processor {selectedProcessor} initialilzed. Processing data ...");

                processor.Process(_filePath, dropDownWorkSheets.SelectedItem.ToString(), "");
            }

            app.UndoRecord.EndCustomRecord();

            Globals.ThisAddIn.ToggleCustomPane(true);
            CloseDialog();
        }

        private bool IsExcelfile(string filePath)
        {
            // Get the file extension
            string extension = Path.GetExtension(filePath).ToLower();

            // List of common Excel file extensions
            var excelExtensions = new HashSet<string> { ".xlsx", ".xls", ".xlsm", ".xlsb" };

            // Check if the file extension is one of the Excel formats
            return excelExtensions.Contains(extension);
        }

        // Retrieve UI info
        private string GetParserName()
        {
            try
            {
                string name = ParserSelection.CheckedItems[0].ToString();
                return name;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private List<string> GetExcelWorksheetsLabels(string excelFilePath)
        {
            List<string> worksheetLabels = new List<string>();

            try
            {
                using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(excelFilePath, false))
                {
                    WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                    Sheets sheets = workbookPart.Workbook.Sheets;

                    foreach (Sheet sheet in sheets)
                    {
                        worksheetLabels.Add(sheet.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., file not found, invalid format)
                MessageBox.Show($"An error occurred while reading Excel file: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return worksheetLabels;
        }

        private void ClearActiveDocument()
        {
            Word.Document document = Globals.ThisAddIn.Application.ActiveDocument;

            if (document != null)
            {
                Word.Range docRange = document.Content;
                docRange.Delete();
            }
            else
            {
                MessageBox.Show("No active document found.");
            }
        }

        // EventHandlers
        private void ImportDialog_OnLoad(object sender, EventArgs e)
        {
            try
            {
                // Get active Word window position and size
                Word.Window wordWindow = Globals.ThisAddIn.Application.ActiveWindow;

                // Calculate center position for the dialog
                int centerX = wordWindow.Left + (wordWindow.Width / 2) - (this.Width / 2);
                int centerY = wordWindow.Top + (wordWindow.Height / 2) - (this.Height / 2);

                // Set dialog start position and location
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new System.Drawing.Point(centerX, centerY);
            }
            catch (Exception ex)
            {
                Utilities.Logger.LogWarning($"Not able to position Dialog Window: {ex.ToString()}");
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            CloseDialog();
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void bAppendToFile_Click(object sender, EventArgs e)
        {
            appendToFile = bAppendToFile.Checked;
        }
        private void ParserSelection_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                CheckedListBox clb = sender as CheckedListBox;
                // Uncheck all other items.
                for (int i = 0; i < clb.Items.Count; i++)
                {
                    if (i != e.Index)
                    {
                        clb.SetItemChecked(i, false);
                    }
                }
            }
        }

    }
}
