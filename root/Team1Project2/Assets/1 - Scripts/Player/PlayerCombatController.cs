using Codice.CM.Common;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
        public GameObject collider;
        public float IRLdistance;
        public float IGDistanceForIcon;
        public Image spearIcon;
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
            StartCoroutine(WeaponAttackCoroutine(e_isFacingRight));
            Debug.Log("Attacking");
        
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
        RectTransform imagePosition = m_AttackData.spearIcon.GetComponent<RectTransform>();
        Collider collider = m_AttackData.collider.GetComponent<Collider>();

        // Get the current position of the image and icon.
        float iconTravel = m_AttackData.IGDistanceForIcon;
        float colliderTravel = m_AttackData.IRLdistance;

        // Calculate the new position of the image and icon.
        Vector2 newImagePosition = (e_isFacingRight ? Vector2.right * iconTravel : Vector2.left * iconTravel);
        //Vector3 newColliderPosition = (e_isFacingRight ? Vector3.right * colliderTravel : Vector3.left * colliderTravel);

        Debug.Log($"IMG {newImagePosition} ");



        // Move the image and icon to their new positions over time.
        while (imagePosition.anchoredPosition != newImagePosition)
        {
            // Calculate the distance to move the image and icon.
            Vector2 imageDistanceToMove = newImagePosition - imagePosition.anchoredPosition;
            //Vector3 colliderDistanceToMove = newColliderPosition - collider.transform.position;

            // Move the image and icon by the calculated distance.
            imagePosition.anchoredPosition += imageDistanceToMove * m_AttackData.stabSpeed * Time.deltaTime;
            //collider.transform.localPosition += colliderDistanceToMove * m_AttackData.stabSpeed * Time.deltaTime;

            yield return null;
        }

        //pla the stab animation
        Debug.Log("resetting");
        
        m_canAct = true;
        //return null;
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

}
