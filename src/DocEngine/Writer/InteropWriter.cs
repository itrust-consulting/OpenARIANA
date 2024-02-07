using System.Collections.Generic;
using System.Runtime.InteropServices;
using Word = Microsoft.Office.Interop.Word;


namespace OpenARIANA.Writer
{
    public class InterOpWriter
    {
        private HashSet<string> missingStyles = new HashSet<string>();
        private Word.Document _document;

        public HashSet<string> MissingStyles
        {
            get { return missingStyles; }
        }

        public InterOpWriter()
        {
            _document = Globals.ThisAddIn.Application.ActiveDocument;
        }

        public void Write(string text, string style)
        {
            _document.Content.Paragraphs.Add();
            Word.Paragraph para = _document.Content.Paragraphs.Last;
            try
            {
                // Apply the specified style
                if (!string.IsNullOrEmpty(style))
                {
                    para.Range.set_Style(style);
                }
                else
                {
                    style = "Normal"; // Default style if none is specified
                    para.Range.set_Style(style);
                }
            }
            catch (COMException)
            {
                string dstyle = "Normal"; // Default style if none is specified
                para.Range.set_Style(dstyle);
                missingStyles.Add(dstyle);
                Utilities.Logger.LogWarning($"Style {style} not found in active document. Set to default '{dstyle}'.");
            }
            para.Range.Text = text;
        }

    }
}
