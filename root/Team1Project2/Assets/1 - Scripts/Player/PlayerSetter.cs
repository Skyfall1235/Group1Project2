using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetter : MonoBehaviour
{
    [SerializeField] private GameObject m_playerPrefab;

    private void Start()
    {
        //Debug.Log(m_playerPrefab);
        StartCoroutine(PlacePlayer());
    }

    private IEnumerator PlacePlayer()
    {
        yield return new WaitForSeconds(0.5f);
        // Find the first GameObject with a PlayerController component in all loaded scenes.
        PlayerController playerController = FindAnyObjectByType<PlayerController>();
        

        // If a GameObject with a PlayerController component was found, set the m_playerPrefab variable.
        if (playerController != null)
        {
            m_playerPrefab = playerController.gameObject;
            m_playerPrefab.transform.position = transform.position;
        }
        
        
        
    }

    public void SetPlayerPersistant()
    {
        AsyncLoader loader = FindAnyObjectByType<AsyncLoader>();
        if (loader != null)
        {
            //we save the struct we want to modify, then modify that copy, then assign it back?

            int indexLocationOfPlayer = findPlayer(loader.SceneNames);
            DefinedSceneData player = loader.SceneNames[indexLocationOfPlayer];
            player.m_isPersistant = true;
            loader.SceneNames[indexLocationOfPlayer] = player;
        }
    }

    private int findPlayer(List<DefinedSceneData> sceneData)
    {
        int playerSceneIndex = -1;
        foreach (DefinedSceneData data in sceneData)
        {
            if (data.m_sceneName == "Player")
            {
                playerSceneIndex = sceneData.IndexOf(data);
                break;
            }
        }
        return playerSceneIndex;
    }
}
