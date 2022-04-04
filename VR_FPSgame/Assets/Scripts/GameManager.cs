using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static UnityEvent<object> onDebugMessage = new UnityEvent<object>();
    private string latestDebugMessage = "nothing received yet";
    public Transform debugMessagePosition;
    
    private void Start()
    {
        onDebugMessage.AddListener(receiveDebugMessage);
    }

    private void receiveDebugMessage(object pMessage)
    {
        latestDebugMessage = pMessage.ToString();
    }

    private void OnDrawGizmos()
    {
        GUIStyle style = new GUIStyle {fontSize = 75};
        Handles.color = Color.white;
        Handles.Label(debugMessagePosition.position, latestDebugMessage, style);
    }
}
