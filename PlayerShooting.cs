using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    [Header("Player Setup")]
    public Camera playerCamera;
    public float shootRange = 50f;
    public int damage = 20;
    public KeyCode shootKey = KeyCode.Mouse0;
    public GameObject bloodPrefab;

    [Header("Audio")]
    public AudioClip shootAudioClip;
    public AudioSource gunAudioSource;

    [Header("Raycast Filtering")]
    public LayerMask shootableLayers;

    [Header("Gun Effects")]
    public Light gunLight;                 // Assign your point light here
    public float lightDuration = 0.05f;    // Duration of the muzzle flash

    void Update()
    {
        if (Input.GetKeyDown(shootKey))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Play gunshot sound
        if (shootAudioClip != null && gunAudioSource != null)
        {
            gunAudioSource.PlayOneShot(shootAudioClip);
        }

        // Flash light
        if (gunLight != null)
        {
            StartCoroutine(FlashLight());
        }

        // Raycast
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, shootRange, shootableLayers))
        {
            Debug.Log("Hit: " + hit.collider.name);

            if (bloodPrefab != null)
            {
                Instantiate(bloodPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            }

            ZombieHealth zombie = hit.collider.GetComponentInParent<ZombieHealth>();
            if (zombie != null)
            {
                zombie.TakeDamage(damage);
            }
        }

        Debug.DrawRay(ray.origin, ray.direction * shootRange, Color.red, 1f);
    }

    IEnumerator FlashLight()
    {
        gunLight.enabled = true;
        yield return new WaitForSeconds(lightDuration);
        gunLight.enabled = false;
    }
}
