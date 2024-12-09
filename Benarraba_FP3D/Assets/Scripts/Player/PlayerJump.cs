using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    #region Variables
    [Header("Components")]
    private PlayerMovement playerMovement;

    [Header("Parameters")]
    [SerializeField] private float jumpForce;
    private Rigidbody rb;

    #endregion
    #region Unity methods
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        playerMovement.GroundDetect();
        if (playerMovement.isGrounded)
        {
            Debug.Log("Suelo");
            playerMovement.isJumping = false;
        }
    }
    #endregion
    #region Jump methods
    public void Jump(InputAction.CallbackContext callbackContext)
    {
        if (!GameManager.Instance.inIntro && !GameManager.Instance.inOutro)
        {
            if (callbackContext.performed && !playerMovement.isJumping)
            {
                playerMovement.isJumping = true;
                rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Force);
            }
        }
    }
    #endregion
}
