using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public Transform muzzle;
    public ParticleSystem muzzleFlash;
    public KeyCode shootKey = KeyCode.Mouse0;
    public float fireRate = 0.2f;
    private float nextFireTime;

    void Update()
    {
        if (Input.GetKey(shootKey) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (muzzleFlash != null && muzzle != null)
        {
            muzzleFlash.transform.position = muzzle.position;
            muzzleFlash.transform.rotation = muzzle.rotation;
            muzzleFlash.Play();
        }
    }
}
