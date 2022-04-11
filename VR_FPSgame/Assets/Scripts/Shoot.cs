using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnpoint;
    [SerializeField] [Range(1f, 100f)] private float bulletForce = 20f;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private AudioSource gunshotSound;
    private bool muzzleFlashEnabled = true;
    private bool soundEffectEnabled = true;
    private float timer;
    private float delay = 1f;

    public bool TestShootMode;

    private void Start()
    {
        if (muzzleFlash == null) muzzleFlashEnabled = false;
        else muzzleFlash.Stop();

        if (gunshotSound == null) soundEffectEnabled = false;
    }

    private void Update()
    {
        if (TestShootMode)
        {
            timer += Time.deltaTime;
            if (timer > delay)
            {
                ShootBullet();
                timer = 0;
            }
        }
    }

    public void ShootBullet()
    {
        if (muzzleFlashEnabled) muzzleFlash.Play();
        if(soundEffectEnabled) gunshotSound.Play();
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnpoint.position, bulletSpawnpoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletForce, ForceMode.Impulse);
        // if (muzzleFlashEnabled) muzzleFlash.Stop();
    }
}