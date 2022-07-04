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

    public static GameManager Instance
    {
        get { return _instance; }
    }

    public bool EnableShutdownAfterDelay;
    public float Delay = 10f;
    private float timer;
    private bool hasStarted;
    [SerializeField] private bool shutDownNeedsInput = false;


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

    private void Update()
    {
        if (EnableShutdownAfterDelay)
        {
            if (shutDownNeedsInput)
            {
                if (!Input.GetKeyDown(KeyCode.Space)) return;
            }
            
            if (timer < Delay)
            {
                timer += Time.deltaTime;
            }
            else Application.Quit();
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
    }
#endif
}