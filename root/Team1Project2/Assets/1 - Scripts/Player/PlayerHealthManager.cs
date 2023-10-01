using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : HealthManager, IHealthManager
{
    public override int TakeDamage(int damageAmount) 
    { 
        base.TakeDamage(damageAmount);
        //im fairly certain this is unreachable
        return 0;
    }
    public override void Heal(int healAmount) 
    { 
        base.Heal(healAmount);
    }
    protected override void Die() 
    { 
        base.Die();
    }
}
