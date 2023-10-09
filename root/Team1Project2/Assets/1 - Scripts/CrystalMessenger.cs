using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalMessenger : HealthManager
{
    [SerializeField] private BossHealthManager bossHPManager;

    protected override void Die()
    {
        //start a cortounie that blinks or does some effect

        //call the boss managers crystal break
        bossHPManager.BreakOneCrystal();
        gameObject.SetActive(false);
    }
}
