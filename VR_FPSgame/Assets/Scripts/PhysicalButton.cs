using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicalButton : MonoBehaviour
{
    public UnityEvent onHandCollision;

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("PlayerHand")) onHandCollision.Invoke();
    }
}
