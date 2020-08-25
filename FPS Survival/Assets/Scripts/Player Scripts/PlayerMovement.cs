using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveDirection;

    public float speed = 4f;
    public float jumpForce = 10f;
    private float gravity = 20f;
    private float verticalVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        moveDirection = new Vector3(Input.GetAxis(Axis.HORIZONTAL),
                                    0f, Input.GetAxis(Axis.VERTICAL));

        // Convert from Local Pos to World Pos
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed * Time.deltaTime;

        ApplyGravity();
        characterController.Move(moveDirection);
    }

    private void ApplyGravity()
    {
        if(characterController.isGrounded)
        {
            PlayerJump();
        }
        verticalVelocity -= gravity * Time.deltaTime;
        moveDirection.y = verticalVelocity * Time.deltaTime;
    }

    private void PlayerJump()
    {
        if(Input.GetKeyDown(Keycode.SPACE))
        {
            verticalVelocity = jumpForce;
        }
    } 
}
