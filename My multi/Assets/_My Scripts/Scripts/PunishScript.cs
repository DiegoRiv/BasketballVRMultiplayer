using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PunishScript : MonoBehaviour
{
    public XRGrabInteractableNetwork XRIN;

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
        CancelInvoke();
        StopCoroutine(ReturnDirect());
    }
    IEnumerator ReturnDirect()
    {
        GameObject.FindGameObjectWithTag("Ball").GetComponent<XRGrabInteractableNetwork>().enabled=false;
        yield return new WaitForSeconds(3f);
        GameObject.FindGameObjectWithTag("Ball").GetComponent<XRGrabInteractableNetwork>().enabled=true;

    }
}
