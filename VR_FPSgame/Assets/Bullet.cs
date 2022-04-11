using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Untagged")) Destroy(this);
        if (other.collider.CompareTag("Enemy")) Destroy(this);
    }
}
