using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameOverScript : MonoBehaviour
{
    AudioSource gameOver;
    // Start is called before the first frame update
    void Start()
    {
        gameOver=GetComponent<AudioSource>();
    }
    [PunRPC]
    void EndMatch()
    {
        gameOver.Play();
    }
}
