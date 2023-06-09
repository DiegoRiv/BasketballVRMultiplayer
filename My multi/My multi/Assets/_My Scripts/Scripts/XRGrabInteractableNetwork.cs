using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class XRGrabInteractableNetwork : XRGrabInteractable
{
    private PhotonView pView;

    void Start()
    {
        pView = GetComponent<PhotonView>();    
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        pView.RequestOwnership();
        base.OnSelectEntered(args);

    }
    
}
