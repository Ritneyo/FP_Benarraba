using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerRun : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] private Image staminaBar;

    [Header("Parameters")]
    [SerializeField] private float fillRatio;

    #region Variables
    private PlayerMovement playerMovement;
    #endregion
    #region Unity methods
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        CheckStaminaUsage();
    }
    #endregion
    #region Run methods
    public void Run(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            playerMovement.moveForce *= 1.5f;
            playerMovement.isRunning = true;
            Debug.Log("Start run");
        }
        else if (callbackContext.canceled)
        {
            playerMovement.moveForce /= 1.5f;
            playerMovement.isRunning = false;
            Debug.Log("Stop run");
        }
    }

    private void CheckStaminaUsage()
    {
        if (playerMovement.moveForce == 7.5 && playerMovement.isWalking) StaminaUpdate(true);
        else if ((playerMovement.moveForce == 5 || ) && staminaBar.fillAmount != 1) StaminaUpdate(false);
    }

    private void StaminaUpdate(bool minus)
    {
        switch (minus)
        {
            case true:
                staminaBar.fillAmount = staminaBar.fillAmount - Time.deltaTime * fillRatio > 0 ? staminaBar.fillAmount - Time.deltaTime * fillRatio : 0 ;
                break;
            case false:
                staminaBar.fillAmount = staminaBar.fillAmount + Time.deltaTime * fillRatio < 1 ? staminaBar.fillAmount + Time.deltaTime * fillRatio : 1 ;
                break;
        }
    }
    #endregion
}
