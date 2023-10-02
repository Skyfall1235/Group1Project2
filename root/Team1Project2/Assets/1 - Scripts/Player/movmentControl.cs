
using UnityEngine;

public class movmentControl : MonoBehaviour
{
    [SerializeField] private PlayerController controller;

    [SerializeField] private float runSpeed = 40f;
    float horizontalMove = 0f; //the value that the script uses to determine movement direction
    [SerializeField] private bool jump = false; //bool to send to the movement script when jumping
    public bool isFacingRight; // is used for the animation of the player
    [SerializeField] private Collider m_interactionCollider;  //is the sepetate collider that triggers interactions from father away so the player doesnt have to ram into NPCs


    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetKeyDown(KeyCode.W))
        {
            jump = true;
            Debug.Log(jump);
        }
        if (horizontalMove >= 0f)
        {
            isFacingRight = true;
            m_interactionCollider.transform.localPosition = new Vector3(-m_interactionCollider.transform.localPosition.x, m_interactionCollider.transform.localPosition.y, m_interactionCollider.transform.localPosition.z);
        }
        else isFacingRight = false;
        m_interactionCollider.transform.localPosition = new Vector3(m_interactionCollider.transform.localPosition.x, m_interactionCollider.transform.localPosition.y, m_interactionCollider.transform.localPosition.z);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

        

}
