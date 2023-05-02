using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
public class NetworkManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    Scene sceneName;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        print("Trying to connect to the server");
        sceneName= SceneManager.GetActiveScene();
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
        if(sceneName.name=="Lobby")
        {
            playerPrefab = PhotonNetwork.Instantiate("Network Player", transform.position, transform.rotation);
        }
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
