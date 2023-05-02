using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class v2SceneSwitch : MonoBehaviour
{
    void OnTriggerEnter(Collider c)
    {
        if (c.transform.tag.Equals("Player"))
        {
            print("selected 2v2");
            SceneManager.LoadScene("v2Scene");
        }
    }
}
