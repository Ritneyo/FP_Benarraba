using System.Collections;
using System.Collections.Generic;
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
    private void Update()
    {
        Move();
    }
    #endregion
    public void Move()
    {
        moveInput = playerInput.actions["Movement"].ReadValue<Vector2>();
        rb.AddForce(new Vector3(moveInput.x, 0f, moveInput.y) * moveForce);
    }
}
