using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    #region Variables
    [Header("Components")]
    [SerializeField] private PlayerMovement playerMovement;

    [Header("Camera parameters")]
    [SerializeField] private float lookSensibility;
    private float horizontalRotation = 0;
    private float verticalRotation = 0;
    [SerializeField] private float maxLookUpAngle;
    [SerializeField] private float maxLookDownAngle;

    //Player look input
    private Vector2 lookInput;
    #endregion
    #region Unity methods
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();    
    }

    private void Update()
    {
        LookAround();
    }
    #endregion
    #region My methods
    public void Look(InputAction.CallbackContext callbackContext)
    {
        lookInput = callbackContext.ReadValue<Vector2>();
    }

    private void LookAround()
    {
        if (!GameManager.Instance.inIntro && !GameManager.Instance.inOutro)
        {
            //Vertical with limits
            horizontalRotation -= lookInput.x * lookSensibility;
            verticalRotation -= lookInput.y * lookSensibility;
            verticalRotation = Mathf.Clamp(verticalRotation, -maxLookDownAngle, maxLookUpAngle);
            transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
            transform.parent.localRotation = Quaternion.Euler(0f, -horizontalRotation, 0f);
        }
    }
    #endregion
}
