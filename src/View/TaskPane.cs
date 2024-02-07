using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace OpenARIANA
{
    public partial class TagTaskPane : UserControl
    {
        public TagTaskPane()
        {
            InitializeComponent();
            UpdateProfileDropdown();
        }

        private Dictionary<string, string> FindTagsInDocument()
        {
            Dictionary<string, string> tagValuePairs = new Dictionary<string, string>();

            string pattern = ProfileManager.Instance.TagPattern;
            Utilities.Logger.LogInfo($"Find tags for pattern {pattern}.");
            Word.Document document = Globals.ThisAddIn.Application.ActiveDocument;
            MatchCollection matches = Regex.Matches(document.Content.Text, pattern);

            foreach (Match match in matches)
            {
                string tag = match.Value;
                if (!tagValuePairs.ContainsKey(tag))
                {
                    tagValuePairs.Add(tag, "");
                }
            }
            return tagValuePairs;
        }
        private void UpdateProfileDropdown()
        {
            // Clear existing items
            dProfileSelection.Items.Clear();

            List<string> profileNames = ProfileManager.Instance.GetAllProfileNames();
            if (profileNames.Count == 0) { return; }

            foreach (var profileName in profileNames)
            {
                dProfileSelection.Items.Add(profileName);
            }
        }
        private void AddToGridView(Dictionary<string, string> tagValuePairs)
        {
            gTagReplacementList.Rows.Clear();

            // Sorting the dictionary. Empty replacements come first, then non-empty, both sorted alphabetically by tag.
            var sortedTagValuePairs = tagValuePairs.OrderByDescending(kvp => string.IsNullOrEmpty(kvp.Value))
                                                   .ThenBy(kvp => kvp.Key);

            foreach (var kvp in sortedTagValuePairs)
            {
                gTagReplacementList.Rows.Add(kvp.Key, kvp.Value);
            }
        }

        private Dictionary<string, string> ReadFromGridView()
        {
            Dictionary<string, string> tagValuePairs = new Dictionary<string, string>();

            foreach (DataGridViewRow row in gTagReplacementList.Rows)
            {
                if (row.Cells[0].Value != null) // Check only the first cell for null as the second cell can be empty
                {
                    string tag = row.Cells[0].Value.ToString();
                    string replacement = row.Cells[1].Value?.ToString() ?? ""; // If second cell is null, use empty string to avoid NullReference
                    tagValuePairs[tag] = replacement;
                }
            }
            return tagValuePairs;
        }

        private void ReplaceTagInDocument(string tag, string replacement, Word.Document document)
        {
            if (document.ProtectionType != Word.WdProtectionType.wdNoProtection)
            {
                MessageBox.Show("Document is protected.");
                return;
            }

            if (IsURL(replacement))
            {
                InsertHyperlink(tag, replacement, document);
            }
            else
            {
                ReplaceText(tag, replacement, document);
            }
        }

        private bool IsURL(string input)
        {
            return Uri.TryCreate(input, UriKind.Absolute, out Uri uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        private void ReplaceText(string text, string replacement, Word.Document document)
        {
            Word.Range range = document.Content;
            Word.Find find = range.Find;
            find.ClearFormatting();
            find.Text = text;
            find.Replacement.ClearFormatting();
            find.Replacement.Text = replacement;
            object replaceAll = Word.WdReplace.wdReplaceAll;


            find.Execute(FindText: Type.Missing, MatchCase: true, MatchWholeWord: true,
                         MatchWildcards: false, MatchSoundsLike: Type.Missing,
                         MatchAllWordForms: false, Forward: true,
                         Wrap: Word.WdFindWrap.wdFindContinue, Format: false,
                         ReplaceWith: Type.Missing, Replace: replaceAll);
        }

        private void InsertHyperlink(string text, string url, Word.Document document)
        {
            Word.Range range = document.Content;
            Word.Find find = range.Find;
            find.ClearFormatting();
            find.Text = text;

            while (find.Execute(FindText: text, MatchCase: true, MatchWholeWord: true))
            {
                // Insert the hyperlink at the found location
                document.Hyperlinks.Add(range, url, Missing.Value, Missing.Value, url, Missing.Value);

                // Move the range forward to avoid an endless loop
                range.SetRange(range.End, document.Content.End);
            }
        }

        private (Dictionary<string, string>, List<string> MissingTags) ApplyProfile(Dictionary<string, string> tagValuePairs,
            Dictionary<string, string> profileTagValuePairs)
        {
            List<string> missingTags = new List<string>();

            foreach (string tag in tagValuePairs.Keys.ToList())
            {
                if (profileTagValuePairs.ContainsKey(tag))
                {
                    // Update tagValuePairs with the value from the profile
                    tagValuePairs[tag] = profileTagValuePairs[tag];
                }
                else
                {
                    // Add to missing tags list and optionally set a default value in tagValuePairs
                    missingTags.Add(tag);
                    tagValuePairs[tag] = "";
                }
            }
            return (tagValuePairs, missingTags);
        }

        private bool SaveProfile(string profileName, Dictionary<string, string> tagValuePairs)
        {
            bool createdNewProfile = ProfileManager.Instance.SaveProfile(profileName, tagValuePairs);
            return createdNewProfile;
        }

        private bool DeleteProfile(string profileName)
        {
            try
            {
                ProfileManager.Instance.DeleteProfile(profileName);
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
            return true;
        }


        // Handlers
        private void btnFindTags_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> tagValuePairs = FindTagsInDocument();
            AddToGridView(tagValuePairs);
        }

        private void btnReplaceFromGrid_Click(object sender, EventArgs e)
        {
            // get data from GridView
            Dictionary<string, string> tagValuePairs = ReadFromGridView();

            // Filter the dictionary to include only those pairs where the replacement is not an empty string
            Dictionary<string, string> filteredTagValuePairs = tagValuePairs
                .Where(kvp => !string.IsNullOrEmpty(kvp.Value))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            Word.Application app = Globals.ThisAddIn.Application;
            Word.Document document = app.ActiveDocument;

            int replacementCount = 0;

            app.ScreenUpdating = false;
            //app.UndoRecord.StartCustomRecord("Replace All Tags");

            foreach (string tag in filteredTagValuePairs.Keys.ToList())
            {
                string replacement = filteredTagValuePairs[tag];
                ReplaceTagInDocument(tag, replacement, document);
                replacementCount++;
            }

            app.ScreenUpdating = true;
            //app.UndoRecord.EndCustomRecord();

            MessageBox.Show($"{replacementCount} tag(s) replaced.",
                "Process Complete",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnApplyProfile_Click(object sender, EventArgs e)
        {
            if (dProfileSelection != null)
            {
                List<string> missingTags = new List<string>();
                string profileName = dProfileSelection.Text;
                try
                {
                    Dictionary<string, string> profileTagValuePairs = ProfileManager.Instance.GetProfile(profileName);
                    Dictionary<string, string> tagValuePairs = FindTagsInDocument();
                    (tagValuePairs, missingTags) = ApplyProfile(tagValuePairs, profileTagValuePairs);
                    AddToGridView(tagValuePairs);

                    if (missingTags?.Any() == true)
                    {
                        MessageBox.Show("Tag replacement(s) missing! Please handle before proceeding.",
                            "Warning",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
                catch (KeyNotFoundException)
                {
                    MessageBox.Show($"Profile '{profileName}' not found!",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    UpdateProfileDropdown();
                    return;
                }
            }

        }

        private void btnSaveProfile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dProfileSelection.Text))
            {
                MessageBox.Show("No Profile specified.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            Dictionary<string, string> tagValuePairs = ReadFromGridView();

            // check if all tags have a replacement
            if (tagValuePairs.Any(kvp => string.IsNullOrEmpty(kvp.Value)))
            {
                DialogResult result = MessageBox.Show("Tag replacement(s) missing!\nStill Proceed?",
                    "Warning",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.Yes) { return; }
            }

            string profileName = dProfileSelection.Text;

            bool isNewProfile = SaveProfile(profileName, tagValuePairs);
            string actionResult = isNewProfile ? "created" : "updated";

            MessageBox.Show($"Profile '{profileName}' {actionResult}!", "Process Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

            UpdateProfileDropdown();
        }

        private void btnDeleteProfile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dProfileSelection.Text))
            {
                return;
            }

            string profileName = dProfileSelection.Text;

            DialogResult result = MessageBox.Show($"Are you sure you want to permanently delete the profile '{profileName}'? This action cannot be undone.",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) { return; }

            bool success = DeleteProfile(profileName);

            if (success)
            {
                MessageBox.Show($"Profile '{profileName}' deleted.",
                    "Process Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                dProfileSelection.Text = "";
                UpdateProfileDropdown();
            }
            else
            {
                MessageBox.Show($"Profile '{profileName}' not found.",
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                UpdateProfileDropdown();
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            SettingsMenu settingsMenu = new SettingsMenu();
            settingsMenu.ShowDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdateProfileDropdown();
        }
    }
}