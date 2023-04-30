using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameControl : MonoBehaviourPunCallbacks
{
    public static GameControl control;
    private PhotonView pView;
    public int Team1=0;
    public int Team2=0;
    public int MaxPlayers =2;


    void Awake() 
    {
        GameObject[] control = GameObject.FindGameObjectsWithTag("GameController");

        if(control.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        pView=GetComponent<PhotonView>();
    }


}
