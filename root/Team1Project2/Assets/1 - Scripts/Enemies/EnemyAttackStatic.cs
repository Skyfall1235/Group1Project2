using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class EnemyAttackStatic : MonoBehaviour
{
    public string opponentTag;
    public GameObject weapon;

    public float attackRange;
    public float attackSpeed;
    public float cooldown = 0.5f;
    public GameObject target;
    private EnemyState state;
    public float distanceFromTarget;
    public Vector3 targetDirectionNormalized;
    public bool Active = false;
    public Coroutine attack = null;
    public bool isFacingRight = true;

    public UnityEvent onAttack = new UnityEvent();


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
        targetDirectionNormalized = (target.transform.position - gameObject.transform.position).normalized;
        if (targetDirectionNormalized.x < 0)
        {
            isFacingRight = false;
        }
        else
        {
            isFacingRight = true;
        }
        //Debug.Log("determining which direction");

        switch (state)
        {
            case EnemyState.waiting:
                //calculate the distance from the player
                if (distanceFromTarget < attackRange)
                {
                    //if the player is within range, begin attack
                    state = EnemyState.Attacking;
                    //Debug.Log("waiting to attacking");
                }
                break;

            case EnemyState.Attacking:
                //Debug.Log("attacking");
                // Code to handle the attacking state.
                //find direction to player, and save it, then start the attack coroutine for that direction
                targetDirectionNormalized = (target.transform.position - gameObject.transform.position).normalized;

                if(attack == null)
                {
                    attack = StartCoroutine(WeaponAttackCoroutine(targetDirectionNormalized));
                }
                //after the oroutine is done with attack and cooldown, set to evaluation.
                
                //Debug.Log("attacking to eval");
                break;

            case EnemyState.evaluating:
                //Debug.Log("eval");
                // Code to handle the evaluating state.
                //if the player is still in the attack range, change the state to attack, else go back to waiting
                while (attack != null) { return; }
                //cooldown is built into coroutine
                state = EnemyState.waiting;
                //Debug.Log("eval to waiting");
                break;

            default://default is the inactive
                state = EnemyState.waiting;
                break;
        }
    }



    private IEnumerator WeaponAttackCoroutine(Vector3 normalizedDirection)
    {
        //Debug.Log("turning on weapon");
        weapon.SetActive(true);
        Vector2 colliderStart = weapon.transform.localPosition;

        // Calculate the new position of the image and icon.
        //Debug.Log("determining collider position");
        Vector3 newColliderPosition = normalizedDirection * attackRange;
        onAttack.Invoke();
        //Debug.Log($"IMG {newColliderPosition} ");

        // Move the image and icon to their new positions over time.
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * attackSpeed;
            Vector3 colliderPosition = Vector3.Lerp(colliderStart, newColliderPosition, t);
            weapon.transform.localPosition = colliderPosition;
            weapon.transform.LookAt(target.transform);

            yield return null;
        }
        //Debug.Log("finished attack");
        yield return new WaitForSeconds(0.1f);
        weapon.SetActive(false);
        weapon.transform.localPosition = colliderStart;

        //playing cooldown
        //Debug.Log("turning off object and applying cooldown");
        yield return new WaitForSeconds(cooldown);
        state = EnemyState.evaluating; //gets ran on the next frame, which then just returns til its done
        attack = null;
    }


}
public enum EnemyState
{
    inactive,
    waiting,
    Attacking,
    evaluating,
}
