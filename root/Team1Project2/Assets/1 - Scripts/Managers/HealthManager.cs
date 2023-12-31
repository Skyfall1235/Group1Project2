using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour, IHealthManager
{
    public int m_currentHealth;
    public int m_armor = 0;
    public int m_maxHealth = 100;
    public int m_maxArmor = 100;
    public bool m_canTakeDamage = true;
    public float IFrameSeconds = 0f;
    public GameObject m_topParent = null;

    [SerializeField] private UnityEvent onHit = new UnityEvent();
    protected virtual void Start()
    {
        m_currentHealth = m_maxHealth;
        m_armor = m_maxArmor;
    }

    protected virtual void Update()
    {
        //TEMPHPSHOW();   
    }

    public virtual int TakeDamage(int damageAmount)
    {
        Debug.Log("reciving the call");
        if (!m_canTakeDamage)
        {
            Debug.Log("not taking damage but reciving the call");
            return 0;
        }
        onHit.Invoke();
        m_armor -= damageAmount;
        //if the armor is maller than 0 its a negative, right?
        //so if i add that to a positive, it becomes the extram damage onto the HP
        if (m_armor < 0)
        {
            //once ive used that tho, we want to make sure the amor does become a damage stack, so lets set it to 0
            //not thats its zero, the next time the player takes damage, the armor becomes negative to the damage type, and the addition works
            m_currentHealth += m_armor;
            m_armor = 0;
            if (m_currentHealth <= 0)
            {
                Die();
            }
        }
        StartCoroutine(Invincible());
        return damageAmount;
    }

    public virtual void ResetHP()
    {
        m_currentHealth = m_maxHealth;
        m_armor = m_maxArmor;
    }

    protected virtual IEnumerator Invincible()
    {
        m_canTakeDamage = false;
        yield return new WaitForSeconds(IFrameSeconds);
        m_canTakeDamage = true;
    }

    public virtual void Heal(int healAmount)
    {
        m_currentHealth += healAmount;
        if (m_currentHealth > m_maxHealth)
        {
            m_currentHealth = m_maxHealth;
        }
    }
    public virtual void AddArmor(int armorAmount)
    {
        m_armor += armorAmount;
        if (m_armor > m_maxArmor)
        {
            m_armor = m_maxArmor;
        }
    }

    protected virtual void Die()
    {
        Destroy(m_topParent);
    }
}



