using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using OpenARIANA.Parser;
using OpenARIANA.Writer;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace OpenARIANA.Processor
{
    public class BasicProcessor : IProcessor
    {
        Writer.InterOpWriter _writer;
        Parser.ExcelParser _parser;
        public BasicProcessor()
        {
            _writer = new InterOpWriter();
            _parser = new ExcelParser(_writer);
        }
        public void Process(string inputFilePath, string outputFilePath = null)
        {
        }
        /// <summary>
        /// Overload Process
        /// </summary>
        public void Process(string inputFilePath, string label, string outputFilePath)
        {
            Word.Application app = Globals.ThisAddIn.Application;
            app.ScreenUpdating = false;

            try
            {
                using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(inputFilePath, false))
                {
                    WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                    WorksheetPart worksheetPart = GetWorksheetPart(workbookPart, label);

                    Utilities.Logger.LogInfo("Retrieving Style Information ...");
                    List<string> requiredStyles = _parser.GetStyleInfo(workbookPart, worksheetPart);
                    List<string> missingStyles = CheckStyleAvailability(requiredStyles);

                    if (missingStyles.Count != 0)
                    {
                        DialogResult result = MessageBox.Show($"Styles Missing in active document: \n- {string.Join("\n- ", missingStyles)}\n\n Still proceed?",
                            "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (result != DialogResult.Yes)
                        {
                            Utilities.Logger.LogInfo("Missing Styles detected. Process aborted by user.");
                            return;
                        }
                    }
                    Utilities.Logger.LogInfo("Styles OK or Warning ignored.");

                    _parser.Parse(workbookPart, worksheetPart);
                }

            }
            catch (Exception ex)
            {
                Utilities.Logger.LogInfo($"{ex.Message}");
            }
            finally
            {
                app.ScreenUpdating = true;
                Utilities.Logger.LogInfo("File successfully processed.");
            }
        }
        private WorksheetPart GetWorksheetPart(WorkbookPart workbookPart, string worksheetLabel)
        {
            WorksheetPart worksheetPart = null;

            foreach (Sheet sheet in workbookPart.Workbook.Sheets)
            {
                if (sheet.Name == worksheetLabel)
                {
                    worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                    break;
                }
            }

            return worksheetPart;
        }

        private List<string> CheckStyleAvailability(List<string> styles)
        {
            List<string> missingStyles = new List<string>();
            Word.Document activeDocument = Globals.ThisAddIn.Application.ActiveDocument;

            foreach (string styleName in styles)
            {
                try
                {
                    // Attempt to access the style by name
                    Word.Style style = activeDocument.Styles[styleName];
                }
                catch (COMException)
                {
                    // catch key error -> style not available in document.
                    missingStyles.Add(styleName);
                }
                catch (Exception ex)
                {
                    Utilities.Logger.LogError($"Error trying to access styles: {ex.ToString()}");
                }
            }
            return missingStyles;
        }
    }
}
