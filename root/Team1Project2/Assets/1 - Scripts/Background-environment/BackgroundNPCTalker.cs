using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BackgroundNPCTalker : MonoBehaviour
{
    public GameObject canvasToDisplay;
    public TextMeshProUGUI NPC_TextDialog;
    public string NPC_Text;

    private bool canvasDisplayed = false;

    public float m_timeToDisplay = 5f;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered npc");
        if (!canvasDisplayed && other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DisplayCanvasCoroutine());
        }
    }

    private IEnumerator DisplayCanvasCoroutine()
    {
        Debug.Log("coroutine is running");
        canvasDisplayed = true;
        NPC_TextDialog.text = NPC_Text;
        canvasToDisplay.SetActive(true);
        yield return new WaitForSeconds(m_timeToDisplay);
        canvasToDisplay.SetActive(false);
    }
}
