using Newtonsoft.Json;
using OpenARIANA.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace OpenARIANA
{
    public class ProfileManager
    {
        // Singleton instance, lazy initialization
        private static readonly Lazy<ProfileManager> instance =
            new Lazy<ProfileManager>(() => new ProfileManager());

        private static string subDirectory = "Profiles";
        private static string profileDirectory;

        private Dictionary<string, string> profileFilePaths;

        private static string customProfilDirectory = Properties.Settings.Default.CustomProfileDir;
        private static string tagPattern = Properties.Settings.Default.TagPattern;


        // Private constructor for singleton
        private ProfileManager()
        {


            if (!string.IsNullOrEmpty(customProfilDirectory))
            {
                FileSystemManager fileSystemManager = new FileSystemManager(customProfilDirectory);
                //profileDirectory = Path.Combine(customProfilDirectory, "ProfileDirectory.json");
                profileDirectory = fileSystemManager.GetAbsoluteFilePath("ProfileDirectory.json", "OpenARIANA\\Profiles");
                Logger.LogSystem($"Custom Profile Directory was set. Read from {profileDirectory}");
            }
            else
            {
                FileSystemManager fileSystemManager = new FileSystemManager();
                profileDirectory = fileSystemManager.GetAbsoluteFilePath("ProfileDirectory.json", "OpenARIANA\\Profiles");
            }

            // Check if file exists and is not empty
            if (File.Exists(profileDirectory) && new FileInfo(profileDirectory).Length == 0)
            {
                // File is empty, initialize it with empty JSON object content
                string initJsonContent = JsonConvert.SerializeObject(new Dictionary<string, string>(), Formatting.Indented);
                File.WriteAllText(profileDirectory, initJsonContent);
            }

            LoadProfileMappings();
        }

        // Public property to access the singleton instance
        public static ProfileManager Instance
        {
            get { return instance.Value; }
        }

        // Public properties (unchanged)
        public string CentralMappingFilePath
        {
            get { return profileDirectory; }
        }

        public string TagPattern
        {
            get { return tagPattern; }
        }

        private void LoadProfileMappings()
        {
            try
            {
                string jsonContent = File.ReadAllText(profileDirectory);
                profileFilePaths = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonContent);
                Logger.LogInfo($"Successfully loaded ProfileDirectory.json.");
            }
            catch (Exception ex)
            {
                Logger.LogWarning($"Couldn't read ProfileDirectory.json: \n{ex.ToString()}");
                return;
            }
        }

        private void UpdateCentralMappingFile()
        {
            // Serialize the updated profile file paths to JSON
            string updatedJson = JsonConvert.SerializeObject(profileFilePaths, Formatting.Indented);

            // Write the updated JSON back to the central mapping file
            File.WriteAllText(profileDirectory, updatedJson);

            Logger.LogInfo($"Successfully updated the Profile Directory");
        }

        public Dictionary<string, string> GetProfile(string profileName)
        {
            if (profileFilePaths.TryGetValue(profileName, out string filePath))
            {
                string jsonContent = File.ReadAllText(filePath);
                var profileData = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonContent);
                return profileData;
            }
            else
            {
                Logger.LogError($"Profile {profileName} not found.");
                throw new KeyNotFoundException($"Profile {profileName} not found.");
            }
        }

        public List<string> GetAllProfileNames()
        {
            // Return all the keys from the profileFilePaths dictionary as a list
            if (profileFilePaths == null)
            {
                return new List<string>();
            }

            return new List<string>(profileFilePaths.Keys);
        }

        public bool SaveProfile(string profileName, Dictionary<string, string> tagValuePairs,
            Dictionary<string, string> genInfo = null)
        {

            //bool isNewProfile = !profileFilePaths.ContainsKey(profileName);
            bool isNewProfile = !GetAllProfileNames().Contains(profileName);
            if (isNewProfile)
            {
                CreateProfile(profileName, tagValuePairs, genInfo);
            }
            else
            {
                UpdateProfile(profileName, tagValuePairs, genInfo);
            }
            return isNewProfile;
        }

        public void CreateProfile(string profileName, Dictionary<string, string> tagValuePairs,
            Dictionary<string, string> genInfo = null)
        {
            string newProfileFilePath;
            // Construct the path for the new profile JSON file
            if (!string.IsNullOrEmpty(customProfilDirectory))
            {
                newProfileFilePath = Path.Combine(customProfilDirectory, $"{profileName}.json");
            }
            else
            {
                FileSystemManager fileSystemManager = new FileSystemManager();

                newProfileFilePath = fileSystemManager.GetAbsoluteFilePath($"{profileName}.json", subDirectory);
            }


            // Serialize the tag-value pairs to JSON and write to the new file
            string jsonContent = JsonConvert.SerializeObject(tagValuePairs, Formatting.Indented);
            File.WriteAllText(newProfileFilePath, jsonContent);

            Logger.LogInfo($"New profile {profileName} created and stored at {newProfileFilePath}.");

            // Update the central mapping with the new profile
            profileFilePaths[profileName] = newProfileFilePath;
            UpdateCentralMappingFile();
        }

        public void UpdateProfile(string profileName, Dictionary<string, string> newTagValuePairs,
            Dictionary<string, string> genInfo = null)
        {
            // Get the file path for the existing profile
            string profileFilePath = profileFilePaths[profileName];

            // Read the existing content of the profile
            string existingJson = File.ReadAllText(profileFilePath);
            var existingTagValuePairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(existingJson);

            // Merge the existing tag-value pairs with the new ones
            foreach (var kvp in newTagValuePairs)
            {
                existingTagValuePairs[kvp.Key] = kvp.Value; // This will add or update entries
            }

            // Serialize the merged tag-value pairs to JSON
            string updatedJson = JsonConvert.SerializeObject(existingTagValuePairs, Formatting.Indented);

            // Write the updated JSON to the existing file
            File.WriteAllText(profileFilePath, updatedJson);

            Logger.LogInfo($"Updated Profile {profileName}.");
        }

        public bool DeleteProfile(string profileName)
        {
            // get filePath
            if (!profileFilePaths.TryGetValue(profileName, out string filePath))
            {
                Logger.LogInfo($"Profile '{profileName}' not listed in ProfileDirectory.json. Nothing to do.");
                throw new KeyNotFoundException($"Profile {profileName} not found.");
            }
            // delete profile file
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Logger.LogInfo($"Profile '{profileName}' deleted.");
            }
            else
            {
                profileFilePaths.Remove(profileName);
                Logger.LogWarning($"Profile file at '{filePath}' not found. Removed '{profileName}' from ProfileDirectory.");
                throw new FileNotFoundException($"File for '{profileName}' not found at {filePath}");
            }
            // delete profile entry in memory and update central file
            profileFilePaths.Remove(profileName);
            Logger.LogInfo($"Profile '{profileName}' deleted.");
            UpdateCentralMappingFile();
            return true;
        }

        public void ChangeProfileDirectory(string newDirectory)
        {
            var newProfileFilePaths = new Dictionary<string, string>();
            foreach (var pair in profileFilePaths)
            {
                string profileName = pair.Key;
                string oldFilePath = pair.Value;
                string newFilePath = Path.Combine(newDirectory, "Profiles", Path.GetFileName(oldFilePath));

                try
                {
                    // Move each file to the new directory
                    if (!Directory.Exists(newDirectory))
                        Directory.CreateDirectory(newDirectory);

                    if (File.Exists(newFilePath))
                        File.Delete(newFilePath); // Delete if already exists

                    File.Move(oldFilePath, newFilePath);
                    newProfileFilePaths.Add(profileName, newFilePath);

                    Logger.LogInfo($"Moved profile '{profileName}' to new directory.");
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Error moving file '{oldFilePath}' to '{newFilePath}': {ex.Message}");
                    throw ex;
                }
            }

            // Update the dictionary and central mapping file
            profileFilePaths = newProfileFilePaths;
            profileDirectory = Path.Combine(newDirectory, "Profiles", "ProfileDirectory.json");
            UpdateCentralMappingFile();
        }


        public string GetProfileFilePath(string profileName)
        {
            try
            {
                return profileFilePaths[profileName];
            }
            catch (KeyNotFoundException)
            {
                Logger.LogError($"Profile {profileName} not found.");
                MessageBox.Show($"Profile {profileName} not found.");
                return null;
            }
        }

        public void UpdateProfileManagerSettings()
        {
            customProfilDirectory = Properties.Settings.Default.CustomProfileDir;
            tagPattern = Properties.Settings.Default.TagPattern;
            Logger.LogSystem($"Tag pattern set to {tagPattern}.");
        }

    }
}