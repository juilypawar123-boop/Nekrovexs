using UnityEngine;

public class VirusSolution : MonoBehaviour
{
    [Header("Manager Reference")]
    public FireZoneManager fireZoneManager;
    public SnowZoneManager snowZoneManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("🧪 Player collected a virus solution!");

            if (fireZoneManager != null)
                fireZoneManager.OnVirusCollected();

            if (snowZoneManager != null)
                snowZoneManager.OnVirusCollected();

            Destroy(gameObject); // remove the collectible
        }
    }
}
