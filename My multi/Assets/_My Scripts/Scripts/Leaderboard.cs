using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviourPunCallbacks
{ 
   public List<string> team1Players = new List<string>(); 
    public List<string> team2Players = new List<string>();
    public int maxTeamSize = 2; 
    public int maxPlayers =4;
    private GameObject Team1Prefab;
    private GameObject Team2Prefab;
    public Transform[] team1Spawns;
    public Transform[] team2Spawns;
    public NetworkManager network;
    public Material hands;

    public override void OnJoinedRoom()
    {
        int build = SceneManager.GetActiveScene().buildIndex;
        base.OnJoinedRoom();
        
        if(PhotonNetwork.PlayerList.Length <=maxPlayers && build !=0)
        {
       
            PhotonNetwork.NickName = "Player "+PhotonNetwork.PlayerList.Length+"."+Random.Range(0,500);
            int num= Random.Range(0,2);
            network= GameObject.FindGameObjectWithTag("Manager").GetComponent<NetworkManager>();

            if(PhotonNetwork.PlayerList.Length%2==0)
            {
                team1Players.Add(PhotonNetwork.LocalPlayer.NickName);
                Team1Prefab= PhotonNetwork.Instantiate("Team1 Prefab", team1Spawns[Random.Range(0,team1Spawns.Length)].position,team1Spawns[Random.Range(0,team1Spawns.Length)].rotation);   
                network.playerPrefab= Team1Prefab;
                hands.color= new Color(0.3490196f,0f,0f);
            }
            else if(PhotonNetwork.PlayerList.Length%2!=0)
            {
                team2Players.Add(PhotonNetwork.LocalPlayer.NickName);
                Team1Prefab= PhotonNetwork.Instantiate("Team2 Prefab", team2Spawns[Random.Range(0,team2Spawns.Length)].position,team2Spawns[Random.Range(0,team2Spawns.Length)].rotation);
                network.playerPrefab=Team2Prefab;
                hands.color= new Color(0.1372549f, 0.4980392f, 0.09019608f);
            
            }
            photonView.RPC("SyncPlayers",RpcTarget.AllBuffered,team1Players.ToArray(),team2Players.ToArray());
            if(PhotonNetwork.PlayerList.Length == maxPlayers)
            {
                GameObject noise = GameObject.FindGameObjectWithTag("Start");
                GameObject ball = GameObject.FindGameObjectWithTag("Ball");
                GameObject timer= GameObject.FindGameObjectWithTag("Timer");
                timer.BroadcastMessage("StartGame",SendMessageOptions.DontRequireReceiver);
                noise.BroadcastMessage("Relocate",SendMessageOptions.DontRequireReceiver);
                ball.BroadcastMessage("StartMatch",SendMessageOptions.DontRequireReceiver);
            }

            }
            if(PhotonNetwork.PlayerList.Length > maxPlayers)
            {
                PhotonNetwork.LoadLevel(0);
            }
    }


    [PunRPC]
    void SyncPlayers(string [] team1, string [] team2)
    {
        team1Players= new List<string>(team1);
        team1Players = new List<string>(team2);
    }

    IEnumerator EndMatch()
    {
        yield return new WaitForSeconds(5);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }
}


