using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetermineDisplay : MonoBehaviour
{

    //this serves to turn on things if the scene has items that require a reload. its not pretty but it works

    public GameObject objectToToggle;
    PlayerSetter playerSetter;
    public Vector3 NewPlayerSetLocation;
    private void Awake()
    {
        playerSetter = FindAnyObjectByType<PlayerSetter>();
    }
    private void Start()
    {
        DetermineToggledDisplay();
    }

    private void DetermineToggledDisplay()
    {
        // Find the first GameObject with a PlayerController component in all loaded scenes.
        TransferInfo info = FindAnyObjectByType<TransferInfo>();

        // If a GameObject with a PlayerController component was found, set the m_playerPrefab variable.
        if (info != null && info.ShowSecondaryItems)
        {
            objectToToggle.SetActive(true);
            playerSetter.gameObject.transform.position = NewPlayerSetLocation;
            playerSetter.SetPlayerPersistant();
        }
    }
}

