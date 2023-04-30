using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class NetworkManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        print("Trying to connect to the server");
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("Connected to the server");

        RoomOptions options = new RoomOptions();
        options.IsOpen = true;
        options.IsVisible = true;
        options.MaxPlayers = 15;
        PhotonNetwork.JoinOrCreateRoom("Room 1", options, TypedLobby.Default);
    }


    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("Joined room");
        //PhotonNetwork.NickName = "Player "+PhotonNetwork.PlayerList.Length+"."+Random.Range(0,500);
        //print(PhotonNetwork.NickName+" has entered");
        //playerPrefab = PhotonNetwork.Instantiate("Network Player", transform.position, transform.rotation);
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
