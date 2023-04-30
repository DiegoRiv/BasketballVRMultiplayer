using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using Unity.XR.CoreUtils;

public class FPC_Motion : MonoBehaviour
{
    private InputDevice lHandControllerDevice;
    private InputDevice rHandControllerDevice;
    public InputDeviceCharacteristics characteristicsLeft, characteristicsRight;
    private XROrigin myRig;
    private CharacterController FPC;

    private Vector2 joystick;
    private bool primBtnValue;
    public float speed = 1f;
    public float jumpSpeed = 10f;
    private float movY = 0f;
    public float gravity = 20f;

    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(characteristicsLeft, devices);
        if (devices.Count > 0) lHandControllerDevice = devices[0];

        InputDevices.GetDevicesWithCharacteristics(characteristicsRight, devices);
        if (devices.Count > 0) rHandControllerDevice = devices[0];

        myRig = GetComponent<XROrigin>();
        FPC = GetComponent<CharacterController>();
    }

    void Update()
    {
        lHandControllerDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out joystick);
        rHandControllerDevice.TryGetFeatureValue(CommonUsages.primaryButton, out primBtnValue);
    }


    private void FixedUpdate()
    {
        if (FPC.isGrounded && primBtnValue)
            movY = jumpSpeed;
        if (movY > -150)
            movY = movY - gravity * Time.fixedDeltaTime;
        else
            movY = -150;

        Quaternion headRotation = Quaternion.Euler(0, myRig.Camera.transform.eulerAngles.y, 0);
        Vector3 moveDir = headRotation * new Vector3(joystick.x, movY, joystick.y);
        FPC.Move(moveDir * Time.fixedDeltaTime * speed);
    }
}
