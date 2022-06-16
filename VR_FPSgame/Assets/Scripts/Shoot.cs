using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private ProjectileType bulletType;
    [SerializeField] private Transform bulletSpawnpoint;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private AudioSource gunshotSound;
    [SerializeField] private GameObject bulletPrefab;
    private bool muzzleFlashEnabled = true;
    private bool soundEffectEnabled = true;
    private float timer;
    private float delay = 0.1f;

    public bool TestShootMode;
    public bool EnableObjectPooling = true;
    private bool hasStarted;

    private void Start()
    {
        if (TestShootMode) GetComponent<Rigidbody>().useGravity = false;
        if (muzzleFlash == null) muzzleFlashEnabled = false;
        if (gunshotSound == null) soundEffectEnabled = false;
    }

    private void Update()
    {
        if (TestShootMode)
        {
            if (Input.GetKeyDown(KeyCode.Space)) hasStarted = true;

            if (hasStarted)
            {
                timer += Time.deltaTime;
                if (timer > delay)
                {
                    ShootBullet();
                    timer = 0;
                }
            }
        }
    }

    public void ShootBullet()
    {
        if (muzzleFlashEnabled) muzzleFlash.Play();
        if (soundEffectEnabled) gunshotSound.Play();
        if (EnableObjectPooling)
        {
            GameObject bullet = ObjectPool.Instance.GetPooledItem(bulletType);
            if (bullet != null)
            {
                bullet.transform.position = bulletSpawnpoint.position;
                bullet.transform.rotation = bulletSpawnpoint.rotation;
                bullet.SetActive(true);
            }
        }
        else Instantiate(bulletPrefab, bulletSpawnpoint.position, bulletSpawnpoint.rotation);
    }
}