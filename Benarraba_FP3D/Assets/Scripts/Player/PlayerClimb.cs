using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerClimb : MonoBehaviour
{
    #region Variables
    [Header("Components")]
    [SerializeField] private CapsuleCollider playerCapsuleCollider;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerMovement playerMovement;
    private RaycastHit raycastHitClimb;

    [Header("Parameters")]
    [SerializeField] private float climbMinDistance;
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
        if (callbackContext.performed && !playerMovement.isClimbing && GameConstants.GeneralDetectionUnique(transform.position, transform.forward, playerCapsuleCollider.radius + 0.5f))
        {
            raycastHitClimb = GameConstants.RaycastInformation(transform.position, transform.forward, playerCapsuleCollider.radius + 0.5f);

            playerCapsuleCollider.transform.position += playerCapsuleCollider.transform.forward * (raycastHitClimb.distance - playerCapsuleCollider.radius - 0.1f);

            Debug.Log(raycastHitClimb.collider.GetComponent<ClimbWall>().upperTransform.position.y);

            if (raycastHitClimb.collider.GetComponent<ClimbWall>().upperTransform.position.y - transform.position.y < climbMinDistance)
            {
                playerMovement.isClimbing = true;
                rb.velocity = Vector3.zero;
                StartCoroutine(PerformClimb(raycastHitClimb.collider.GetComponent<ClimbWall>().upperTransform.position.y - transform.position.y + 1,
                                            transform.forward * 300));
            }
        }
    }

    private IEnumerator PerformClimb(float upDistance, Vector3 forwardForce)
    {
        yield return null;

        for (float i = 0; i <= upDistance; i += upDistance / 10)
        {
            transform.position += new Vector3(  0,
                                                upDistance / 10,
                                                0);
            yield return new WaitForSecondsRealtime(0.01f);
        }

        rb.AddForce(forwardForce);

        playerMovement.isClimbing = false;

        yield return null;
    }
    #endregion
}
