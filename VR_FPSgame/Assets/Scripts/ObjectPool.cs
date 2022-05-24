using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> pooledItems;
    public GameObject itemToPool;
    public int amountToPool;
    
    private static ObjectPool _instance;
    public static ObjectPool Instance => _instance;

    private void Awake()
    {
        
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject item = Instantiate(itemToPool);
            item.SetActive(false);
            pooledItems.Add(item);
        }
    }

    public GameObject GetPooledItem()
    {
        return null;
    }
}
