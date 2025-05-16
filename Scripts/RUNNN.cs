using UnityEngine;

public class RUNNN : MonoBehaviour
{
    public float moveSpeed = 40f;
    public float acceleration = 50f;
    public float deceleration = 40f;
    public float maxSpeed = 100f;
    public float rotationSpeed = 10f; // Speed at which the character rotates

    private Rigidbody rb;
    private Transform cameraTransform;
    private Vector3 moveDirection;
    private Animator animator; // Reference to the Animator component

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform; // Get the main camera's transform
        animator = GetComponent<Animator>(); // Get the Animator component
    }

    void Update()
    {
        // Get input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate movement direction relative to the camera
        moveDirection = (cameraTransform.forward * moveZ + cameraTransform.right * moveX).normalized;
        moveDirection.y = 0; // Ensure the player doesn't move vertically

        // Update the Animator
        if (animator != null)
        {
            float speed = rb.linearVelocity.magnitude;
            if (speed < 0.1f) speed = 0f; // Deadzone for idle
            animator.SetFloat("Speed", speed); // Set the Speed parameter
            
        }
    }

    void FixedUpdate()
    {
        // Calculate target velocity
        Vector3 targetVelocity = moveDirection * moveSpeed;

        rb.linearVelocity = Vector3.MoveTowards(rb.linearVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);

        // Limit speed
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }

        // Apply deceleration when no input
        if (moveDirection.magnitude == 0)
        {
            rb.linearVelocity = Vector3.MoveTowards(rb.linearVelocity, Vector3.zero, deceleration * Time.fixedDeltaTime);
        }

        // Rotate the character toward the camera's forward direction
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0; // Ensure the character doesn't tilt up or down
        if (cameraForward != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }
}