using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class BallMovement: MonoBehaviour
{
    AudioSource bounce;
    public Transform Restartpoint;
    private PhotonView pview;
    void Start()
    {
        bounce=GetComponent<AudioSource>();
        pview=GetComponent<PhotonView>();
    }
    private void OnCollisionEnter(Collision other) 
    {
        bounce.Play();  
    }
    private void StartMatch()
    {
        pview.RPC("StartGame",RpcTarget.All);
        GameObject[] hoops = GameObject.FindGameObjectsWithTag("Hoops");
        hoops[0].BroadcastMessage("StartGame",SendMessageOptions.DontRequireReceiver);
        hoops[1].BroadcastMessage("StartGame",SendMessageOptions.DontRequireReceiver);
    }
    [PunRPC]
    void StartGame()
    {
        gameObject.transform.position=Restartpoint.position;
    }
}