using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb; 
    [SerializeField] [Range(1f, 100f)] private float bulletForce = 20f;

    
    private void Start()
    {
        rb.AddForce(transform.forward * bulletForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Untagged")) Destroy(this);
        if (other.collider.CompareTag("Enemy")) Destroy(this);
    }
}