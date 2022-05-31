using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] [Range(1f, 100f)] private float bulletForce = 20f;
    private float timer;
    private float maxTimeAlive = 3f;

    private void OnEnable()
    {
        timer = 0;
        rb.AddForce(transform.forward * bulletForce, ForceMode.Impulse);
    }

    private void Update()
    {
        if (timer < maxTimeAlive)
        {
            timer += Time.deltaTime;
            return;
        }

        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Weapon")) return;
        if (other.collider.CompareTag("Enemy")) gameObject.SetActive(false);
    }
}