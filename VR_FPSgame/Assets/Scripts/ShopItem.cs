using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class ShopItem : MonoBehaviour
{
    private UnityEvent<WeaponType> onPlayerAttemptBuy = new UnityEvent<WeaponType>();
    [SerializeField] private WeaponType weaponType;
    private BoxCollider boxTrigger;
    private XRGrabInteractable interactableItem;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI nextLevelText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI ammoText;

    private void Awake()
    {
        ShopManager.Instance.onGameStartUpdateWeaponUI.AddListener(updateItemUI);
    }

    private void Start()
    {
        interactableItem = transform.GetChild(1).GetChild(0).GetComponent<XRGrabInteractable>();
        interactableItem.enabled = false;

        onPlayerAttemptBuy.AddListener(ShopManager.Instance.CheckTransaction);
        ShopManager.Instance.onTransactionCheckFinish.AddListener(receiveTransactionValidation);
    }

    private void receiveTransactionValidation(bool pCanBuy, WeaponSO pWeaponSo)
    {
        interactableItem.enabled = pCanBuy;
        if(pCanBuy) updateItemUI(pWeaponSo);
    }

    private void updateItemUI(WeaponSO pWeapon)
    {
        if (pWeapon.WeaponType != weaponType) return;

        if (pWeapon.Level < pWeapon.MaxLevel)
        {
            int expectedLevel = pWeapon.Level;
            expectedLevel++;
            nameText.SetText($"{pWeapon.WeaponName}");
            costText.SetText($"Cost: \n{pWeapon.AllCosts[expectedLevel]}");
            nextLevelText.SetText($"Next level: \n{expectedLevel}");
            damageText.SetText($"Next damage: \n{pWeapon.AllDamage[expectedLevel]}");
            if (pWeapon.WeaponType != WeaponType.Sword)
                ammoText.SetText($"Next ammo: \n{pWeapon.AllAmmo[expectedLevel]}");
            else ammoText.SetText("Ammo: N/A");
        }
        else
        {
            costText.SetText("");
            nextLevelText.SetText("Max level reached!");
            damageText.SetText("");
            costText.SetText("");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand")) onPlayerAttemptBuy.Invoke(weaponType);
    }
}