using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public List<Waypoint> patrolPoints;
    
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        
    }
#endif
}