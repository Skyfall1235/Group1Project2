using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDamageProvider : MonoBehaviour
{
    public int m_damageValue = 0;
    [SerializeField] private bool m_canGiveDamage = true;
    [SerializeField] private float m_timeBetweenDamageCall = 0.25f;
    private void OnCollisionEnter(Collision collision)
    {
        if (!m_canGiveDamage)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("hitting the player ENTER");
            // Check if the player can take damage.
            I_HealthManager healthManager = collision.gameObject.GetComponent<I_HealthManager>();
            if (healthManager != null)
            {
                Debug.Log("attempting to apply damage");
                // Run the damage method on the player's health manager.
                healthManager.TakeDamage(m_damageValue);
                StartCoroutine(WaitToGiveDamage(m_timeBetweenDamageCall));
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(!m_canGiveDamage)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if the player can take damage.
            I_HealthManager healthManager = collision.gameObject.GetComponent<I_HealthManager>();
            if (healthManager != null)
            {
                // Run the damage method on the player's health manager.
                healthManager.TakeDamage(m_damageValue);
            }
            Debug.Log("hitting the player STAY");
            StartCoroutine(WaitToGiveDamage(m_timeBetweenDamageCall));
        }
    }

    private IEnumerator WaitToGiveDamage(float time)
    {
        m_canGiveDamage = false;
        yield return new WaitForSeconds(time);
        m_canGiveDamage = true;
    }
}
