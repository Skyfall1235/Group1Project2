
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerCombatController : MonoBehaviour
{
    //needs a collider for the jab, and the tip, array should do fine
    //prefab colliuders?

    //lerp from the start to the front
    //pl;ayer controller will handle the call and waits, this script only needs to run them
    
    public bool m_canAttack = false;
    public bool m_canBlock = false;
    public bool m_canAct = true;

    public bool localRight;
    [System.Serializable]
    private struct AttackData
    {
        public float stabSpeed;
        public GameObject collider;
        public float IRLdistance;
        public float IGDistanceForIcon;
        public Image spearIcon;
    }

    [SerializeField]
    private AttackData m_AttackData;

    public UnityEvent attackEvent = new UnityEvent();


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

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space) && m_canAct)
        {
            WeaponAttack();
        }
        if(Input.GetKeyUp(KeyCode.LeftControl) && m_canAct)
        {
            Block();
        }
        
        localRight = e_isFacingRight;
    }

    

    
    public void WeaponAttack()
    {
        //prevent block and attack stacking
        if (!m_canAttack || !m_canAct) { return; }
            StartCoroutine(WeaponAttackCoroutine(e_isFacingRight));
            //Debug.Log("Attacking");
        
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

    private IEnumerator WeaponAttackCoroutine(bool facingRight)
    {
        m_AttackData.collider.SetActive(true);
        m_canAct = false;
        Vector2 colliderStart = m_AttackData.collider.transform.localPosition;

        // Calculate the new position of the image and icon.
        Vector3 newColliderPosition = Vector3.right * m_AttackData.IRLdistance;
        //Debug.Log(newColliderPosition);
        attackEvent.Invoke();

        //Debug.Log($"IMG {newColliderPosition} ");

        // Move the image and icon to their new positions over time.
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * m_AttackData.stabSpeed;
            Vector3 colliderPosition = Vector3.Lerp(colliderStart, newColliderPosition, t);
            m_AttackData.collider.transform.localPosition = colliderPosition;

            yield return null;
        }
        //Debug.Log("now local position");
        //Debug.Log(m_AttackData.collider.transform.localPosition);
        yield return new WaitForSeconds(0.1f);
        m_AttackData.collider.SetActive(false);
        m_AttackData.collider.transform.localPosition = colliderStart;

        // Play the stab animation.
        //Debug.Log("resetting");

        m_canAct = true;
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

}
