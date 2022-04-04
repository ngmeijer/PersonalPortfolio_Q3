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
        foreach (var point in secondHandGrabPoints)
        {
            point.selectEntered.AddListener(OnSecondHandGrab);
            point.selectExited.AddListener(OnSecondHandRelease);
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("First grab enter");
        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        Debug.Log("First grab exit");
        base.OnSelectExited(args);
    }
    
    private void OnSecondHandGrab(SelectEnterEventArgs args)
    {
        Debug.Log("2nd hand grab");
    }

    private void OnSecondHandRelease(SelectExitEventArgs args)
    {
        Debug.Log("2nd hand release");
    }


    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        bool isAlreadyGrabbed = isSelected && !interactor.Equals(firstInteractorSelecting);
        return base.IsSelectableBy(interactor) && !isAlreadyGrabbed;
    }
}