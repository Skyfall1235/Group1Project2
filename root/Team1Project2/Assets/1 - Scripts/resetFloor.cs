using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class resetFloor : MonoBehaviour
{
    public Transform ResetPos;
    [SerializeField] private UnityEvent FellOffMap = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Player"))
        {
            return;
        }
        Debug.Log(gameObject.name);
        Debug.Log("player fell off map from resetfloor");
        FellOffMap.Invoke();
    }
}
