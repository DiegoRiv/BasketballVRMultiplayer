using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameRoomDescription
{
    public string RoomName;
    public int maxNumPlayers;
    public int sceneIndex;
}
public class NetworkManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    //public Material hands;
    //---------------------------------
    public List<GameRoomDescription> roomType;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        PhotonNetwork.ConnectUsingSettings();
        print("Trying to connect to the server");
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("Connected to the server");
        PhotonNetwork.JoinLobby();
        
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("Joined the Lobby");
    }


    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("Joined room");
        Debug.Log(PhotonNetwork.PlayerList);
        if( PhotonNetwork.CurrentRoom.PlayerCount < PhotonNetwork.CurrentRoom.MaxPlayers+1)
        {


            if(PhotonNetwork.CurrentRoom.PlayerCount%2==0)
            {
                //team1Players.Add(PhotonNetwork.LocalPlayer.NickName);
                playerPrefab= PhotonNetwork.Instantiate("Team1 Prefab", transform.position,transform.rotation);   
                //hands.color= new Color(0.3490196f,0f,0f);
            }
            else if(PhotonNetwork.CurrentRoom.PlayerCount%2!=0)
            {
                //team2Players.Add(PhotonNetwork.LocalPlayer.NickName);
                playerPrefab= PhotonNetwork.Instantiate("Team2 Prefab", transform.position,transform.rotation);
                //hands.color= new Color(0.1372549f, 0.4980392f, 0.09019608f);
            
            }
            //photonView.RPC("SyncPlayers",RpcTarget.AllBuffered,team1Players.ToArray(),team2Players.ToArray());
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

    public void SetupRoom(int roomIndex)
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        print("Joining room #" + roomIndex);
        PhotonNetwork.LoadLevel(roomType[roomIndex].sceneIndex);

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = (byte)roomType[roomIndex].maxNumPlayers;
        options.IsOpen = true;
        options.IsVisible = true;
        PhotonNetwork.JoinOrCreateRoom(roomType[roomIndex].RoomName, options, TypedLobby.Default);
    }
}
