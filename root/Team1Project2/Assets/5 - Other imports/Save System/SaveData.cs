/*
 * Associated Package : Serialized Field Save System
 * Author: Wyatt Murray
 * Version: 1
 * Date: 9/21/23
 */
using System.Collections;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    private static SaveData _current;
    public static SaveData current
    {
        get
        {
            if (_current == null)
            {
                _current = new SaveData();
            }
            return _current;
        }
    }

    //add all your variables and data you want to save here as the data type you wish to store.
    //modify variables here using another script, do not modify them inside of this singleton.
    public string playerName;


}