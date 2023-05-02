using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class v1SceneSwitch : MonoBehaviour
{
    void OnTriggerEnter(Collider c)
    {
        if (c.transform.tag.Equals("Player"))
        {
            print("selected 1v1");
            SceneManager.LoadScene("v1Scene");
        }
    }
}
