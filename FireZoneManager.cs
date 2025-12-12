using UnityEngine;
using System.Collections.Generic;

public class FireZoneManager : MonoBehaviour
{
    [Header("Zombies in Fire Zone")]
    public List<GameObject> zombies = new List<GameObject>();

    [Header("Virus Solutions in Fire Zone")]
    public List<GameObject> virusSolutions = new List<GameObject>();
    public int totalVirusRequired = 3;

    [Header("Portal Gate to Snow Zone")]
    public GameObject portalGate;

    private int collectedVirusCount = 0;
    private bool portalActivated = false;

    void Start()
    {
        if (portalGate != null)
            portalGate.SetActive(false); // hide or lock portal initially

        // Hide all virus collectibles until zombies are dead
        foreach (var virus in virusSolutions)
            virus.SetActive(false);
    }

    void Update()
    {
        zombies.RemoveAll(z => z == null);

        // Once all zombies dead, show virus solutions
        if (zombies.Count == 0 && !portalActivated && collectedVirusCount < totalVirusRequired)
        {
            foreach (var virus in virusSolutions)
                virus.SetActive(true);

            Debug.Log("🔥 All zombies defeated! Collect all virus samples to unlock the portal.");
        }

        // If player has collected all virus samples
        if (collectedVirusCount >= totalVirusRequired && !portalActivated)
        {
            ActivatePortal();
        }
    }

    public void OnVirusCollected()
    {
        collectedVirusCount++;
        Debug.Log($"🧬 Virus collected in FireZone: {collectedVirusCount}/{totalVirusRequired}");

        if (collectedVirusCount >= totalVirusRequired)
        {
            ActivatePortal();
        }
    }

    void ActivatePortal()
    {
        portalActivated = true;
        if (portalGate != null)
        {
            portalGate.SetActive(true);
            Debug.Log("🚪 Portal to Snow Zone unlocked!");
        }
    }
}
