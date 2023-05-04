using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class StartGameCue : MonoBehaviour
{
    public AudioSource whistle;
    private PhotonView pVIew;

    void Start() 
    {
        pVIew = GetComponent<PhotonView>();
    }

    [PunRPC]
    public void Relocate()
    {
        whistle.Play();
    }
}
