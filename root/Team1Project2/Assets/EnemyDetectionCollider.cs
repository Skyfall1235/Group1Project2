using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionCollider : MonoBehaviour
{
    [SerializeField] private EnemyAttackStatic attackStatic;
    [SerializeField] private EnemyRangedAttack rangedAttack;
    [SerializeField] string opponentTag;

    private void OnTriggerEnter(Collider other)
    {
        //this is just to find the enemy of tag X
        if (!other.gameObject.CompareTag(opponentTag)) { return; }
        if (attackStatic != null)
        {
            attackStatic.Active = true;
            attackStatic.target = other.gameObject;
        }
        //ranged if its hooked up
        if(rangedAttack != null)
        {
            rangedAttack.Active = true;
            rangedAttack.target = other.gameObject;
        }
        
    }
}
