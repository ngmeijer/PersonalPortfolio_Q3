using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class MovePlayerForward : MonoBehaviour
{
    [SerializeField] private bool enableTest;

    [SerializeField] private float runTime = 10f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private XROrigin standardController;
    private float timer;
    private bool hasStarted;

    private void Start()
    {
        if(enableTest) standardController.enabled = false;
    }

    private void Update()
    {
        if (enableTest)
        {
            if (Input.GetKeyDown(KeyCode.Space)) hasStarted = true;
            
            if (!hasStarted) return;
            
            if (timer < runTime)
            {
                var position = transform.position;
                position +=
                    new Vector3(speed, 0, 0) * Time.deltaTime;
                transform.position = position;
                timer += Time.deltaTime;
            }
            else Application.Quit();
        }
    }
}