using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    #region Variables
    [Header("Components")]
    [SerializeField] private Transform groundDetection;

    [Header("Parameters")]
    [SerializeField] private float jumpForce;
    private Rigidbody rb;

    //States
    private bool isJumping;
    #endregion
    #region Unity methods
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");

        if (GameConstants.GeneralDetectionUnique(groundDetection.transform.position, Vector3.down, 0.5f/*, GameConstants.layerGround*/))
        {
            Debug.Log("Suelo");
            
        }
    }
    #endregion
    #region Jump methods
    public void Jump(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            isJumping = true;
            rb.AddForce(new Vector3(0, jumpForce, 0));
        }
    }
    #endregion
}
