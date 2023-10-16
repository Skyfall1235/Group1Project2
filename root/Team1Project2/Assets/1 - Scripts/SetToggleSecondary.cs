using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetToggleSecondary : MonoBehaviour
{
    private TransferInfo info;

    private void Start()
    {
        //after this object loads in, it will set the secondaries in place for the return to level 1
        StartCoroutine(SetTransferInfo());
    }

    private IEnumerator SetTransferInfo()
    {
        yield return new WaitForSeconds(0.5f);
        // Find the first GameObject with a PlayerController component in all loaded scenes.
        info = FindAnyObjectByType<TransferInfo>();

        // If a GameObject with a PlayerController component was found, set the m_playerPrefab variable.
        if (info != null)
        {
            info.ShowSecondaryItems = true;
        }


    }
}
