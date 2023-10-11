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


    private void OnTriggerEnter(Collider other)
    {
        HealthManager targetManager;
        if (!other.gameObject.CompareTag(targetTag)) { return; }

        if (other.gameObject.GetComponent<HealthManager>() != null)
        {
            targetManager = other.gameObject.GetComponent<HealthManager>();
            targetManager.TakeDamage(damage);
            if (knockback)
            {
                if (other.GetComponent<Rigidbody>() != null)
                {
                    Rigidbody rb = other.GetComponent<Rigidbody>();
                    bool isFacingRight = moveControl.isFacingRight;
                    Vector3 knockBackDirection = (isFacingRight ? Vector3.right : Vector3.left);
                    rb.AddForce(knockBackDirection * knockbackForce, ForceMode.Impulse);
                }
            }
        }


    }
}
