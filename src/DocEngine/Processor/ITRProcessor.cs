using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Office.Interop.Word;
using OpenARIANA.Parser;
using OpenARIANA.Writer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace OpenARIANA.Processor
{
    public class ItrustProcessor : IProcessor
    {
        private readonly string _name = "itrust Processor";
        private readonly HashSet<string> missingStyles = new HashSet<string>();



        object missing = System.Reflection.Missing.Value;
        readonly string[,] tagsAndStyles = new string[31, 2] {{"*§T1", "TabText1"},
                                                        {"*§T2", "TabText2"},
                                                        {"*§H1", "TabHeader1"},
                                                        {"*§H2", "TabHeader2"},
                                                        {"*§L1", "TabBulletL1"},
                                                        {"*§L2", "TabBulletL2"},
                                                        {"*§L3", "TabBulletL3"},
                                                        {"*§N1", "TabEnumL1"},
                                                        {"*§N2", "TabEnumL2"},
                                                        {"*§N3", "TabEnumL3"},
                                                        {"*§B1", "Bullet L1"},
                                                        {"*§B2", "Bullet L2"},
                                                        {"*§B3", "Bullet L3"},
                                                        {"*§B4", "Bullet L4"},
                                                        {"*§C",  "Caption"},
                                                        {"*§H4", "Heading 4"},
                                                        {"*§H5", "Heading 5"},
                                                        {"*§OB", "SP-OtherInfoBullet"},
                                                        {"*§OW", "SP-Owner"},
                                                        {"*§QN", "SP-Question"},
                                                        {"*§QE", "SP-Quote"},
                                                        {"*§HI", "Hidden"},
                                                        {"*§MA", "Mandatory"},
                                                        {"*§MB", "SP-ImplementationBullet"},
                                                        {"*§IB", "SP-InputBullet"},
                                                        {"*§AB", "SP-ActionBullet"},
                                                        {"*§TB", "SP-TriggerBullet"},
                                                        {"*§EB", "SP-OutputBullet"},
                                                        {"*§SB1", "SP-BulletL1"},
                                                        {"*§SB2", "SP-BulletL2"},
                                                        {"*§SB3", "SP-BulletL3"}};
        readonly string[,] tagsAndStylesWithEndingTags = new string[6, 2] {
                                                        {"*&B1", "SP-Block"},
                                                        {"*&D1", "SP-Bold"},
                                                        {"*&W1", "SP-Specific1"},
                                                        {"*&W2", "SP-Specific2"},
                                                        {"*&HI", "Hidden Char"},
                                                        {"*&MA", "Mandatory Char"} };
        readonly Dictionary<string, string> numberedTagsAndStyles = new Dictionary<string, string> {
                                                        { "*§E1", "Enumeration L1"},
                                                        { "*§E2", "Enumeration L2"},
                                                        { "*§E3", "Enumeration L3"},
                                                        { "*§E4", "Enumeration L4"},
                                                        {"*§SE1", "SP-EnumL1"},
                                                        {"*§SE2", "SP-EnumL2"},
                                                        {"*§SE3", "SP-EnumL3"}};
        Word.Document _document;

        public string Name
        {
            get { return _name; }
        }

        public HashSet<string> MissingStyles
        {
            get { return missingStyles; }
        }

        public ItrustProcessor()
        {
            _document = Globals.ThisAddIn.Application.ActiveDocument;
        }

        public void Process(string inputFilePath, string outputFilePath) { }
        // overload Process for now.
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

                    InterOpWriter writer = new InterOpWriter();
                    ExcelParser parser = new ExcelParser(writer);
                    parser.Parse(workbookPart, worksheetPart);
                }
                postProcessingEndLists(_document);

                for (int i = 0, limit = (tagsAndStyles.Length / 2); i < limit; i++)
                {
                    postProcessing(_document, tagsAndStyles[i, 0], tagsAndStyles[i, 1]);
                }

                for (int i = 0, limit = (tagsAndStylesWithEndingTags.Length / 2); i < limit; i++)
                {
                    postProcessingEndingTags(_document, tagsAndStylesWithEndingTags[i, 0], tagsAndStylesWithEndingTags[i, 0] + "Ed", tagsAndStylesWithEndingTags[i, 1]);
                }

                addNumberedList(_document, numberedTagsAndStyles);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while parsing the worksheet: {ex.Message}");
            }
            finally
            {
                app.ScreenUpdating = true;
                if (missingStyles.Count != 0)
                {
                    string result = string.Join("\n", missingStyles);
                    MessageBox.Show($"Couldn't find styles:\n{result}");
                }
            }
        }

        private WorksheetPart GetWorksheetPart(WorkbookPart workbookPart, string worksheetLabel)
        {
            // Implement logic to find and return the worksheet part based on the label
            // You can iterate through the sheets and match the label to the worksheet name
            WorksheetPart worksheetPart = null;

            foreach (Sheet sheet in workbookPart.Workbook.Sheets.OfType<Sheet>())
            {
                if (sheet.Name == worksheetLabel)
                {
                    worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                    break;
                }
            }

            return worksheetPart;
        }

        // Append Postprocessing steps.
        void postProcessing(Word.Document document, object findText, string style)
        {
            Word.Range wholeDocument = document.Content;
            Word.Find find = wholeDocument.Find;

            find.ClearFormatting();
            while (wholeDocument.Find.Execute(ref findText,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing))
            {
                wholeDocument.Select();
                Word.Selection currentSelection = Globals.ThisAddIn.Application.Selection;
                try
                {
                    currentSelection.Text = "";
                    currentSelection.EndOf(Word.WdUnits.wdParagraph, Word.WdMovementType.wdExtend);
                    currentSelection.Text = currentSelection.Text.Trim() + "\n";
                    currentSelection.set_Style(style);
                }
                catch (COMException)
                {
                    missingStyles.Add(style);
                    Utilities.Logger.LogWarning($"<ItrustProcessor.postProcessing> Style {style} not found for {findText}");
                }

            }
        }
        void postProcessingEndLists(Word.Document document)
        {
            List<string> endTags = new List<string> { "*§EL", "*§SEL" };

            foreach (string tag in endTags)
            {
                Word.Range wholeDocument = document.Content; // Reset the range for each tag
                wholeDocument.Find.ClearFormatting();
                object findText = tag;
                var style = tag == "*§EL" ? "End list" : "SP-EndList";

                while (wholeDocument.Find.Execute(FindText: ref findText,
                    MatchCase: false, MatchWholeWord: true, MatchWildcards: false,
                    MatchSoundsLike: ref missing, MatchAllWordForms: false,
                    Forward: true, Wrap: WdFindWrap.wdFindStop,
                    Format: false, ReplaceWith: ref missing, Replace: WdReplace.wdReplaceNone))
                {
                    wholeDocument.Select();
                    Word.Selection currentSelection = Globals.ThisAddIn.Application.Selection;
                    currentSelection.Range.MoveEnd(Word.WdUnits.wdParagraph, 1);
                    currentSelection.set_Style(style);
                    currentSelection.Text = ""; //The end list is an empty paragraph with tiny margins and padding.
                }
            }
        }

        private void postProcessingEndingTags(Word.Document document, object findText, object findEndText, string style)
        {
            // Define the range in which to conduct the search
            Word.Range wholeDocument = document.Content;

            Word.Range textToFormat;

            Word.Range endTagPosition = null;
            Word.Find find = wholeDocument.Find;

            find.ClearFormatting();

            while (wholeDocument.Find.Execute(ref findText,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing))

            {
                try
                {
                    textToFormat = wholeDocument.Duplicate;
                    endTagPosition = wholeDocument.Duplicate;
                    wholeDocument.Text = "";

                    endTagPosition.Find.Execute(ref findEndText,
                        ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing);

                    endTagPosition.Text = "";
                    textToFormat.End = endTagPosition.End;
                    textToFormat.Select();
                    textToFormat.set_Style(style);
                }
                catch (COMException)
                {
                    missingStyles.Add(style);
                    Utilities.Logger.LogWarning($"<ItrustProcessor.postProcessing> Style {style} not found for {findEndText}");
                }
            }
        }


        Word.ListTemplate findListTemplateByStyleName(string style)
        {
            if (!String.IsNullOrEmpty(style))
            {
                foreach (ListTemplate item in _document.ListTemplates)
                {
                    if (!item.OutlineNumbered)
                        continue;

                    foreach (ListLevel lvl in item.ListLevels)
                    {
                        if (lvl.LinkedStyle == style)
                        {
                            return item;
                        }
                    }

                }
            }
            return null;
        }


        void addNumberedList(Word.Document document, Dictionary<string, string> tags)
        {
            // Declares a pointer to the application
            Word.Application app = Globals.ThisAddIn.Application;

            // The type of numbering style to be used in the list
            object index = 1;

            bool continueList;
            // Declare the numbering template to be used
            Word.ListTemplate defaultTemplate =
                app.ListGalleries[Word.WdListGalleryType.wdOutlineNumberGallery].ListTemplates.get_Item(ref index);


            for (int i = 1; i <= document.Paragraphs.Count; i++)
            {
                // Get the current paragraph as a string

                // Get the range of the current paragraph
                Word.Range currentRange = document.Paragraphs[i].Range;

                // The tags are 4 characters long. Hence, strings shorter than that should not be checked.
                if (currentRange.Text.Trim().Length > 4)
                {
                    // Get the initial 4 letters of the paragraph
                    string style, fullText, initialCharacters;

                    // If the initial 5 letters are in tags
                    if (findCurrentRangeStyle(currentRange, out style, out fullText, out initialCharacters))
                    {
                        //Search the custom enumeration List
                        Word.ListTemplate template = findListTemplateByStyleName(style);

                        if (fullText.EndsWith("@s"))
                        {
                            currentRange.Text = currentRange.Text.Replace(fullText, "").TrimStart();
                            continueList = false;
                        }
                        else
                        {
                            currentRange.Text = currentRange.Text.Replace(initialCharacters, "").TrimStart();
                            continueList = true;
                        }

                        currentRange.set_Style(style);

                        if (!continueList)
                            currentRange.ListFormat.ApplyListTemplate(template == null ? defaultTemplate : template,
                                                                               ContinuePreviousList: false,
                                                                               ApplyTo: Word.WdListApplyTo.wdListApplyToWholeList,
                                                                               Word.WdDefaultListBehavior.wdWord10ListBehavior);



                    }

                }
            }

            bool findCurrentRangeStyle(Word.Range currentRange, out string style, out string fullText, out string initialCharacters)
            {
                string text = currentRange.Text.Trim();

                //Check for SP block L1 @s
                fullText = text.Length > 8 ? text.Substring(0, 8) : text;

                if (!fullText.EndsWith("@s") && fullText.Length > 7)//check for L1 @S
                    fullText = text.Substring(0, 7);

                initialCharacters = fullText.Length > 5 ? fullText.Substring(0, 5) : fullText; //check SP block List
                if (!tags.TryGetValue(initialCharacters, out style))
                {
                    initialCharacters = initialCharacters.Length> 4? initialCharacters.Substring(0, 4): initialCharacters;// check for List
                    if (!tags.TryGetValue(initialCharacters, out style))
                        return false;
                }
                return true;
            }
        }
    }
}
