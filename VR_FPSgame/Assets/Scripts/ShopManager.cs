using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public enum WeaponType
{
    Pistol,
    AssaultRifle,
    Shotgun,
    Sword,
    Grenade
}

public class ShopManager : MonoBehaviour
{
    private static ShopManager _instance;

    public static ShopManager Instance
    {
        get { return _instance; }
    }

    public WeaponSO pistolProperties;
    public WeaponSO rifleProperties;
    public WeaponSO shotgunProperties;
    public WeaponSO swordProperties;

    [SerializeField] private TextMeshProUGUI goldCountText;
    [SerializeField] private TextMeshProUGUI lastActionText;

    public UnityEvent<WeaponSO> onGameStartUpdateWeaponUI = new UnityEvent<WeaponSO>();
    public UnityEvent<bool, WeaponSO> onTransactionCheckFinish = new UnityEvent<bool, WeaponSO>();
    private WeaponType lastWeaponBought;
    private int lastItemPrice;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        updatePlayerStats();
        onGameStartUpdateWeaponUI.Invoke(pistolProperties);
        onGameStartUpdateWeaponUI.Invoke(rifleProperties);
        onGameStartUpdateWeaponUI.Invoke(shotgunProperties);
        onGameStartUpdateWeaponUI.Invoke(swordProperties);
    }

    private void updatePlayerStats()
    {
        const string defaultGoldCount = "Gold: ";
        goldCountText.SetText($"{defaultGoldCount}{PlayerStats.CurrentGold}");
        lastActionText.SetText($"Spent {lastItemPrice} on {lastWeaponBought}");
    }

    public void CheckTransaction(WeaponType pWeaponType)
    {
        WeaponSO weapon = getWeaponProperties(pWeaponType);
        if (weapon == null)
        {
            Debug.LogError($"Not a valid data object assigned for {pWeaponType}");
            onTransactionCheckFinish.Invoke(false, weapon);
            return;
        }

        if (weapon.Level >= weapon.MaxLevel) return;

        int weaponCost = weapon.AllCosts[weapon.Level];

        if (PlayerStats.CurrentGold < weaponCost)
        {
            onTransactionCheckFinish.Invoke(false, weapon);
            return;
        }

        onTransactionCheckFinish.Invoke(true, weapon);

        weapon.Level++;
        weapon.CurrentDamage = weapon.AllDamage[weapon.Level];
        weapon.CurrentAmmoCount = weapon.AllAmmo[weapon.Level];
        PlayerStats.CurrentGold -= weaponCost;
        PlayerStats.UpdateWeaponProperties(pWeaponType, weapon);
        lastWeaponBought = pWeaponType;
        lastItemPrice = weaponCost;

        updatePlayerStats();
    }

    private WeaponSO getWeaponProperties(WeaponType pWeaponTypeType)
    {
        switch (pWeaponTypeType)
        {
            case WeaponType.Pistol:
                return pistolProperties;
            case WeaponType.AssaultRifle:
                return rifleProperties;
            case WeaponType.Shotgun:
                return shotgunProperties;
            case WeaponType.Sword:
                break;
            case WeaponType.Grenade:
                break;
        }

        return null;
    }
}