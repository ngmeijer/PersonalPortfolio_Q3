using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class ShopItem : MonoBehaviour
{
    private UnityEvent<WeaponType> onPlayerAttemptBuy = new UnityEvent<WeaponType>();
    [SerializeField] private WeaponType weaponTypeType;
    private BoxCollider boxTrigger;
    private XRGrabInteractable interactableItem;
    
    private void Start()
    {
        interactableItem = transform.GetChild(1).GetChild(0).GetComponent<XRGrabInteractable>();
        interactableItem.enabled = false;
        
        onPlayerAttemptBuy.AddListener(ShopManager.Instance.CheckTransaction);
        ShopManager.Instance.onTransactionCheckFinish.AddListener(receiveTransactionValidation);
    }

    private void receiveTransactionValidation(bool pCanBuy) => interactableItem.enabled = pCanBuy;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerHand")) onPlayerAttemptBuy.Invoke(weaponTypeType);
    }
}