using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using static BossHealthManager;

public class BossHealthManager : EnemyHealthmanager
{
    [System.Serializable]
    public struct CrystalInfo
    {
        public GameObject[] crystal;
        public int crystalsLeft;
        public int m_maxBreaks;
    }

    [Space(2)]
    [Header("CrystalInfo")]
    [Space(1)]

    [SerializeField] private CrystalInfo crystalInfo;
    [SerializeField] private float m_timeToAttack;



    protected override void Start()
    {
        m_canTakeDamage = false;
        crystalInfo.m_maxBreaks = 3;
    }

    protected override void Update()
    {
        base.Update();
        if(crystalInfo.crystalsLeft == 0 && crystalInfo.m_maxBreaks > 0)
        {
            
            StartCoroutine(HandleDamageBehavior());
            
        }
        if(crystalInfo.crystalsLeft == 0 && crystalInfo.m_maxBreaks == 0)
        {
            m_canTakeDamage = true;
        }
    }

    private void PlaceCrystals()
    {
        m_canTakeDamage = false;
        foreach (var crystal in crystalInfo.crystal)
        {
            crystal.SetActive(true);
        }
    }

    private IEnumerator HandleDamageBehavior()
    {
        crystalInfo.m_maxBreaks--;
        m_canTakeDamage = true;
        yield return new WaitForSeconds(m_timeToAttack);
        PlaceCrystals();
        crystalInfo.crystalsLeft = 3;
    }

    public void BreakOneCrystal()
    {
        crystalInfo.crystalsLeft--;
        //start a coroutine to show the boss took some hits
        //if the boss has crystyals, keep the allowdamage tag off, else, start a corotuine that allows the player to attack them for a given amount of time
    }


}
