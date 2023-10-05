using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSetter : MonoBehaviour
{
    [SerializeField] private GameObject m_playerPrefab;

    private void Start()
    {
        

        Debug.Log(m_playerPrefab);
        StartCoroutine(PlacePlayer());
    }

    private IEnumerator PlacePlayer()
    {
        yield return new WaitForSeconds(0.3f);
        // Find the first GameObject with a PlayerController component in all loaded scenes.
        PlayerController playerController = FindAnyObjectByType<PlayerController>();

        // If a GameObject with a PlayerController component was found, set the m_playerPrefab variable.
        if (playerController != null)
        {
            m_playerPrefab = playerController.gameObject;
        }
        
        m_playerPrefab.transform.position = transform.position;
    }
}
