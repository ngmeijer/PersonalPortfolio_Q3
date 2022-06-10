using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Weapon Scriptable Object", fileName = "WeaponSO", order = 0)]
public class WeaponSO : ScriptableObject
{
    public string WeaponName = "Unknown";
    public WeaponType WeaponType;

    public int[] AllCosts = new int[5]
    {
        1, 2, 3, 4, 5
    };

    public int[] AllDamage = new int[5]
    {
        100, 100, 100, 100, 100
    };

    public int[] AllAmmo = new int[5]
    {
        10, 10, 10, 10, 10
    };

    public int Level;
    public int MaxLevel = 4;

    public int CurrentDamage;
    public int CurrentAmmoCount;
}