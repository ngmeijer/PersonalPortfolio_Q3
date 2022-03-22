using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateCanvas : MonoBehaviour
{
    [SerializeField] private UpdateStats updateStats;

    [SerializeField] private TextMeshProUGUI timeLeftText;
    
    private void Start()
    {
        updateStats.timeLeftEvent.AddListener(updateTimeLeft);
    }

    private void updateTimeLeft()
    {
        timeLeftText.text = PlayerStats.TimeLeft.ToString();
    }
}
