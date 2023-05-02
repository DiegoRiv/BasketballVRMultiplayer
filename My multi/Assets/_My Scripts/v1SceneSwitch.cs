using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class v1SceneSwitch : MonoBehaviour
{
    void OnCollisionEnter(Collision c)
    {
        if (c.transform.tag.Equals("gameType"))
        {
            SceneManager.LoadScene("v1Scene");
        }
    }
}
