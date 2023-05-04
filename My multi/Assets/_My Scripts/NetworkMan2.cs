using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class NetworkMan2 : MonoBehaviourPunCallbacks
{ 
    public GameObject playerPrefab;
    public Material Hand;
    //private int buildI;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        print("Trying to connect to the server");
        //buildI = SceneManager.GetActiveScene().buildIndex;
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("Connected to the server");

        RoomOptions options = new RoomOptions();
        options.IsOpen = true;
        options.IsVisible = true;
        options.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom("v1", options, TypedLobby.Default);
        print("created 1v1 room");
    }


    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("Joined room");
        //playerPrefab = PhotonNetwork.Instantiate("Network Player", transform.position, transform.rotation);
        //PhotonNetwork.NickName = "Player "+PhotonNetwork.PlayerList.Length+"."+Random.Range(0,500);
        //print(PhotonNetwork.NickName+" has entered");
        // print("New player has entered the room");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        print("New player has entered the room");
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        print("Leaving the room now");
        PhotonNetwork.Destroy(playerPrefab);
    }
}
