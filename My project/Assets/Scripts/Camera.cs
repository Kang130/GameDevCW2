using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 offset = new Vector3(0, 5, -10); // Camera offset from the player
    public float smoothSpeed = 0.125f; // How smoothly the camera follows the player
    public float mouseSensitivity = 100f; // How sensitive the camera is to mouse movement

    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start()
    {
        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogError("Player reference is not set in the CameraController!");
            return;
        }

        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the camera based on mouse input
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Prevent over-rotation

        // Apply rotation to the camera
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        // Calculate the desired position for the camera
        Vector3 desiredPosition = player.position + transform.rotation * offset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}