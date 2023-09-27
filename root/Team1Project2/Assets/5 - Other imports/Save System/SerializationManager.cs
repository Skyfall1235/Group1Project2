/*
 * Associated Package : Serialized Field Save System
 * Author: Wyatt Murray
 * Version: 1
 * Date: 9/21/23
 * 
 * Description:
 * The SerializationManager class provides a simple way to serialize and deserialize data in Unity, including custom types. 
 * It does this by using the BinaryFormatter class with surrogate selectors. 
 * The Save() and Load() methods can be used to save and load any type of data, including custom types.
 * 
 * Setup:
 * 1. Add and initialize Surrogates to the selector as needed
 * 2. use Save() and Load() as needed
 */
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public class SerializationManager
{
    /// <summary>
    /// Saves the specified save data to the specified save name.
    /// </summary>
    /// <param name="saveName">The name of the save file.</param>
    /// <param name="saveData">The save data to be saved.</param>
    /// <returns>True if the save was successful, false otherwise.</returns>
    public static bool Save(string saveName, object saveData)
    {
        BinaryFormatter formatter = GetBinaryFormatter();

        if(!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        string path = Application.persistentDataPath + "/saves/" + saveName + ".save";

        FileStream file = File.Create(path);

        formatter.Serialize(file, saveData);

        file.Close();

        return true;
    }

    /// <summary>
    /// Loads the save data from the specified path.
    /// </summary>
    /// <param name="path">The path to the save file.</param>
    /// <returns>The loaded save data, or null if the save file could not be loaded.</returns>
    public static object Load(string path)
    {
        if(!File.Exists(path))
        {
            return null;
        }

        BinaryFormatter formatter = GetBinaryFormatter();

        FileStream file = File.Open(path, FileMode.Open);

        try
        {
            object save = formatter.Deserialize(file);
            file.Close();
            return save;
        }
        catch
        {
            Debug.LogErrorFormat("Failed to load file at {0}", path);
            file.Close();
            return null;
        }

    }

    /// <summary>
    /// Gets a BinaryFormatter object with the appropriate surrogate selectors attached.
    /// </summary>
    /// <returns>A BinaryFormatter object with the appropriate surrogate selectors attached.</returns>
    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        SurrogateSelector selector = new SurrogateSelector();

        //add and initialize surrogates here
        Vector3SerializationSurrogate vector3Surrogate = new Vector3SerializationSurrogate();
        QuaternionSerializationSurrogate quaternionSurrogate = new QuaternionSerializationSurrogate();


        //add the surrogates to the selector
        selector.AddSurrogate(typeof(Vector3),new StreamingContext(StreamingContextStates.All), vector3Surrogate);
        selector.AddSurrogate(typeof (Quaternion),new StreamingContext(StreamingContextStates.All), quaternionSurrogate);

        formatter.SurrogateSelector = selector;


        return formatter;
    }

}
