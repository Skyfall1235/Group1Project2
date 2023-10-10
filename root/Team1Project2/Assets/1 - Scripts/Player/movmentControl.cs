
using UnityEngine;

public class movmentControl : MonoBehaviour
{
    [SerializeField] private PlayerController controller;

    [SerializeField] private float runSpeed = 40f;
    float horizontalMove = 0f; //the value that the script uses to determine movement direction
    [SerializeField] private bool jump = false; //bool to send to the movement script when jumping
    public bool m_hasWings = false;
    [SerializeField] private GameObject m_WingIcon;
    [SerializeField] private float m_newJumpForce;
    public bool isFacingRight; // is used for the animation of the player
    [SerializeField] private Collider m_interactionCollider;  //is the sepetate collider that triggers interactions from father away so the player doesnt have to ram into NPCs
    public bool directionalWalk = false;
    public float offAxisWalkValue = 0f;


    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetKeyDown(KeyCode.W))
        {
            jump = true;
            //Debug.Log(jump);
        }
        if (horizontalMove >= 0f)
        {
            isFacingRight = true;
            m_interactionCollider.transform.localPosition = new Vector3(-m_interactionCollider.transform.localPosition.x, m_interactionCollider.transform.localPosition.y, m_interactionCollider.transform.localPosition.z);
        }
        else isFacingRight = false;
        m_interactionCollider.transform.localPosition = new Vector3(m_interactionCollider.transform.localPosition.x, m_interactionCollider.transform.localPosition.y, m_interactionCollider.transform.localPosition.z);
        
        //if the local walk area is not 0, we modify it, except when we want to
        if (!directionalWalk)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            return;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, offAxisWalkValue);
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    public void GiveWings()
    {
        controller.m_JumpForce = m_newJumpForce;
        m_WingIcon.SetActive(true);
    }

        

}
