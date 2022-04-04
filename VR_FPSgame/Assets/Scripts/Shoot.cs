using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnpoint;
    [SerializeField] [Range(1f, 100f)] private float bulletForce = 20f;
    [SerializeField] private ParticleSystem muzzleFlashEnabled;
    private bool playMuzzleFlash = true;
    
    private void Start()
    {
        if (muzzleFlashEnabled == null)
        {
            playMuzzleFlash = false;
        }
    }

    public void ShootBullet()
    {
        if(muzzleFlashEnabled) muzzleFlashEnabled.Play();
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnpoint.position, bulletSpawnpoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletForce, ForceMode.Impulse);
        if(muzzleFlashEnabled) muzzleFlashEnabled.Stop();
    }
}