using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movmentControl : MonoBehaviour
{
    [SerializeField] private PlayerController controller;

    [SerializeField] private float runSpeed = 40f;
    float horizontalMove = 0f;
    [SerializeField] private bool jump = false;
    [SerializeField] private bool isFacingRight;
    [SerializeField] private Collider collider;


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
            collider.transform.localPosition = new Vector3(-collider.transform.localPosition.x, collider.transform.localPosition.y, collider.transform.localPosition.z);
        }
        else isFacingRight = false;
        collider.transform.localPosition = new Vector3(collider.transform.localPosition.x, collider.transform.localPosition.y, collider.transform.localPosition.z);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

        

}
