using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dashDistance = 10f; // How far the player dashes
    public float dashDuration = 0.2f; // How long the dash lasts
    public float dashCooldown = 1f; // Cooldown between dashes

    private float lastDashTime = -Mathf.Infinity; // Time of the last dash
    private bool isDashing = false; // Whether the player is currently dashing
    private Vector3 dashDirection; // Direction of the dash

    private Rigidbody rb;
    private Transform cameraTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform; // Get the main camera's transform
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing) // Change this to your preferred input
        {
            if (Time.time > lastDashTime + dashCooldown)
            {
                StartDash();
            }
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            // Apply dash velocity
            rb.linearVelocity = dashDirection * (dashDistance / dashDuration);
        }
    }

    void StartDash()
    {
        // Calculate dash direction based on the camera's forward direction
        dashDirection = cameraTransform.forward;
        dashDirection.y = 0; // Ensure the dash stays horizontal
        dashDirection.Normalize();

        // Start the dash
        isDashing = true;
        lastDashTime = Time.time;

        // Stop the dash after the duration
        Invoke(nameof(StopDash), dashDuration);
    }

    void StopDash()
    {
        isDashing = false;
        rb.linearVelocity = Vector3.zero; // Stop the player after the dash
    }
}