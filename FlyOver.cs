using UnityEngine;

public class FlyOver : MonoBehaviour
{
    [Header("Flight Settings")]
    public float speed = 50f;          // Speed of the aircraft
    public float heightOffset = 300f;  // How high above the waypoints it flies
    public bool loop = true;           // Should the plane loop back and forth?

    [Header("Waypoints (Drag & Drop)")]
    public Transform startPoint;       // Drag PointA here
    public Transform endPoint;         // Drag PointB here

    private Vector3 currentTarget;

    void Start()
    {
        if (startPoint == null || endPoint == null)
        {
            Debug.LogError("❌ FlyOver: Please assign Start Point and End Point in the Inspector!");
            enabled = false;
            return;
        }

        // Initialize position
        Vector3 startPos = startPoint.position + new Vector3(0, heightOffset, 0);
        transform.position = startPos;
        currentTarget = endPoint.position + new Vector3(0, heightOffset, 0);
        transform.LookAt(currentTarget);
    }

    void Update()
    {
        // Move towards the current target
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

        // Check if we reached the target
        if (Vector3.Distance(transform.position, currentTarget) < 5f)
        {
            if (loop)
            {
                // Swap target points
                Transform temp = startPoint;
                startPoint = endPoint;
                endPoint = temp;

                currentTarget = endPoint.position + new Vector3(0, heightOffset, 0);
                transform.LookAt(currentTarget);
            }
            else
            {
                enabled = false; // Stop flying
            }
        }
    }
}
