using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    public WeaponType weaponType;
    public UnityEvent<WeaponType> onWeaponAttemptStore = new UnityEvent<WeaponType>();
    private Rigidbody rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void EquippedWeapon()
    {
        rb.constraints = RigidbodyConstraints.None;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onWeaponAttemptStore.Invoke(weaponType);
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
