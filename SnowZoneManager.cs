using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SnowZoneManager : MonoBehaviour
{
    [Header("Zombies in Snow Zone")]
    public List<GameObject> zombies = new List<GameObject>();

    [Header("Virus Solutions in Snow Zone")]
    public List<GameObject> virusSolutions = new List<GameObject>();
    public int totalVirusRequired = 6;

    [Header("Final Portal Gate")]
    public GameObject portalGate;
    public Transform winTeleportTarget; // optional

    private int collectedVirusCount = 0;
    private bool virusUnlocked = false;
    private bool portalActivated = false;
    private bool gameFinished = false;

    void Start()
    {
        if (portalGate != null)
            portalGate.SetActive(false);

        // Hide virus collectibles initially
        foreach (var virus in virusSolutions)
            virus.SetActive(false);
    }

    void Update()
    {
        zombies.RemoveAll(z => z == null);

        // After all zombies dead, spawn virus collectibles
        if (zombies.Count == 0 && !virusUnlocked)
        {
            virusUnlocked = true;
            foreach (var virus in virusSolutions)
                virus.SetActive(true);

            Debug.Log("❄️ Zombies defeated! Collect all virus solutions to unlock the final portal.");
        }

        if (collectedVirusCount >= totalVirusRequired && !portalActivated)
        {
            ActivatePortal();
        }
    }

    public void OnVirusCollected()
    {
        collectedVirusCount++;
        Debug.Log($"🧬 Virus collected in SnowZone: {collectedVirusCount}/{totalVirusRequired}");

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
            Debug.Log("🏁 Final portal unlocked! Cross it to finish the game.");
        }
    }

    public void OnPortalEntered(GameObject player)
    {
        if (portalActivated && !gameFinished)
        {
            gameFinished = true;
            Debug.Log("🎉 You win! Game completed!");

            if (winTeleportTarget != null)
                player.transform.position = winTeleportTarget.position;
            else
                SceneManager.LoadScene("WinScene");
        }
        else
        {
            Debug.Log("⚠️ You must collect all virus solutions before entering the portal!");
        }
    }
}
