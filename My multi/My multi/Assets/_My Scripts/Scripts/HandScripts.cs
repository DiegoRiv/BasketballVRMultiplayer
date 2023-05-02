using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScripts : MonoBehaviour
{
    public Collider[] handColliders;
    public bool control;
    public PlayerMovementCtrl PMC;
    void Start()
    {
       handColliders= GetComponentsInChildren<Collider>();
        PMC= GameObject.FindGameObjectWithTag("MainPlayer").GetComponent<PlayerMovementCtrl>();
    }

    // Update is called once per frame
    public void EnableColliders()
    {
        foreach (var item in handColliders)
        {
            item.enabled=true;
        }
    }

    public void DisabledColliders()
    {
        control=true;
        foreach (var item in handColliders)
        {
            item.enabled=false;
        }
        PMC.state=control;
    }

    public void EnabledDelay()
    {
        control=false;
        Invoke("EnableColliders",0.5f);
        PMC.state=control;
    }
}
