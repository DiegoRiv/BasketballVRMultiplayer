using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class StartGameCue : MonoBehaviourPunCallbacks
{
    public AudioSource whistle;
    private PhotonView pVIew;

    void Start() 
    {
        pVIew = GetComponent<PhotonView>();
        whistle.GetComponent<AudioSource>();
    }
    [PunRPC]
    public void StartGame()
    {
        whistle.Play();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        if(PhotonNetwork.CurrentRoom.PlayerCount==PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            pVIew.RPC("StartGame",RpcTarget.All);
        }
    }
}
