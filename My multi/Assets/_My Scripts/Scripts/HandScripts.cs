using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScripts : MonoBehaviour
{
    public Collider[] handColliders;
    public XRGrabInteractableNetwork ball;
    void Start()
    {

       handColliders= GetComponentsInChildren<Collider>();
       ball=GameObject.FindGameObjectWithTag("Ball").GetComponent<XRGrabInteractableNetwork>();
    }

    public void EnableColliders()
    {
        foreach (var item in handColliders)
        {
            item.enabled=true;
        }
    }

    public void DisabledColliders()
    {
        foreach (var item in handColliders)
        {
            item.enabled=false;
        }
        GameObject.FindGameObjectWithTag("MainPlayer").GetComponent<FPC_Motion>().enabled=false;
        Invoke("Punish",5);
    }

    public void EnabledDelay()
    {
        Invoke("EnableColliders",0.5f);
        GameObject.FindGameObjectWithTag("MainPlayer").GetComponent<FPC_Motion>().enabled=true;
    }
    public void Punish()
    {
        StartCoroutine(ReturnDirect());
    }
    IEnumerator ReturnDirect()
    {
        ball.enabled=false;
        yield return new WaitForSeconds(5f);
        ball.enabled=true;
    }
}
