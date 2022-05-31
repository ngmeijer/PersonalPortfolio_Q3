using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    [SerializeField] private TextMeshProUGUI pistolCostText;
    [SerializeField] private TextMeshProUGUI pistolNextLevelText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI ammoText;

    [SerializeField] private TextMeshProUGUI goldCountText;

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
        updateWeaponPedestalUI();
    }

    private void updatePlayerStats()
    {
        const string defaultGoldCount = "Gold: ";
        goldCountText.SetText($"{defaultGoldCount}{PlayerStats.CurrentGold}");
    }

    private void updateWeaponPedestalUI()
    {
        const string defaultCostText = "Cost: ";
        const string defaultLevelText = "Next level: ";
        const string defaultDamageText = "Damage: ";
        const string defaultAmmoText = "Ammo: ";
        int newLevel = pistolProperties.Level + 1;
        pistolCostText.SetText($"{defaultCostText}{pistolProperties.AllCosts[newLevel]}");
        pistolNextLevelText.SetText($"{defaultLevelText}{newLevel}");
        damageText.SetText($"{defaultDamageText}{pistolProperties.Damage[newLevel]}");
        ammoText.SetText($"{defaultAmmoText}{pistolProperties.AmmoCount[newLevel]}");

        //Rest of the weapons.
    }

    public void CheckTransaction(WeaponType pWeaponTypeType)
    {
        WeaponSO weapon = getWeaponProperties(pWeaponTypeType);
        if (weapon == null)
        {
            Debug.Log($"Not a valid data object assigned for {weapon}");
            return;
        }
        int weaponCost = weapon.AllCosts[weapon.Level];

        if (PlayerStats.CurrentGold < weaponCost) return;
        weapon.Level++;
        weapon.CurrentDamage = weapon.Damage[weapon.Level];
        weapon.CurrentAmmoCount = weapon.AmmoCount[weapon.Level];
        PlayerStats.CurrentGold -= weaponCost;
        PlayerStats.UpdateWeaponProperties(pWeaponTypeType, weapon);

        updateWeaponPedestalUI();

        //Succesfully bought weapon
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