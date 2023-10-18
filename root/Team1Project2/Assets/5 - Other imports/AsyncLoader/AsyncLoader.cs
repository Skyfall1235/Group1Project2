/*
 * Author: Wyatt Murray
 * Version: 2.1
 * Date: 9/28/23
 * 
 * Description:
 * Manages asynchronous loading and unloading of scenes with fade transitions.
 * 
 * Setup:
 * To set up in your project, create a scene with this script on an empty GameObject. 
 * Create a canvas under it and add a panel that covers the full screen and is colored black.
 * Remove all other scenes' EventSystems except this one.
 * Load the necessary data into the Inspector as needed and link the fade image and canvas.
 * Provide a fade duration of at least 0.25 seconds.
 * 
 * Additional Info:
 * - Ensure that referenced objects are available to call the two public methods.
 * - Remember that data must be preloaded unless you want to use GameObject.Find.
 * - This script is designed for loading single scenes with a player scene attached.
 * - There may be future improvements to support loading multiple scenes simultaneously.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoader : MonoBehaviour
{
    #region Member Variables
    /// <summary>
    /// A list of scene names to be used during loading and unloading operations.
    /// </summary>
    [SerializeField] private List<DefinedSceneData> m_sceneNames = new List<DefinedSceneData>();
    /// <summary>
    /// Public read access to the list of all scenes and their persistant tag.
    /// </summary>
    public List<DefinedSceneData> SceneNames
    {
        get { return m_sceneNames; }
        set { m_sceneNames = value; }
    }

    /// <summary>
    /// The image used for fading transitions.
    /// </summary>
    [SerializeField] private Image m_fadeImage;

    /// <summary>
    /// The Canvas GameObject associated with this loader.
    /// </summary>
    [SerializeField] private GameObject m_canvas;

    /// <summary>
    /// The duration of the fade-in and fade-out transitions in seconds.
    /// </summary>
    [SerializeField] private float m_fadeDuration = 1.0f;

    /// <summary>
    /// Current state of the fade transition between scenes.
    /// </summary>
    private bool m_isFading = false;

    /// <summary>
    /// Float value of the current scene load operation.
    /// </summary>
    private float m_percentLoaded;

    /// <summary>
    /// Public reference to the current scene load operation for the automatic fade system
    /// </summary>
    public float PercentLoaded
    {
        get { return m_percentLoaded; }
        set { m_percentLoaded = value; }
    }

    #endregion

    private void Awake()
    {
        // Make sure the fadeImage is clear upon first load
        SetFadeAlpha(0.0f);
    }

    #region Overridabble Methods

    /// <summary>
    /// Starts the coroutine to load a list of scenes together with a fade transition and confirmation that the desired active scene is loaded and set.
    /// </summary>
    /// <param name="sceneList">Array of all scene names that wish to be loaded during the transition</param>
    /// <param name="activeScene">The scene that we wish to confirm to be active, and if it is not, to set it so</param>
    public virtual void LoadSceneListWithFade(string[] sceneList, string activeScene, bool isCursorVisible)
    {
        if (!m_isFading)
        {
            StartCoroutine(FadeOutAndLoad(sceneList, activeScene));
            Cursor.visible = isCursorVisible;
        }
    }

    public virtual void LoadSceneList(string[] sceneList, string activeScene, bool reloadFlag)
    {
        StartCoroutine(LoadScenes(sceneList, activeScene, reloadFlag));
    }

    #endregion

    #region Coroutines
    /// <summary>
    /// the coroutine to load a list of scenes together and determine if a reload of the current scene list is needed.
    /// </summary>
    /// <param name="sceneList">Array of all scene names that wish to be loaded during the transition</param>
    /// <param name="activeScene">The scene that we wish to confirm to be active, and if it is not, to set it so</param>
    private IEnumerator LoadScenes(string[] sceneList, string activeScene, bool Reload)
    {
        // Create a new list to store the scenes that need to be loaded.
        List<string> scenesToLoad = new List<string>();

        //if the user wishes to reload all scenes, they can do so, else, it will only add the scenes to the
        if (Reload)
        {
            // Unload all scenes except for the Persistent Scene (if needed)
            UnloadAllScenesExceptPersistent();
        }
        else
        {
            // Iterate over the sceneList and check if each scene is already loaded.
            foreach (string scene in sceneList)
            {
                Scene selectedScene = SceneManager.GetSceneByName(scene);
                if (!selectedScene.isLoaded)
                {
                    // Add the scene to the scenesToLoad list.
                    scenesToLoad.Add(scene);
                }
            }
        }

        // Load the new scene asynchronously
        foreach (string scene in sceneList)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }

        // Check if the scene that is wished to be active is loaded
        Scene currentScene = SceneManager.GetSceneByName(activeScene);
        if (!currentScene.isLoaded)
        {
            // Load the active scene asynchronously
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(activeScene, LoadSceneMode.Additive);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            SceneManager.SetActiveScene(currentScene);
        }
    }

    /// <summary>
    /// Fades out and loads a new scene asynchronously.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    /// <param name="includePlayer">Whether to include the player scene.</param>
    private IEnumerator FadeOutAndLoad(string[] sceneList, string activeScene)
    {
        m_canvas.SetActive(true);
        m_isFading = true;
        // Fade out to black
        yield return StartCoroutine(Fade(0.0f, 1.0f));

        // Unload all scenes except for the Persistent Scene (if needed)
        UnloadAllScenesExceptPersistent();

        // Load the new scene asynchronously
        foreach (string scene in sceneList)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }

        // Check if the scene that is wished to be active is loaded
        Scene currentScene = SceneManager.GetSceneByName(activeScene);
        if (!currentScene.isLoaded)//is not loaded
        {
            // Load the active scene asynchronously
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(activeScene, LoadSceneMode.Additive);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            Debug.Log(currentScene.name);
            Debug.Log(activeScene);
            SceneManager.SetActiveScene(currentScene);
        }

        // Fade back in
        yield return StartCoroutine(Fade(1.0f, 0.0f));
        m_isFading = false;
        m_canvas.SetActive(false);
    }

    /// <summary>
    /// Fades the screen from one alpha value to another over a specified duration.
    /// </summary>
    /// <param name="startAlpha">The starting alpha value.</param>
    /// <param name="endAlpha">The ending alpha value.</param>
    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < m_fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / m_fadeDuration);
            SetFadeAlpha(alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final alpha value is set
        SetFadeAlpha(endAlpha);
    }

    /// <summary>
    /// Sets the alpha value of the fade image.
    /// </summary>
    /// <param name="alpha">The alpha value to set.</param>
    private void SetFadeAlpha(float alpha)
    {
        if (m_fadeImage != null)
        {
            Color color = m_fadeImage.color;
            color.a = alpha;
            m_fadeImage.color = color;
        }
    }

    #endregion

    #region miscelannious for code execution

    /// <summary>
    /// Unloads all scenes that are not explicitly stated as persistant.
    /// </summary>
    private void UnloadAllScenesExceptPersistent()
    {
#if UNITY_EDITOR
        Debug.Log("Unloading all scenes except those in sceneNames list.");
#endif

        int sceneCount = SceneManager.sceneCount;

        for (int i = 0; i < sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);

            // Check if the scene is marked as persistent in the sceneNames list
            bool isPersistent = false;
            foreach (DefinedSceneData definedScene in m_sceneNames)
            {
                if (definedScene.m_sceneName == scene.name && definedScene.m_isPersistant)
                {
                    isPersistent = true;
                    break;
                }
            }

            if (!isPersistent)
            {
                AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(scene);
#if UNITY_EDITOR
                unloadOperation.completed += (operation) =>
                {
                    if (unloadOperation.isDone)
                    {
                        Debug.Log("Unloaded scene: " + scene.name);
                    }
                    else
                    {
                        Debug.LogError("Failed to unload scene: " + scene.name);
                    }
                };
#endif
            }
        }
    }

    //private void UnloadSelectedScenes()

    #endregion
}

/// <summary>
/// Struct for containing the data about all useable scenes in the game.
/// </summary>
[System.Serializable]
public struct DefinedSceneData
{
    /// <summary>
    /// The name of the Scene as a string.
    /// </summary>
    public string m_sceneName;
    /// <summary>
    /// A toggle for labeling which scenes are persistant.
    /// </summary>
    public bool m_isPersistant;
}


