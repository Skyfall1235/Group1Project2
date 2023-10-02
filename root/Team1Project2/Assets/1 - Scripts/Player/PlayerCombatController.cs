using System.Collections;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    //needs a collider for the jab, and the tip, array should do fine
    //prefab colliuders?

    //lerp from the start to the front
    //pl;ayer controller will handle the call and waits, this script only needs to run them
    [SerializeField] private float stabSpeed = 5f;
    [SerializeField] GameObject m_attackPrefab;
    public bool m_canAttack = false;
    [SerializeField] private Vector3 initialLocalScale;
    private bool e_isFacingRight
    {
        get { return gameObject.GetComponent<movmentControl>().isFacingRight; }
    }

    private void Start()
    {
        // Set the prefab's initial local scale.
        initialLocalScale = m_attackPrefab.transform.localScale;
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            WeaponAttack();
        }
    }

    

    
    public void WeaponAttack()
    {
        Debug.Log("Attacking");
        if (e_isFacingRight)
        {
            m_attackPrefab.transform.localScale = initialLocalScale;
        }
        // Otherwise, flip the prefab's local scale on the x-axis.
        else
        {
            m_attackPrefab.transform.localScale = new Vector3(-initialLocalScale.x, initialLocalScale.y, initialLocalScale.z);
        }

        //should only be 3 objects
        if (m_canAttack)
        {
            Debug.Log("coroutine called");
            StartCoroutine(WeaponAttackCoroutine());
        }

    }

    public void Block()
    {

    }

    public void ProjectileAttack()
    {

    }

    private IEnumerator WeaponAttackCoroutine()
    {
        //pla the stab animation
        Debug.Log("resetting");
        return null;
    }

    private IEnumerator BlockCoroutine()
    {
        return null;
    }

    private IEnumerator projectileCoroutine()
    {
        return null;
    }

}
