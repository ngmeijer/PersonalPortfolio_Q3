using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static float TimeLeft = 60f;
    public static int EnemiesKilled = 0;
    public static int CurrentGold = 500;
    public static float TotalCurrentScore;

    public static int PistolLevel = 1;
    public static int PistolAmmo = 20;
    public static int PistolDamage = 15;

    public static int ShotgunLevel = 0;
    public static int ShotgunAmmo = 6;
    public static int ShotgunDamage = 70;

    public static int RifleLevel = 0;
    public static int RifleAmmo = 60;
    public static int RifleDamage = 35;

    public static int SwordLevel = 0;
    
    public static int GrenadeLevel = 0;
    public static int GrenadeAmmo = 4;

    public static int PowerUp1;

    public static void UpdateWeaponProperties(WeaponType pWeaponTypeType, WeaponSO pNewProperties)
    {
        switch (pWeaponTypeType)
        {
            case WeaponType.Pistol:
                PistolLevel = pNewProperties.Level;
                PistolDamage = pNewProperties.CurrentDamage;
                PistolAmmo = pNewProperties.CurrentAmmoCount;
                break;
            case WeaponType.AssaultRifle:
                break;
            case WeaponType.Shotgun:
                break;
            case WeaponType.Sword:
                break;
            case WeaponType.Grenade:
                break;
        }
    }
}