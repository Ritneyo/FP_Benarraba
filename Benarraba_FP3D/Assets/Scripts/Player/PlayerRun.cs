using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRun : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerMovement playerMovement;
    private float oriMoveForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void Run(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            rb.maxLinearVelocity = playerMovement.moveForce / 2;
            Debug.Log("Start run");
        }
        else if (callbackContext.canceled)
        {
            rb.maxLinearVelocity = playerMovement.moveForce / 4;
            Debug.Log("Stop run");
        }
    }
}
