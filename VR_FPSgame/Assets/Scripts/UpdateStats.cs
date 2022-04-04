using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpdateStats : MonoBehaviour
{
    public UnityEvent timeLeftEvent;
    
    private void Start()
    {
        StartCoroutine(countDown());
    }

    private IEnumerator countDown()
    {
        if (PlayerStats.TimeLeft > 0)
        {
            PlayerStats.TimeLeft -= 1;
        }

        timeLeftEvent.Invoke();
        yield return new WaitForSeconds(1);
        StartCoroutine(countDown());
    }
}