using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    public string opponentTag;
    public GameObject projectilePrefab;

    public float attackRange;
    public float projectileForce;
    public float cooldown = 0.5f;
    public GameObject target;
    private EnemyState state;
    public float distanceFromTarget;
    public Vector3 targetDirectionNormalized;
    public bool Active = false;
    public Coroutine attack = null;
    public bool isFacingRight = true;

    public AudioClip projectileClip;


    private void Update()
    {
        if (!Active) { return; }
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

                if (attack == null)
                {
                    attack = StartCoroutine(ShootCoroutine(targetDirectionNormalized));
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



    private IEnumerator ShootCoroutine(Vector3 normalizedDirection)
    {

        // Calculate the new position of the image and icon.
        //Debug.Log("determining collider position");
        GameObject projectile = LaunchProjectile(normalizedDirection);
        
        
        //Debug.Log("finished attack");
        //playing cooldown
        //Debug.Log("turning off object and applying cooldown");
        yield return new WaitForSeconds(cooldown);
        projectile.GetComponent<Weapon>().DestroyObject();
        state = EnemyState.evaluating; //gets ran on the next frame, which then just returns til its done
        attack = null;
    }


    public GameObject LaunchProjectile(Vector3 direction)
    {
        // Create a new projectile object.
        GameObject projectile = Instantiate(projectilePrefab);
        AudioSource source = GetComponent<AudioSource>();
        if(projectileClip != null) { source.PlayOneShot(projectileClip); }
        

        // Set the projectile's position to the launcher's position.
        projectile.transform.position = transform.position;
        projectile.GetComponent<Weapon>().isFacingRight = isFacingRight;

        // Add a force to the projectile in the specified direction.
        projectile.GetComponent<Rigidbody>().AddRelativeForce(direction * projectileForce);
        return projectile;
    }


}
