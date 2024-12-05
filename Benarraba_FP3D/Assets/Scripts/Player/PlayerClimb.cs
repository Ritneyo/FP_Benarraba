using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerClimb : MonoBehaviour
{
    #region Variables
    [Header("Components")]
    [SerializeField] private CapsuleCollider playerCapsuleCollider;
    private RaycastHit raycastHitClimb;

    [Header("Parameters")]
    [SerializeField] private float raycastDistance;
    #endregion
    #region Unity methods
    private void Update()
    {
        if (GameConstants.GeneralDetectionUnique(transform.position, transform.forward, playerCapsuleCollider.radius + 0.5f))
        {
            Debug.Log("Detect");
        }
    }
    #endregion
    #region My methods
    public void Climb(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed && GameConstants.GeneralDetectionUnique(transform.position, transform.forward, playerCapsuleCollider.radius + 0.5f))
        {
            raycastHitClimb = GameConstants.RaycastInformation(transform.position, transform.forward, playerCapsuleCollider.radius + 0.5f);

            playerCapsuleCollider.transform.position += playerCapsuleCollider.transform.forward * (raycastHitClimb.distance - playerCapsuleCollider.radius);

            //raycastHitClimb.collider.transform
        }
    }
    #endregion
}
