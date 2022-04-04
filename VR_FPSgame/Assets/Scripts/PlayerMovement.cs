using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Range(1, 10)]private float speed = 1;
    public XRNode inputSource;

    private XROrigin xrOrigin;
    private Vector2 inputAxis;
    private CharacterController charController;

    private void Start()
    {
        charController = GetComponent<CharacterController>();
        xrOrigin = GetComponent<XROrigin>();
    }

    private void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

    private void FixedUpdate()
    {
        Quaternion headYaw = Quaternion.Euler(0, xrOrigin.Camera.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);
        charController.Move(direction * (Time.fixedDeltaTime * speed));
    }
}
