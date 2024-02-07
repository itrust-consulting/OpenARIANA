using Microsoft.Office.Tools;
using System.Collections.Generic;
using System.Linq;
using Word = Microsoft.Office.Interop.Word;


namespace OpenARIANA
{

    public partial class ThisAddIn
    {
        private Dictionary<Word.Window, CustomTaskPane> panesPerWindow = new Dictionary<Word.Window, CustomTaskPane>();

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            Utilities.Logger.LogSystem("OpenARIANA Add-in startup.");

            this.Application.WindowActivate += new Word.ApplicationEvents4_WindowActivateEventHandler(Application_WindowActivate);
            this.Application.DocumentBeforeClose += new Word.ApplicationEvents4_DocumentBeforeCloseEventHandler(Application_DocumentBeforeClose);
        }


        void Application_WindowActivate(Word.Document Doc, Word.Window Wn)
        {
            int windowHandle = Wn.Hwnd;
            Utilities.Logger.LogSystem($"Window Activate - Window Handle: {windowHandle}");
            Word.Window window = Doc.ActiveWindow;
            InitializeTaskPane(window);
        }

        void Application_DocumentBeforeClose(Word.Document Doc, ref bool Cancel)
        {
            int windowHandle = 0;
            if (Doc.ActiveWindow != null)
            {
                windowHandle = Doc.ActiveWindow.Hwnd;

                // Remove and dispose the task pane associated with this window
                if (panesPerWindow.TryGetValue(Doc.ActiveWindow, out CustomTaskPane taskPane))
                {
                    this.CustomTaskPanes.Remove(taskPane);
                    taskPane.Dispose();
                    panesPerWindow.Remove(Doc.ActiveWindow);

                    Utilities.Logger.LogSystem($"Removed Task Pane for Window Handle: {windowHandle}");
                }
            }
            Utilities.Logger.LogSystem($"Closing Document - Window Handle: {windowHandle}");
        }

        public void InitializeTaskPane(Word.Window window)
        {
            int windowHandle = window.Hwnd;
            if (!panesPerWindow.ContainsKey(window))
            {
                Utilities.Logger.LogSystem($"Initialize new Task Pane for Window Handle: {windowHandle}");
                TagTaskPane myTaskPane = new TagTaskPane();
                CustomTaskPane myCustomTaskPane = this.CustomTaskPanes.Add(myTaskPane, "OpenARIANA", window);
                myCustomTaskPane.DockPosition = Microsoft.Office.Core.MsoCTPDockPosition.msoCTPDockPositionRight;
                myCustomTaskPane.Width = 250;
                myCustomTaskPane.Visible = false;
                panesPerWindow[window] = myCustomTaskPane;
            }
            else
            {
                Utilities.Logger.LogSystem($"Task Pane already exists for Window Handle: {windowHandle}");
            }
        }

        public bool IsTaskPaneInitializedForWindow(Word.Window window)
        {
            return panesPerWindow.ContainsKey(window);
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            Utilities.Logger.LogSystem("OpenARIANA Add-in shutdown.");
        }


        public void ToggleCustomPane(bool open = false)
        {
            var activeWindow = Globals.ThisAddIn.Application.ActiveWindow;
            if (activeWindow != null && panesPerWindow.TryGetValue(activeWindow, out CustomTaskPane taskPane))
            {
                int windowHandle = activeWindow.Hwnd;
                Utilities.Logger.LogSystem($"Toggling Task Pane for Window Handle: {windowHandle}");

                CustomTaskPane customPane = this.CustomTaskPanes.FirstOrDefault(ctp => ctp.Window == activeWindow);
                if (customPane != null)
                {
                    customPane.Visible = open || !customPane.Visible;
                }
                else
                {
                    Utilities.Logger.LogSystem($"No Task Pane found to toggle for Window Handle: {windowHandle}");
                }
            }
        }
        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion
    }
}
