using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager _instance;

    public static InventoryManager Instance
    {
        get { return _instance; }
    }

    [SerializeField] private List<Transform> weaponSlots;
    private Dictionary<WeaponType, GameObject> weaponLinks = new Dictionary<WeaponType, GameObject>();

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
        Weapon[] foundWeapons = FindObjectsOfType<Weapon>();
        foreach (var weapon in foundWeapons)
        {
            weapon.onWeaponAttemptStore.AddListener(storeWeaponInSlot);
            weaponLinks.Add(weapon.weaponType, weapon.gameObject);
        }
    }

    private void storeWeaponInSlot(WeaponType pWeaponTypeType)
    {
        weaponLinks.TryGetValue(pWeaponTypeType, out GameObject weaponInstance);
        if (weaponInstance == null) return;
        switch (pWeaponTypeType)
        {
            case WeaponType.Pistol:
                weaponInstance.transform.SetParent(weaponSlots[0]);
                break;
            case WeaponType.AssaultRifle:
                weaponInstance.transform.SetParent(weaponSlots[0]);
                break;
            case WeaponType.Shotgun:
                weaponInstance.transform.SetParent(weaponSlots[0]);
                break;
            case WeaponType.Sword:
                weaponInstance.transform.SetParent(weaponSlots[0]);
                break;
            case WeaponType.Grenade:
                weaponInstance.transform.SetParent(weaponSlots[0]);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(pWeaponTypeType), pWeaponTypeType, null);
        }
        
        weaponInstance.transform.localPosition = new Vector3(0, 0, 0);
        weaponInstance.transform.rotation = weaponInstance.transform.parent.rotation;
    }
}