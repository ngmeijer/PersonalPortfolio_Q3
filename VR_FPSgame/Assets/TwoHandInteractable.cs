using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class TwoHandInteractable : XRGrabInteractable
{
    public List<XRSimpleInteractable> secondHandGrabPoints = new List<XRSimpleInteractable>();

    private void Start()
    {
        foreach (var VARIABLE in secondHandGrabPoints)
        {
            VARIABLE.selectEntered.AddListener(OnSecondHandGrab);
            VARIABLE.selectExited.AddListener(OnSecondHandRelease);
        }
    }

    public void OnSecondHandGrab(SelectEnterEventArgs args)
    {
        Debug.Log("2nd hand grab");
        GameManager.onDebugMessage.Invoke("2nd hand release");
    }

    private void OnSecondHandRelease(SelectExitEventArgs args)
    {
        Debug.Log("2nd hand release");
        GameManager.onDebugMessage.Invoke("2nd hand grab");
    }


    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("First grab enter");
        base.OnSelectEntered(args);
        GameManager.onDebugMessage.Invoke("First grab enter");
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        Debug.Log("First grab exit");
        base.OnSelectExited(args);
        GameManager.onDebugMessage.Invoke("First grab exit");
    }


    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        bool isAlreadyGrabbed = isSelected && !interactor.Equals(firstInteractorSelecting);
        return base.IsSelectableBy(interactor) && !isAlreadyGrabbed;
    }
}