
using UnityEngine;

public class End : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            movmentControl manager = other.gameObject.GetComponent<movmentControl>();
            //get parent of that object

            Transform foundmanager = manager.gameObject.transform.parent;
            foundmanager.GetComponent<GameUIManager>().DisplayWinPanel();
        }
    }
}
