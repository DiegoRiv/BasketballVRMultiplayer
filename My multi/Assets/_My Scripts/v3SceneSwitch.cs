using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class v3SceneSwitch : MonoBehaviour
{
     private void OnTriggerEnter(Collider c) 
    {
        if (c.transform.tag.Equals("MainPlayer"))
        {
            SceneManager.LoadScene("v3Scene");
        }
    }
}
