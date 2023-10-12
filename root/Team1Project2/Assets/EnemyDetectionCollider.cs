using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionCollider : MonoBehaviour
{
    [SerializeField] private EnemyAttackStatic attackStatic;

    private void OnTriggerEnter(Collider other)
    {
        //this is just to find the enemy of tag X
        if (!other.gameObject.CompareTag(attackStatic.opponentTag)) { return; }
        attackStatic.Active = true;
        attackStatic.target = other.gameObject;
    }
}
