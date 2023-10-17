using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowBuildLevel : MonoBehaviour
{
    public TextMeshProUGUI worldIndicator;

    private void FixedUpdate()
    {
        // Get the current build index.
        

        // Get the scene name.
        string sceneName = FindCurrentWorldName(GetAllOpenScenes());
        int buildIndex = GetSceneIndexByName(sceneName);
        worldIndicator.text = $"Level {buildIndex} / World {sceneName}";
    }

    private List<Scene> GetAllOpenScenes()
    {
        // Get the number of scenes loaded.
        int sceneCount = SceneManager.sceneCount;

        // Create a list to store the open scenes.
        List<Scene> openScenes = new List<Scene>();

        // Iterate over the scenes and add them to the list if they are open.
        for (int i = 0; i < sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.isLoaded)
            {
                openScenes.Add(scene);
            }
        }

        // Return the list of open scenes.
        return openScenes;
    }
    private string FindCurrentWorldName(List<Scene> openScenes)
    {
        List<string> worldNames = new List<string>();

        foreach (Scene scene in openScenes)
        {
            if (scene.name != "Persistant" && scene.name != "Player")
            {
                worldNames.Add(scene.name);
            }
        }

        if (worldNames.Count > 0)
        {
            return worldNames[0];
        }
        else
        {
            return null;
        }
    }
    private int GetSceneIndexByName(string sceneName)
    {
        // Get the number of scenes loaded.
        int sceneCount = SceneManager.sceneCount;

        // Iterate over the scenes and return the index of the scene with the specified name.
        for (int i = 0; i < sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name == sceneName)
            {
                return i;
            }
        }

        // Return -1 if the scene was not found.
        return -1;
    }

}
