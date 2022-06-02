using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Weapon Scriptable Object", fileName = "WeaponSO", order = 0)]
public class WeaponSO : ScriptableObject
{
    public string Name = "Unknown";

    public int[] AllCosts = new int[5]
    {
        1, 2, 3, 4, 5
    };

    public int[] Damage = new int[5]
    {
        100, 100, 100, 100, 100
    };

    public int[] AmmoCount = new int[5]
    {
        10, 10, 10, 10, 10
    };

    public int Level;
    public int MaxLevel = 4;

    public int CurrentDamage;
    public int CurrentAmmoCount;

    private void OnEnable()
    {
        Level = 0;
        CurrentDamage = 0;
        CurrentAmmoCount = 0;
    }
}