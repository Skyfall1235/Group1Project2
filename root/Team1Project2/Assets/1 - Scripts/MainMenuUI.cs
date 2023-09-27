using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private AsyncLoader loader;
    //[SerializeField] private GameObject loaderGO;
    [SerializeField] private DefinedSceneData[] scenesForFirstLevel;

    private void Awake()
    {
        // Load the `loader` scene.
        SceneManager.LoadScene("Persistant", LoadSceneMode.Additive);

        // Find the object in the `loader` scene.
        GameObject loaderObject = GameObject.Find("Async Loader");

        // If the object is found, do something with it.
        if (loaderObject != null)
        {
            loader = loaderObject.GetComponent<AsyncLoader>();
        }
        else
        {
            //loader = loaderGO.GetComponent<AsyncLoader>();
        }
    }
    public void StartGame()
    {
        string[] sceneListToLoad = new string[scenesForFirstLevel.Length];
        for (int i = 0; i < scenesForFirstLevel.Length; i++)
        {
            sceneListToLoad[i] = scenesForFirstLevel[i].m_sceneName;
        }
        loader.LoadSceneListWithFade(sceneListToLoad, "Player");
    }
    public void QuitGame()
    {
        Application.Quit();
    }


}
