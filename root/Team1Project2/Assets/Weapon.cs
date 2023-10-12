using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //get the object it hits
    //have a damage value
    //what targets can it hit
    public string targetTag;
    public int damage;

    //if the enemy is not static, knock it back
    public bool knockback;
    public float knockbackForce;
    public movmentControl moveControl;
    public EnemyAttackStatic attackStatic;


    private void OnTriggerEnter(Collider other)
    {
        HealthManager targetManager;
        if (!other.gameObject.CompareTag(targetTag)) { return; }
        Debug.Log("hitting the target tag");
        Debug.Log((other.gameObject.GetComponent<HealthManager>() != null));

        //check and see if the parent has the Hp manager
        if (other.gameObject.transform.parent.GetComponent<HealthManager>() != null)
        {
            Debug.Log("getting the health manager");
            targetManager = other.gameObject.transform.parent.GetComponent<HealthManager>();
            targetManager.TakeDamage(damage);
            Debug.Log("giving damage");
            if (knockback)
            {
                if (other.GetComponent<Rigidbody>() != null)
                {
                    bool isFacingRight = true;
                    Rigidbody rb = other.GetComponent<Rigidbody>();
                    if (moveControl != null)
                    {
                        isFacingRight = moveControl.isFacingRight;
                    }
                    if (attackStatic != null)
                    {
                        isFacingRight = attackStatic.isFacingRight;

                    }
                    Vector3 knockBackDirection = (isFacingRight ? Vector3.right : Vector3.left);
                    rb.AddForce(knockBackDirection * knockbackForce, ForceMode.Impulse);
                }
            }
        }
        //if the parent doesnt, check the object itself
        else if (other.gameObject.GetComponent<HealthManager>() != null)
        {
            Debug.Log("getting the health manager");
            targetManager = other.gameObject.GetComponent<HealthManager>();
            targetManager.TakeDamage(damage);
            Debug.Log("giving damage");
            if (knockback)
            {
                if (other.GetComponent<Rigidbody>() != null)
                {
                    bool isFacingRight = true;
                    Rigidbody rb = other.GetComponent<Rigidbody>();
                    if (moveControl != null)
                    {
                        isFacingRight = moveControl.isFacingRight;
                    }
                    if (attackStatic != null)
                    {
                        isFacingRight = attackStatic.isFacingRight;

                    }
                    Vector3 knockBackDirection = (isFacingRight ? Vector3.right : Vector3.left);
                    rb.AddForce(knockBackDirection * knockbackForce, ForceMode.Impulse);
                }
            }
        }
    }
}
