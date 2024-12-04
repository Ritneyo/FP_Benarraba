using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerRun : MonoBehaviour
{
    #region Variables
    [Header("HUD")]
    [SerializeField] private Image staminaBar;

    [Header("Parameters")]
    [SerializeField] private float fillRatio;
    private float oriMoveForce;

    private PlayerMovement playerMovement;
    #endregion
    #region Unity methods
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        oriMoveForce = playerMovement.moveForce;
    }

    private void Update()
    {
        Debug.Log(playerMovement.moveForce);
        CheckStaminaUsage();
    }
    #endregion
    #region Run methods
    public void Run(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            playerMovement.moveForce = oriMoveForce * 1.5f;
            playerMovement.isRunning = true;
            Debug.Log("Start run");
        }
        else if (callbackContext.canceled)
        {
            playerMovement.moveForce = oriMoveForce;
            playerMovement.isRunning = false;
            Debug.Log("Stop run");
        }
    }

    private void CheckStaminaUsage()
    {
        if (playerMovement.isRunning && playerMovement.isWalking && staminaBar.fillAmount > 0) StaminaUpdate(true);
        else if (((playerMovement.isRunning && !playerMovement.isWalking) || (!playerMovement.isRunning && playerMovement.isWalking) || !playerMovement.isRunning) &&
            staminaBar.fillAmount != 1) StaminaUpdate(false);
    }

    private void StaminaUpdate(bool minus)
    {
        switch (minus)
        {
            case true:
                staminaBar.fillAmount = staminaBar.fillAmount - Time.deltaTime * fillRatio > 0 ? staminaBar.fillAmount - Time.deltaTime * fillRatio : 0 ;
                if (staminaBar.fillAmount == 0) playerMovement.moveForce = oriMoveForce;
                break;
            case false:
                staminaBar.fillAmount = staminaBar.fillAmount + Time.deltaTime * fillRatio < 1 ? staminaBar.fillAmount + Time.deltaTime * fillRatio : 1 ;
                break;
        }
    }
    #endregion
}
