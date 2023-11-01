using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace DK
{
    public class SaveFileDataWriter
    {
        public string saveDataDirectoryPath = "";
        public string saveFileName = "";

        // Before we creane a new save file, we must check to see if one of this character slot already exists (max 10 character slots)
        public bool CheckToSeeIfFileExists()
        {
            if (File.Exists(Path.Combine(saveDataDirectoryPath, saveFileName)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Used to delete character save files
        public void DeleteSaveFile()
        {
            foreach (string file in Directory.GetFiles(saveDataDirectoryPath, "*", SearchOption.AllDirectories))
            {
                FileAttributes attributes = File.GetAttributes(file);

                if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    attributes &= ~FileAttributes.ReadOnly;

                    File.SetAttributes(file, attributes);
                }
            }

            File.Delete(Path.Combine(saveDataDirectoryPath, saveFileName));
        }
        
        // Used to create a save file upon starting a new game
        public void CreateNewCharacterSaveFile(CharacterSaveData characterData)
        {
            // Make a path to save the file (a locotion on the machine)
            string savePath = Path.Combine(saveDataDirectoryPath, saveFileName);

            try
            {
                // Create the directory the file will be written to, if it does not already exists
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                Debug.Log("Creating save file, at save path: " + savePath);

                // Serialize the C# game data into json
                string dataToStore = JsonUtility.ToJson(characterData, true);

                // Write the file to our system
                using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    using (StreamWriter fileWriter = new StreamWriter(stream))
                    { 
                        fileWriter.Write(dataToStore);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error whilst trying to save character data, game not saved " + savePath + "\n" + ex);
            }
        }

        // Used to load a save file upon loading a previous game
        public CharacterSaveData LoadSaveFile()
        {
            CharacterSaveData characterData = null;

            // MAKE A PATH TO LOAD THE FILE (A LOCATION ON THE MACHINE)
            string loadPath = Path.Combine(saveDataDirectoryPath, saveFileName);

            if (File.Exists(loadPath))
            {
                try
                {
                    string dataToLoad = "";

                    using (FileStream stream = new FileStream(loadPath, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }

                    // DESERIALIZE THE DATA FROM JSON BACK TO UNITY 
                    characterData = JsonUtility.FromJson<CharacterSaveData>(dataToLoad);
                }
                catch (Exception ex)
                {
                }
            }
            return characterData;
        }
    }
}