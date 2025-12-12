using UnityEngine;
using UnityEngine.SceneManagement; // Only if you're loading a new scene

public class PortalGate : MonoBehaviour
{
    public Transform teleportTarget; // e.g. position inside SnowZone

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (teleportTarget != null)
            {
                other.transform.position = teleportTarget.position;
                Debug.Log("Player teleported to Snow Zone!");
            }
        }
    }
}
