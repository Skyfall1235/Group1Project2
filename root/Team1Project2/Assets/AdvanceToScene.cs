using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdvanceToScene : MonoBehaviour
{
    [SerializeField] private string[] sceneList;
    [SerializeField] private string activeScene;
    [SerializeField] private bool isCursorVisible;
    public AsyncLoader loader;

    private void Start()
    {
        loader = GameObject.FindWithTag("AsyncLoader").GetComponent<AsyncLoader>();
    }

    private void OnTriggerEnter(Collider other)
    {
       loader.LoadSceneListWithFade(sceneList, activeScene, false);
    }
}
