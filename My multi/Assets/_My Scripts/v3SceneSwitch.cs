using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class v3SceneSwitch : MonoBehaviour
{
    void OnTriggerEnter(Collider c)
    {
        if (c.transform.tag.Equals("Player"))
        {
            print("selected 3v3");
            SceneManager.LoadScene("v3Scene");
        }
    }
}
