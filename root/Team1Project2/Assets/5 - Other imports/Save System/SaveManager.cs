/*
 * Associated Package : Serialized Field Save System
 * Author: Wyatt Murray
 * Version: 1
 * Date: 9/21/23
 * 
 * Description:
 * The SaveManager class provides a simple way to save and load game data in Unity. 
 * It has two public fields: saveName, an input field for entering the save name, and loadButtonPrefab, a prefab for the load button. 
 * It also has two public methods: OnSave(), which saves the game data, and GetLoadFiles(), which retrieves a list of save files.
 * 
 * Setup:
 * 1. Import the Save Manager Package
 * 2. Extend the save and serializable surrogates as needed
 * 3. Use SaveManager to handle saving and loading of data.
 */

using TMPro;
using UnityEngine;
using System.IO;

/// <summary>
/// Manages the saving and loading of game data.
/// </summary>
public class SaveManager : MonoBehaviour
{
    /// <summary>
    /// Input field for entering the save name.
    /// </summary>
    public TMP_InputField saveName;

    /// <summary>
    /// Prefab for the load button.
    /// </summary>
    public GameObject loadButtonPrefab;

    /// <summary>
    /// Saves the game data.
    /// </summary>
    public void OnSave()
    {
        SerializationManager.Save(saveName.text, SaveData.current);
    }

    /// <summary>
    /// Array of save file names.
    /// </summary>
    public string[] saveFiles;

    
    /// <summary>
    /// Retrieves a list of save files.
    /// </summary>
    public void GetloadFiles()
    {
        // Check if the save directory exists, and if not, create it.
        if (!Directory.Exists(Application.persistentDataPath + "/saves/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves/");
        }

        // Get the list of save files in the save directory.
        saveFiles = Directory.GetFiles(Application.persistentDataPath + "/saves/");

        // Now, saveFiles array contains the names of all saved files in the saves directory.
    }

    public virtual void ShowLoadScreen()
    {
        //extend your logic here to display it how you wish
    }
}





