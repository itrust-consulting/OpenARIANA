using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OpenARIANA.Parser
{
    public class ExcelParser
    {
        private Writer.InterOpWriter _writer;

        public ExcelParser(Writer.InterOpWriter writer)
        {
            _writer = writer;
        }

        public List<string> GetStyleInfo(WorkbookPart workbookPart, WorksheetPart worksheetPart)
        {
            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().FirstOrDefault();
            SharedStringTable sharedStringTable = workbookPart.SharedStringTablePart.SharedStringTable;
            List<string> columnStyles = new List<string>();


            // Assuming the first row contains the style mappings
            Row styleRow = sheetData?.Elements<Row>().FirstOrDefault();
            if (styleRow != null)
            {
                foreach (Cell cell in styleRow.Elements<Cell>())
                {
                    string cellValue = GetCellValue(cell, sharedStringTable);
                    columnStyles.Add(cellValue);
                }
            }

            return columnStyles;
        }

        public void Parse(WorkbookPart workbookPart, WorksheetPart worksheetPart)
        {

            SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().FirstOrDefault();
            SharedStringTable sharedStringTable = workbookPart.SharedStringTablePart.SharedStringTable;
            Dictionary<string, string> columnStyles = new Dictionary<string, string>();


            // Assuming the first row contains the style mappings
            Row styleRow = sheetData?.Elements<Row>().FirstOrDefault();
            if (styleRow != null)
            {
                foreach (Cell cell in styleRow.Elements<Cell>())
                {
                    string cellValue = GetCellValue(cell, sharedStringTable);
                    string columnIndex = GetColumnIndex(cell.CellReference);
                    columnStyles[columnIndex] = cellValue;
                }
            }

            // Parse subsequent rows and apply styles
            foreach (Row row in sheetData.Elements<Row>())
            {
                if (row.RowIndex > 1) // Skip the first row with style mappings
                {
                    foreach (Cell cell in row.Elements<Cell>())
                    {
                        string cellText = GetCellValue(cell, sharedStringTable);
                        if (cellText != "")
                        {
                            string columnIndex = GetColumnIndex(cell.CellReference);
                            string cellStyle = columnStyles.ContainsKey(columnIndex) ? columnStyles[columnIndex] : "Body Of Text";

                            // Add text to the Word document with the corresponding style
                            _writer.Write(cellText, cellStyle);
                        }
                    }
                }
            }
        }

        private string GetCellValue(Cell cell, SharedStringTable sharedStringTable)
        {
            // Excel maintains a shared string table/dict that stores all unique text values used within the spreadsheet. Cells that contain
            // the same text value only contain the reference to the unique text value stored in the table. 
            // Assuming the cell contains a shared string (text), you can retrieve it like this:
            string value = null;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                if (sharedStringTable.Elements<SharedStringItem>().Count() > 0)
                {
                    int sharedStringIndex = int.Parse(cell.InnerText);
                    SharedStringItem sharedStringItem = sharedStringTable.Elements<SharedStringItem>().ElementAt(sharedStringIndex);
                    value = sharedStringItem.Text?.Text;
                }
            }
            else
            {
                // If the cell contains simple text (not shared string), retrieve text directly
                value = cell.InnerText;
            }
            if (!string.IsNullOrEmpty(value))
            {
                value = value.Replace("_x000B_", "\r\n").Replace("&#10;", "\n").Replace("&#13;", "\r");
            }

            return value;
        }

        private string GetColumnIndex(string cellReference)
        {
            // Returns Column Index for cellReferences, e.g., AA1 -> AA
            string pattern = @"[A-Za-z]+";
            Match match = Regex.Match(cellReference, pattern);

            if (match.Success)
            {
                string columnIndex = match.Value;
                return columnIndex;
            }

            // No match -> shouldnt happen
            return null;
        }
    }
}
