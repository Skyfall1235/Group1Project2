using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveWings : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }
        movmentControl control = other.gameObject.GetComponent<movmentControl>();
        control.GiveWings();
        gameObject.SetActive(false);
    }
}
