using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShopItem : MonoBehaviour
{
    private UnityEvent<Weapons> onPlayerAttemptBuy = new UnityEvent<Weapons>();
    [SerializeField] private Weapons weaponType;
    private BoxCollider boxTrigger;
    
    private void Start()
    {
        onPlayerAttemptBuy.AddListener(ShopManager.Instance.CheckTransaction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerHand")) onPlayerAttemptBuy.Invoke(weaponType);
    }
}
