using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType
{
    PistolBullet,
    RifleBullet,
    ShotgunShell,
    Grenade,
    None,
}

public class ObjectPool : MonoBehaviour
{
    [Header("Pistol")]
    public List<GameObject> pooledPistolBullets;
    public ObjectPoolItem pistolBullet;

    [Header("Rifle")]
    public ObjectPoolItem rifleBullet;
    public List<GameObject> pooledRifleBullets;
    
    [Header("Shotgun")]
    public ObjectPoolItem shotgunShell;
    
    [Header("Grenade")]
    public ObjectPoolItem grenade;

    [Space(5)]
    public ProjectileType lastProjectileType = ProjectileType.None;
    private List<GameObject> collectionToTakeFrom;

    private static ObjectPool _instance;
    public static ObjectPool Instance => _instance;

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

        instantiateProjectiles(pistolBullet, pooledPistolBullets);
        instantiateProjectiles(rifleBullet, pooledRifleBullets);
    }

    private void instantiateProjectiles(ObjectPoolItem pItem, List<GameObject> pItemsInstantiated)
    {
        for (int i = 0; i < pItem.AmountToPool; i++)
        {
            GameObject item = Instantiate(pItem.Prefab);
            item.SetActive(false);
            pItemsInstantiated.Add(item);
        }
    }

    public GameObject GetPooledItem(ProjectileType pType)
    {
        if (pType != lastProjectileType)
        {
            switch (pType)
            {
                case ProjectileType.PistolBullet:
                    collectionToTakeFrom = pooledPistolBullets;
                    break;
                case ProjectileType.RifleBullet:
                    collectionToTakeFrom = pooledRifleBullets;
                    break;
                case ProjectileType.ShotgunShell:
                    break;
                case ProjectileType.Grenade:
                    break;
            }

            lastProjectileType = pType;
        }

        for (int i = 0; i < collectionToTakeFrom.Count; i++)
        {
            if (!collectionToTakeFrom[i].activeInHierarchy)
                return collectionToTakeFrom[i];
        }

        return null;
    }
}

[Serializable]
public class ObjectPoolItem
{
    public GameObject Prefab;
    public int AmountToPool;
}