using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interfaces
{ }
public interface IHealthManager
{
    public virtual int TakeDamage(int damageAmount) { return 0; }
    public virtual void Heal(int healAmount) { }
    virtual void Die() { }

}
