using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShopItem : MonoBehaviour
{
    private UnityEvent<WeaponType> onPlayerAttemptBuy = new UnityEvent<WeaponType>();
    [SerializeField] private WeaponType weaponTypeType;
    private BoxCollider boxTrigger;
    
    private void Start()
    {
        onPlayerAttemptBuy.AddListener(ShopManager.Instance.CheckTransaction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerHand")) onPlayerAttemptBuy.Invoke(weaponTypeType);
    }
}
