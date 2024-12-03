using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
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

    #endregion
    #region Unity methods
    private void Start()
    {
        Cursor.visible = false;
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

            rb.velocity = new Vector3(moveDirection.x * moveForce, rb.velocity.y, moveDirection.z * moveForce);
        }
        //else
        //{
        //    rb.velocity = new Vector3(rb.velocity.x * 0.9f, rb.velocity.y, rb.velocity.z * 0.9f);
        //} 
    }
    #endregion
}
