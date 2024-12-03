using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRun : MonoBehaviour
{
    #region Variables
    private PlayerMovement playerMovement;
    #endregion
    #region Unity methods
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    #endregion
    #region Run methods
    public void Run(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            playerMovement.moveForce *= 1.5f;
            Debug.Log("Start run");
        }
        else if (callbackContext.canceled)
        {
            playerMovement.moveForce /= 1.5f;
            Debug.Log("Stop run");
        }
    }
    #endregion
}
