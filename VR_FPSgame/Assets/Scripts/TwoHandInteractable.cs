using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class TwoHandInteractable : XRGrabInteractable
{
    public List<XRSimpleInteractable> secondHandGrabPoints = new List<XRSimpleInteractable>();
    private IXRSelectInteractor firstInteractor;
    private IXRSelectInteractor secondInteractor;
    private Quaternion attachInitialLocalRotation;
    

    public enum TwoHandRotationType
    {
        None,
        First,
        Second
    };

    public TwoHandRotationType RotationType;

    private void Start()
    {
        foreach (var point in secondHandGrabPoints)
        {
            point.selectEntered.AddListener(OnSecondHandGrab);
            point.selectExited.AddListener(OnSecondHandRelease);
        }
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (firstInteractor != null && secondInteractor != null)
        {
            firstInteractor.transform.rotation = GetTwoHandRotation();
        }

        base.ProcessInteractable(updatePhase);
    }

    private Quaternion GetTwoHandRotation()
    {
        Quaternion targetRotation;

        if (RotationType == TwoHandRotationType.None)
        {
            targetRotation = Quaternion.LookRotation(
                secondInteractor.transform.position - firstInteractor.transform.position);
        }
        else if (RotationType == TwoHandRotationType.First)
        {
            targetRotation = Quaternion.LookRotation(
                secondInteractor.transform.position - firstInteractor.transform.position, firstInteractor.transform.up);
        }
        else
        {
            targetRotation = Quaternion.LookRotation(
                secondInteractor.transform.position - firstInteractor.transform.position,
                secondInteractor.transform.up);
        }

        return targetRotation;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("First grab enter");
        firstInteractor = args.interactorObject;
        base.OnSelectEntered(args);
        attachInitialLocalRotation = firstInteractor.transform.localRotation;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        Debug.Log("First grab exit");
        base.OnSelectExited(args);
        firstInteractor.transform.localRotation = attachInitialLocalRotation;
        firstInteractor = null;
    }

    private void OnSecondHandGrab(SelectEnterEventArgs args)
    {
        Debug.Log("2nd hand grab");
        secondInteractor = args.interactorObject;
    }

    private void OnSecondHandRelease(SelectExitEventArgs args)
    {
        Debug.Log("2nd hand release");
        secondInteractor = null;
    }


    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        bool isAlreadyGrabbed = isSelected && !interactor.Equals(firstInteractorSelecting);
        return base.IsSelectableBy(interactor) && !isAlreadyGrabbed;
    }
}