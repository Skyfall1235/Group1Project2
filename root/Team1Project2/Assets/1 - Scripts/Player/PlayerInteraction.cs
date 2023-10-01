using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public List<KeyItem> collectedKeyItems = new List<KeyItem>();


    private void FindKey(Collider other)
    {
        // If the player collides with a key, add the key to the player's inventory and turn it off.
        if (other.tag == "Key")
        {
            KeyItem key = other.GetComponent<KeyItem>();
            collectedKeyItems.Add(key);

            // Turn off the key.
            other.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        FindKey(other);
    }
}

[System.Serializable]
public struct KeyItem
{
    public string Name;
    public int ID;
}

[System.Serializable]
public struct LockItem
{
    public int ID;
    public bool unlocked;
    public GameObject invisWall;
}
