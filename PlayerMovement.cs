using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 500f;
    public float sprintMultiplier = 1.5f;
    public float jumpForce = 5f;
    public float groundDrag = 5f;

    [Header("Ground Check")]
    public float playerHeight = 2f;
    public LayerMask whatIsGround;
    private bool grounded;

    [Header("References")]
    public Transform orientation;   // Usually the camera or an empty forward object
    public Camera playerCamera;     // First-person camera
    private Rigidbody rb;

    // Input
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        HandleInput();
        HandleGroundCheck();
        HandleDrag();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void HandleInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Jump
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);  // Reset Y velocity
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void HandleGroundCheck()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
    }

    private void HandleDrag()
    {
        rb.linearDamping = grounded ? groundDrag : 0f;
    }

    private void MovePlayer()
    {
        if (orientation == null) return;

        // Calculate movement relative to orientation
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        moveDirection.y = 0f; // prevent vertical movement

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? moveSpeed * sprintMultiplier : moveSpeed;

        rb.AddForce(moveDirection.normalized * currentSpeed, ForceMode.Force);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * (playerHeight * 0.5f + 0.2f));
    }
}
