using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using Unity.XR.CoreUtils;

public class NetworkPlayer3 : MonoBehaviourPunCallbacks
{
    private PhotonView pView;
    private XROrigin playerRig;
    public Transform VRHelmet, lHandCtrl, rHandCtrl;    // Player controller components
    public Transform head, body, lHand, rHand;  // Avatar components
    private float triggerVal, gripVal;
    public Animator leftAnimator, rightAnimator;



    void Start()
    {
        pView = GetComponent<PhotonView>();
        XROrigin playerRig = FindObjectOfType<XROrigin>();
        VRHelmet = playerRig.transform.Find("Camera Offset/Main Camera");
        lHandCtrl = playerRig.transform.Find("Camera Offset/LeftHand Controller");
        rHandCtrl = playerRig.transform.Find("Camera Offset/RightHand Controller");

        if (pView.IsMine)
        {
            head.gameObject.SetActive(false);
            body.gameObject.SetActive(false);
            //lHand.gameObject.SetActive(false);
            //rHand.gameObject.SetActive(false);
        }
        
    }
    void SetPositionAndRotation(Transform avatarPart, Transform device)
    {
        avatarPart.position = device.position;
        avatarPart.rotation = device.rotation;
    }

    void SetBodyPositionAndRotation(Transform avatarPart, Transform device)
    {
        avatarPart.position = new Vector3(device.position.x, device.position.y-0.4f, device.position.z);
        avatarPart.rotation = Quaternion.Euler(0, device.rotation.y,0);
    }


    void Update()
    {
        if (pView.IsMine)
        {
            SetPositionAndRotation(head, VRHelmet);
            SetBodyPositionAndRotation(body, VRHelmet);
            SetPositionAndRotation(lHand, lHandCtrl);
            SetPositionAndRotation(rHand, rHandCtrl);
            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.LeftHand), leftAnimator);
            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.RightHand), rightAnimator);

        }
    }

    void UpdateHandAnimation(InputDevice handCtrlDevice, Animator handAnim)
    {
        if (handCtrlDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerVal))
            handAnim.SetFloat("Trigger", triggerVal);
        else
            handAnim.SetFloat("Trigger", 0);

        if (handCtrlDevice.TryGetFeatureValue(CommonUsages.grip, out float gripVal))
            handAnim.SetFloat("Grip", gripVal);
        else
            handAnim.SetFloat("Grip", 0);
    }
}
