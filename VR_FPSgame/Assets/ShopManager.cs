using System;
using System.Collections;
using System.Collections.Generic;
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

    public void CheckTransaction(Weapons pWeaponType)
    {
        if (PlayerStats.CurrentGold < 100) return;

        int currentWeaponLevel = checkCurrentWeaponLevel(pWeaponType);

        Debug.Log($"Bought a {pWeaponType}. Level {currentWeaponLevel++}");
    }

    private int checkCurrentWeaponLevel(Weapons pWeaponType)
    {
        int currentWeaponLevel = 0;

        switch (pWeaponType)
        {
            case Weapons.Pistol:
                currentWeaponLevel = PlayerStats.PistolLevel;
                break;
            case Weapons.AssaultRifle:
                currentWeaponLevel = PlayerStats.RifleLevel;
                break;
            case Weapons.Shotgun:
                break;
            case Weapons.Sword:
                break;
            case Weapons.Grenade:
                break;
        }

        return currentWeaponLevel;
    }
}