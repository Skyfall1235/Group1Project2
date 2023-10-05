using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : HealthManager, IHealthManager
{
    [SerializeField] private GameUIManager m_gameUIManager;

    protected override void Start()
    {
        base.Start();
        if (m_gameUIManager != null )
        {
            m_gameUIManager = gameObject.GetComponent<GameUIManager>();
        }
    }
    public override int TakeDamage(int damageAmount) 
    { 
        base.TakeDamage(damageAmount);
        m_gameUIManager.UpdateStatusBars();
        //im fairly certain this is unreachable
        return 0;
    }
    public override void Heal(int healAmount) 
    { 
        base.Heal(healAmount);
        m_gameUIManager.UpdateStatusBars();
    }
    protected override void Die() 
    {
        m_gameUIManager.UpdateStatusBars();
        base.Die();
    }
    public override void AddArmor(int armorAmount)
    {
        base.AddArmor(armorAmount);
        m_gameUIManager.UpdateStatusBars();
    }
}
