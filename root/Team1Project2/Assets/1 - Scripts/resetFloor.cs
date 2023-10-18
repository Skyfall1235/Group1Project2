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
        FellOffMap.Invoke();
    }
}
