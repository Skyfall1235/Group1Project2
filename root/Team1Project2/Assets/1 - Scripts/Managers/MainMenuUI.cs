using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private AsyncLoader loader;
    //[SerializeField] private GameObject loaderGO;
    [SerializeField] private DefinedSceneData[] scenesForFirstLevel;


    private void Awake()
    {
        // Start a coroutine to wait until the scene has been loaded before finding the loader.
        StartCoroutine(FindLoader());
    }

    private IEnumerator FindLoader()
    {
        // Wait until the scene has been loaded.
        yield return SceneManager.LoadSceneAsync("Persistant", LoadSceneMode.Additive);

        // Find the loader object in the `loader` scene.
        GameObject loaderObject = GameObject.Find("Async Loader");

        // If the object is found, do something with it.
        if (loaderObject != null)
        {
            loader = loaderObject.GetComponent<AsyncLoader>();
        }
        else
        {
            Debug.LogWarning("Loader was not located, please retry");
        }
    }
    public void StartGame()
    {
        string[] sceneListToLoad = new string[scenesForFirstLevel.Length];
        for (int i = 0; i < scenesForFirstLevel.Length; i++)
        {
            sceneListToLoad[i] = scenesForFirstLevel[i].m_sceneName;
        }
        Debug.Log("list of sceneListToLoad is of length " + sceneListToLoad.Length);
        loader.LoadSceneListWithFade(sceneListToLoad, "Player", true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }


}
