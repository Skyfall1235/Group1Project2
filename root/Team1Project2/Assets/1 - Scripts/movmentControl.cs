using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movmentControl : MonoBehaviour
{
    [SerializeField] private PlayerController controller;

    [SerializeField] private float runSpeed = 40f;
    float horizontalMove = 0f;
    [SerializeField] private bool jump = false;

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetKeyDown(KeyCode.W))
        {
            jump = true;
            Debug.Log(jump);
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
