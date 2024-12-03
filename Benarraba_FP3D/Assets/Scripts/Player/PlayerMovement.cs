using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [Header("Components")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerInput playerInput;

    [Header("Player stats")]
    public float moveForce = 5f;

    //MoveInput Vector2
    private Vector2 moveInput;

    //Parameters
    private bool accelUp = false;
    private bool accelDown = false;
    #endregion
    #region Unity methods
    private void Awake()
    {
        rb.maxLinearVelocity = moveForce / 8;
    }

    private void Update()
    {
        Move();
    }
    #endregion
    #region My methods
    public void Move()
    {
        moveInput = playerInput.actions["Movement"].ReadValue<Vector2>();

        if (moveInput != Vector2.zero)
        {
            Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
            moveDirection = transform.TransformDirection(moveDirection);

            rb.AddForce(moveDirection * moveForce, ForceMode.Force);
        }
        else
        {
            rb.maxLinearVelocity = moveForce / 8;
            new WaitForSecondsRealtime(0.5f);
            rb.maxLinearVelocity = moveForce / 4;
        } 
    }
    #endregion
}
