using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [Header("Components")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Transform groundDetection;

    [Header("Player stats")]
    public float moveForce = 5f;

    //MoveInput Vector2
    private Vector2 moveInput;

    //State
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool isWalking;
    [HideInInspector] public bool isRunning;
    [HideInInspector] public bool isJumping;
    [HideInInspector] public bool isClimbing;
    #endregion
    #region Unity methods
    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        GroundDetect();
        Move();

        if (Keyboard.current.rKey.wasPressedThisFrame) gameObject.transform.position = GameManager.Instance.checkpoint + Vector3.up;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Present"))
        {
            collision.gameObject.SetActive(false);
            GameManager.Instance.presentsFound += 1;
            GameManager.Instance.checkpoint = gameObject.transform.position;
        }
        else if (collision.gameObject.CompareTag("Despawn"))
        {
            Debug.Log("Collisiona");
            rb.velocity = Vector3.zero;
            gameObject.transform.position = GameManager.Instance.checkpoint + Vector3.up;
        }
    }
    #endregion
    #region My methods
    public void GroundDetect()
    {
        if (GameConstants.GeneralDetectionUnique(groundDetection.transform.position, Vector3.down, 0.1f) ||
            GameConstants.GeneralDetectionUnique(groundDetection.transform.position + Vector3.forward * 0.3f, Vector3.down, 0.5f) ||
            GameConstants.GeneralDetectionUnique(groundDetection.transform.position + Vector3.back * 0.3f, Vector3.down, 0.5f) ||
            GameConstants.GeneralDetectionUnique(groundDetection.transform.position + Vector3.right * 0.3f, Vector3.down, 0.5f) ||
            GameConstants.GeneralDetectionUnique(groundDetection.transform.position + Vector3.left * 0.3f, Vector3.down, 0.5f))
            isGrounded = true;
        else isGrounded = false;
    }
    public void Move()
    {
        if (!GameManager.Instance.inIntro && !GameManager.Instance.inOutro)
        {
            moveInput = playerInput.actions["Movement"].ReadValue<Vector2>();

            if (moveInput != Vector2.zero && isGrounded && !isJumping && !isClimbing)
            {
                isWalking = true;

                Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
                moveDirection = transform.TransformDirection(moveDirection);

                rb.velocity = new Vector3(moveDirection.x * moveForce, rb.velocity.y, moveDirection.z * moveForce);
            }
            else isWalking = false;
        }
    }
    #endregion
}
