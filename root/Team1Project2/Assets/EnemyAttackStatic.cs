using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class EnemyAttackStatic : MonoBehaviour
{
    public string opponentTag;
    public GameObject weapon;

    public float attackRange;
    public float attackSpeed;
    public float cooldown = 0.5f;
    private GameObject target;
    private bool canAttack = true;
    private EnemyState state;
    public float distanceFromTarget;
    private bool Active = false;
    private Coroutine attack;


    private void Update()
    {
        if(!Active) { return; }
        //else, determine the action
        DetermineAction();
    }


    private void DetermineAction()
    {
        //wait til the player comes into its detection collider
        //grab the players health manager so we can quickly reference it
        //if the player is in the collider, track its current direction from the static
        //if within range, choose to attack, then cycle to a cooldown, then re-eval
        distanceFromTarget = Vector3.Distance(target.transform.position, gameObject.transform.position);

        switch (state)
        {
            case EnemyState.waiting:
                //calculate the distance from the player
                if(distanceFromTarget > attackRange)
                {
                    //if the player is within range, begin attack
                    state = EnemyState.Attacking;
                }
                break;

            case EnemyState.Attacking:
                // Code to handle the attacking state.
                //find direction to player, and save it, then start the attack coroutine for that direction
                Vector3 targetDrectionNormalized = (target.transform.position - gameObject.transform.position).normalized;

                if(attack != null)
                {
                    attack = StartCoroutine(WeaponAttackCoroutine(targetDrectionNormalized));
                }
                //after the oroutine is done with attack and cooldown, set to evaluation.
                state = EnemyState.evaluating; //gets ran on the next frame, which then just returns til its done
                break;

            case EnemyState.evaluating:
                // Code to handle the evaluating state.
                //if the player is still in the attack range, change the state to attack, else go back to waiting
                while (attack != null) { return; }
                //cooldown is built into coroutine
                state = EnemyState.waiting;
                break;

            default://default is the inactive
                break;
        }
    }



    private IEnumerator WeaponAttackCoroutine( Vector3 normalizedDirection)
    {
        weapon.SetActive(true);
        canAttack = false;
        Vector2 colliderStart = weapon.transform.localPosition;

        // Calculate the new position of the image and icon.
        Vector3 newColliderPosition = normalizedDirection * attackRange;

        Debug.Log($"IMG {newColliderPosition} ");

        // Move the image and icon to their new positions over time.
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * attackSpeed;
            Vector3 colliderPosition = Vector3.Lerp(colliderStart, newColliderPosition, t);
            weapon.transform.localPosition = colliderPosition;

            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        weapon.SetActive(false);
        weapon.transform.localPosition = colliderStart;

        //playing cooldown
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //this is just to find the enemy of tag X
        if(!other.gameObject.CompareTag(opponentTag)) { return; }
        Active = true;
        target = other.gameObject;
    }


}
public enum EnemyState
{
    inactive,
    waiting,
    Attacking,
    evaluating,
}
