using System.Collections;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    //needs a collider for the jab, and the tip, array should do fine
    //prefab colliuders?

    //lerp from the start to the front
    //pl;ayer controller will handle the call and waits, this script only needs to run them
    
    public bool m_canAttack = false;
    public bool m_canBlock = false;
    public bool m_canAct = true;

    [System.Serializable]
    private struct AttackData
    {
        public float stabSpeed;
        public AnimationClip stabLeftClip;
        public AnimationClip stabRightClip;
    }

    [SerializeField]
    private AttackData m_AttackData;


    [System.Serializable]
    private struct BlockData
    {
        public GameObject m_blockCollider;
        public float blockTime;
    }

    [SerializeField] private BlockData m_blockData;

    [SerializeField] private Vector3 initialLocalScale;
    private bool e_isFacingRight
    {
        get { return gameObject.GetComponent<movmentControl>().isFacingRight; }
    }

    private void Start()
    {

    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space) && m_canAct)
        {
            WeaponAttack();
        }
        if(Input.GetKeyUp(KeyCode.C) && m_canAct)
        {
            Block();
        }
        

    }

    

    
    public void WeaponAttack()
    {
        //prevent block and attack stacking
        if (!m_canAttack || !m_canAct) { return; }

            Debug.Log("Attacking");
        if (e_isFacingRight)
        {
            //play the right facing animation
        }
        // Otherwise, flip the prefab's local scale on the x-axis.
        else
        {
            //play the left facing animation
        }
    }

    public void Block()
    {
        if (!m_canBlock) { return; }

        if (e_isFacingRight)
        {
            m_blockData.m_blockCollider.transform.localPosition = new Vector3(-1.4f, 0, 0);
        }
        m_blockData.m_blockCollider.transform.localPosition = new Vector3(1.4f, 0, 0);
        StartCoroutine(BlockCoroutine());

    }

    public void ProjectileAttack()
    {

    }

    private IEnumerator WeaponAttackCoroutine(bool facingRight)
    {
        m_canAct = false;
        //pla the stab animation
        Debug.Log("resetting");
        
        m_canAct = true;
        return null;
    }

    private IEnumerator BlockCoroutine()
    {
        m_canAct = false;
        m_blockData.m_blockCollider.SetActive(true);
        float blockTime = m_blockData.blockTime;
        yield return new WaitForSeconds(blockTime);
        m_blockData.m_blockCollider.SetActive(false);
        m_canAct = true;

    }

    private IEnumerator projectileCoroutine()
    {
        return null;
    }


    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int LeftStab = Animator.StringToHash("LeftStab");
    private static readonly int RightStab = Animator.StringToHash("RightStab");
    private static readonly int FireProjectile = Animator.StringToHash("FireProjectile");
}
