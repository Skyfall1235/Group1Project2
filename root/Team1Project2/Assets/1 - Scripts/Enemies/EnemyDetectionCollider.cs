using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionCollider : MonoBehaviour
{
    [SerializeField] private EnemyAttackStatic attackStatic = null;
    [SerializeField] private EnemyRangedAttack rangedAttack = null;
    [SerializeField] string opponentTag;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        Debug.Log(opponentTag);
        Debug.Log(!other.gameObject.CompareTag(opponentTag));
        //this is just to find the enemy of tag X
        if (!other.gameObject.CompareTag(opponentTag)) 
        {
            Debug.Log($"returning item with tag {other.gameObject.tag}");
            return; 
        }

        Debug.Log(attackStatic);
        Debug.Log(rangedAttack);
        if (attackStatic != null)
        {
            Debug.Log($"setting static");
            attackStatic.Active = true;
            attackStatic.target = other.gameObject;
        }
        //ranged if its hooked up
        if(rangedAttack != null)
        {
            Debug.Log("setting ranged");
            rangedAttack.Active = true;
            rangedAttack.target = other.gameObject;
        }
        
    }
}
