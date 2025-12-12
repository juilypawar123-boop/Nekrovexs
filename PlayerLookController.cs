using UnityEngine;

public class PlayerLookController : MonoBehaviour
{
    [Header("References")]
    public Transform playerBody;     // The whole player object that moves
    public Transform cameraTransform; // The camera or camera pivot

    [Header("Settings")]
    public float mouseSensitivity = 200f;
    public float verticalClamp = 80f;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Apply vertical rotation to camera
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -verticalClamp, verticalClamp);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Apply horizontal rotation to player body
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
