using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //get the object it hits
    //have a damage value
    //what targets can it hit
    public string targetTag;
    public bool useTarget = false;
    public int damage;

    public bool destroyAfterTime = false;
    public int TimeTilDestroy = 5;

    //if the enemy is not static, knock it back
    public bool knockback;
    public float knockbackForce;
    public movmentControl moveControl;
    public EnemyAttackStatic attackStatic;
    public bool isFacingRight = true;


    private void OnTriggerEnter(Collider other)
    {
        HealthManager targetManager;
        if (useTarget)
        {
            if (!other.gameObject.CompareTag(targetTag)) { return; }
        }
        else
        {
            if (!other.gameObject.CompareTag("Enemy")) { return; }
        }
        
        Debug.Log("hitting the target tag");
        Debug.Log((other.gameObject.GetComponent<HealthManager>() != null));

        //check and see if the parent has the Hp manager
        
        //if the parent doesnt, check the object itself
        if (other.gameObject.GetComponent<HealthManager>() != null)
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

        else if(other.gameObject.transform.parent.GetComponent<HealthManager>() != null)
        { 
            Debug.Log("getting the health manager");
            targetManager = other.gameObject.transform.parent.GetComponent<HealthManager>();
            targetManager.TakeDamage(damage);
            Debug.Log("giving damage");
            if (knockback)
            {
                if (other.GetComponent<Rigidbody>() != null)
                {

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

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
