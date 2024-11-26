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
    [SerializeField] private float moveForce = 5f;

    //MoveInput Vector2
    private Vector2 moveInput;
    #endregion
    #region Unity methods
    private void Awake()
    {
        rb.maxLinearVelocity = moveForce/4;
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
    }
    #endregion
}
