using System.Collections;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    //needs a collider for the jab, and the tip, array should do fine
    //prefab colliuders?

    //lerp from the start to the front
    //pl;ayer controller will handle the call and waits, this script only needs to run them

    [SerializeField] GameObject m_attackPrefab;
    [SerializeField] Transform[] m_attackColliders = new Transform[2];
    [SerializeField] GameObject m_blockCollider;


    public void WeaponAttack()
    {
        GameObject prefab = Instantiate(m_attackPrefab);
        m_attackColliders = prefab.GetComponentsInChildren<Transform>();
        //should only be 3 objects
        if (m_attackColliders.Length <= 0) { Debug.LogWarning("attack colliders do not exist");  }
        if (m_attackColliders.Length > 0 )
        {
            //1 should be the animated
            GameObject animatedAttack = m_attackColliders[0].gameObject;
            //2 should be the tip
            GameObject tipAttack = m_attackColliders[1].gameObject;
            //3 is the endpoint
            GameObject attackEndAttack = m_attackColliders[2].gameObject;
        }    
    }

    public void Block()
    {

    }

    public void ProjectileAttack()
    {

    }

    private IEnumerator WeaponAttackCoRoutine(GameObject animated, GameObject tip, GameObject endPoint)
    {

    }

    private IEnumerator BlockCoRoutine()
    {

    }

    private IEnumerator projectileCoRoutine()
    {

    }

}
