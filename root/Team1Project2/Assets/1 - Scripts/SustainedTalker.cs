using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SustainedTalker : MonoBehaviour
{
    [SerializeField] private List<Dialog> m_dialog = new List<Dialog>();
    [SerializeField] private List<DisplayAndContent> m_speakers = new List<DisplayAndContent>();
    [SerializeField] private float timetoDisplayText = 5f;
    [SerializeField] private bool m_hasBeenTriggered = false;
    [SerializeField] private bool m_StopPlayer = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered  sustained Talker NPC");
        if (!m_hasBeenTriggered && other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(StartChat(other));
            m_hasBeenTriggered = true;
        }
    }

    private void Start()
    {
        //turn all talk text off
        foreach (Dialog dialog in m_dialog)
        {
            // Get the speaker for the current dialog.
            Talker speaker = dialog.speaker;
            DisplayAndContent displayAndContent;
            try
            {
                // Find the DisplayAndContent object for the speaker.
                displayAndContent = m_speakers.Find((d) => d.speaker == speaker);
            }
            catch (Exception e)
            {
                Debug.LogWarning("could not find the speaker in the list provided");
                Debug.Log(e);
                return;
            }
            // Turn off the speaker's game object.
            displayAndContent.TextDisplay.SetActive(false);
        }
    }


    //method that once is triggers, start going through the dialogs 1 by one, for each text
    private IEnumerator StartChat(Collider other)
    {
        if(m_StopPlayer)
        {
            other.gameObject.GetComponent<movmentControl>().canMove = false;
        }
        
        //for each dialog in the list, 
        //turn on the gameobject idicated by the speaker, display the new text, wait a few seconds, and then turn off the panel

        // Iterate over the dialogs in the list.
        foreach (Dialog dialog in m_dialog)
        {
            // Get the speaker for the current dialog.
            Talker speaker = dialog.speaker;
            DisplayAndContent displayAndContent;
            try
            {
                // Find the DisplayAndContent object for the speaker.
                displayAndContent = m_speakers.Find((d) => d.speaker == speaker);
            }
            catch (Exception e)
            {
                Debug.LogWarning("could not find the speaker in the list provided");
                Debug.Log(e);
                yield break;
            }

            // Set the text content of the speaker's text mesh.
            displayAndContent.TextContent.text = dialog.content;

            // Turn on the speaker's game object.
            displayAndContent.TextDisplay.SetActive(true);

            // Wait for a few seconds.
            yield return new WaitForSeconds(timetoDisplayText);

            // Turn off the speaker's game object.
            displayAndContent.TextDisplay.SetActive(false);
            //little bit of delay for chats so they dont teleport
            yield return new WaitForSeconds(0.5f);
        }
        other.gameObject.GetComponent<movmentControl>().canMove = true;
    }
}

[System.Serializable]
public struct Dialog
{
    public Talker speaker;
    public string content;
}

[System.Serializable]
public struct DisplayAndContent
{
    public Talker speaker;
    public GameObject TextDisplay;
    public TextMeshProUGUI TextContent;
}

[System.Serializable]
public enum Talker
{
    player,
    priest,
    demon,
    angel,
    god
}
