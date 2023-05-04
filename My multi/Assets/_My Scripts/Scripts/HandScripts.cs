using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandScripts : MonoBehaviour
{
    public Collider[] handColliders;
    public XRGrabInteractableNetwork XRIN;
    private bool holdBall;
    void Start()
    {

       handColliders= GetComponentsInChildren<Collider>();
       if(SceneManager.GetActiveScene().buildIndex!=0)
       {
            XRIN = GameObject.FindGameObjectWithTag("Ball").GetComponent<XRGrabInteractableNetwork>();
       }
       
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
        GameObject.FindGameObjectWithTag("MainPlayer").GetComponent<FPC_Motion>().enabled=false;
        foreach (var item in handColliders)
        {
            item.enabled=false;
        }

        
        
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
        XRIN.enabled=false;
        yield return new WaitForSeconds(5f);
        XRIN.enabled=true;

    }
}
