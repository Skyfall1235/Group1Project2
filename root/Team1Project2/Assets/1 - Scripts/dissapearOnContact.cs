using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dissapearOnContact : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Disapear());
        }
    }

    private IEnumerator Disapear()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
