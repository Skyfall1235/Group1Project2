using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatIncreasePack : MonoBehaviour
{
    public enum Stat
    { 
        Health,
        Armor
    }
    public Stat stat;
    [Range(0, 100)] public int increaseAmount;
    [SerializeField] private PlayerHealthManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) 
        {
            return;
        }
        Transform parent = other.transform.parent;
        manager = parent.GetComponent<PlayerHealthManager>();
        bool p1 = (manager.m_currentHealth == manager.m_maxHealth);
        bool p2 = (manager.m_armor == manager.m_maxArmor);
        if (p1 && p2)
        {
            return;
        }
        if(stat == Stat.Health)
        {
            manager.Heal(increaseAmount);
            gameObject.SetActive(false);
        }
        else
        {
            manager.AddArmor(increaseAmount);
            gameObject.SetActive(false);
        }
    }

}
