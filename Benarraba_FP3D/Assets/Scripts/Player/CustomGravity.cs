using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomGravity : MonoBehaviour
{
    private PlayerMovement mMovement;

    // Gravity Scale editable on the inspector
    // providing a gravity scale per object

    public float gravityScale = 1.0f;

    // Global Gravity doesn't appear in the inspector. Modify it here in the code
    // (or via scripting) to define a different default gravity for all objects.

    public static float globalGravity = -9.81f;

    Rigidbody m_rb;

    void OnEnable()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.useGravity = false;
    }

    private void Start()
    {
        mMovement = GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        if (!mMovement.isClimbing)
        {
            m_rb.AddForce(gravity, ForceMode.Acceleration);
        }
    }
}