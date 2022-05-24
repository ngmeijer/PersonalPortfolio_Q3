using UnityEngine;

[CreateAssetMenu(menuName = "Create Weapon Scriptable Object", fileName = "WeaponSO", order = 0)]
public class WeaponSO : ScriptableObject
{
    public string Name;
    public int[] AllCosts;
    public int[] Damage;
    public int[] AmmoCount;
    public int Level = -1;

    public int CurrentDamage;
    public int CurrentAmmoCount;
}