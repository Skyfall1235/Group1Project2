using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BossHealthManager : EnemyHealthmanager
{
    [System.Serializable]
    public struct CrystalInfo
    {
        public GameObject[] crystal;
        public Vector3[] crystalPositions;
        public Transform crystalParent;
        public GameObject cystalPrefab;
        public int crystalsLeft;
        public int maxCrystals;
        public bool useDebugTools;
    }

    [Space(2)]
    [Header("CrystalInfo")]
    [Space(1)]

    [SerializeField] private CrystalInfo crystalInfo;


    protected override void Start()
    {


        SetCrystalPositions();
        m_canTakeDamage = false;
    }

    protected override void Update()
    {
        base.Update();
        if (crystalInfo.useDebugTools)
        {
            SetCrystalPositions();
        }
    }

    private void SetCrystalPositions()
    {
        crystalInfo.crystalPositions = new Vector3[crystalInfo.crystal.Length];
        int index = 0;
        foreach (GameObject crystalObject in crystalInfo.crystal)
        {
            Vector3 pos = crystalObject.transform.position;
            crystalInfo.crystalPositions[index] = pos;
            index++;
        }
    }

    private void PlaceCrystals()
    {
        crystalInfo.crystalsLeft = crystalInfo.maxCrystals;
        foreach (Vector3 pos in crystalInfo.crystalPositions)
        {
            Instantiate(crystalInfo.cystalPrefab, crystalInfo.crystalParent.position, Quaternion.identity);
        }
    }

    public void BreakOneCrystal()
    {
        crystalInfo.crystalsLeft--;
        //start a coroutine to show the boss took some hits
        //if the boss has crystyals
    }


}
