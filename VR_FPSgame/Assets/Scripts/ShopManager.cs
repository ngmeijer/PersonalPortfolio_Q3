using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Weapons
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

    [SerializeField] private TextMeshProUGUI pistolCost;
    [SerializeField] private TextMeshProUGUI pistolNextLevel;

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

    private void updateWeaponPedestalUI()
    {
        string defaultCostText = "Cost: ";
        string defaultLevelText = "Next level: ";
        pistolCost.SetText($"{defaultCostText}{pistolProperties.AllCosts[pistolProperties.Level].ToString()}");
        pistolNextLevel.SetText($"{defaultLevelText}{pistolProperties.Level++}");
        
        //Rest of the weapons.
    }

    public void CheckTransaction(Weapons pWeaponType)
    {
        WeaponSO weapon = getWeaponProperties(pWeaponType);
        int weaponCost = weapon.AllCosts[weapon.Level++];

        if (PlayerStats.CurrentGold < weaponCost) return;
        weapon.Level++;
        weapon.CurrentDamage = weapon.Damage[weapon.Level];
        weapon.CurrentAmmoCount = weapon.AmmoCount[weapon.Level];
        PlayerStats.CurrentGold -= weaponCost;
        PlayerStats.UpdateWeaponProperties(pWeaponType, weapon);
        
        updateWeaponPedestalUI();

        //Succesfully bought weapon
    }

    private WeaponSO getWeaponProperties(Weapons pWeaponType)
    {
        switch (pWeaponType)
        {
            case Weapons.Pistol:
                return pistolProperties;
            case Weapons.AssaultRifle:
                return rifleProperties;
            case Weapons.Shotgun:
                return shotgunProperties;
                break;
            case Weapons.Sword:
                break;
            case Weapons.Grenade:
                break;
        }

        return null;
    }
}