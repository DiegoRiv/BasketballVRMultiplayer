using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PunishScript : MonoBehaviour
{
    public XRGrabInteractableNetwork XRIN;

    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex!=0)
        {
            XRIN = GameObject.FindGameObjectWithTag("Ball").GetComponent<XRGrabInteractableNetwork>();
        }
    }
    public void StartPunish()
    {
        Invoke("Punish",5);
    }
    private void Punish()
    {
        StartCoroutine(ReturnDirect());
    }
    public void StopPunishment()
    {
        StopCoroutine(ReturnDirect());
    }
    IEnumerator ReturnDirect()
    {
        XRIN.enabled=false;
        yield return new WaitForSeconds(3f);
        XRIN.enabled=true;

    }
}
