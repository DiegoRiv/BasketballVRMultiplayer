using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScripts : MonoBehaviour
{
    public Collider[] handColliders;
    public Collider Left;
    public Collider Right;
    void Start()
    {

       handColliders= GetComponentsInChildren<Collider>();
       Left=GameObject.FindGameObjectWithTag("Left").GetComponent<Collider>();
       Right=GameObject.FindGameObjectWithTag("Left").GetComponent<Collider>();
       
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
        Left.enabled=false;
        Right.enabled=false;
        yield return new WaitForSeconds(5f);
        Left.enabled=true;
        Right.enabled=true;
    }
}
