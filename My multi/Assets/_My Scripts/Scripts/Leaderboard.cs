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
    public int maxTeamSize; 
    public int maxPlayers;
    private GameObject Team1Prefab;
    private GameObject Team2Prefab;
    public Transform[] team1Spawns;
    public Transform[] team2Spawns;
    public NetworkMan2 network;
    public Material hands;
    
    public override void OnJoinedRoom()
    {
        int build = SceneManager.GetActiveScene().buildIndex;
        base.OnJoinedRoom();
        GameObject player= GameObject.FindGameObjectWithTag("MainPlayer");
        
        if( PhotonNetwork.CurrentRoom.PlayerCount <maxPlayers+1 && build !=0)
        {
       
            PhotonNetwork.NickName = "Player "+PhotonNetwork.PlayerList.Length+"."+Random.Range(0,500);
            int num= Random.Range(0,2);
            network= GameObject.FindGameObjectWithTag("Manager").GetComponent<NetworkMan2>();

            if(PhotonNetwork.CurrentRoom.PlayerCount%2==0)
            {
                team1Players.Add(PhotonNetwork.LocalPlayer.NickName);
                Team1Prefab= PhotonNetwork.Instantiate("Team1 Prefab", player.transform.position,player.transform.rotation);   
                network.playerPrefab= Team1Prefab;
                hands.color= new Color(0.3490196f,0f,0f);
            }
            else if(PhotonNetwork.CurrentRoom.PlayerCount%2!=0)
            {
                team2Players.Add(PhotonNetwork.LocalPlayer.NickName);
                Team1Prefab= PhotonNetwork.Instantiate("Team2 Prefab", player.transform.position,player.transform.rotation);
                network.playerPrefab=Team2Prefab;
                hands.color= new Color(0.1372549f, 0.4980392f, 0.09019608f);
            
            }
            photonView.RPC("SyncPlayers",RpcTarget.AllBuffered,team1Players.ToArray(),team2Players.ToArray());
            if( PhotonNetwork.CurrentRoom.PlayerCount == maxPlayers)
            {
                GameObject noise = GameObject.FindGameObjectWithTag("Start");
                GameObject ball = GameObject.FindGameObjectWithTag("Ball");
                GameObject timer= GameObject.FindGameObjectWithTag("Timer");
                timer.BroadcastMessage("StartGame",SendMessageOptions.DontRequireReceiver);
                noise.BroadcastMessage("Relocate",SendMessageOptions.DontRequireReceiver);
                ball.BroadcastMessage("StartMatch",SendMessageOptions.DontRequireReceiver);
            }
            if(PhotonNetwork.PlayerList.Length > maxPlayers)
            {
                PhotonNetwork.LoadLevel(0);
            } 
        }
          
    }
    public void OnPlayerConnected(NetworkPlayer player) 
    {
        if(SceneManager.GetActiveScene().buildIndex!=0)
        {
            if( PhotonNetwork.CurrentRoom.PlayerCount == maxPlayers)
            {
                GameObject noise = GameObject.FindGameObjectWithTag("Start");
                GameObject ball = GameObject.FindGameObjectWithTag("Ball");
                GameObject timer= GameObject.FindGameObjectWithTag("Timer");
                timer.BroadcastMessage("StartGame",SendMessageOptions.DontRequireReceiver);
                noise.BroadcastMessage("Relocate",SendMessageOptions.DontRequireReceiver);
                ball.BroadcastMessage("StartMatch",SendMessageOptions.DontRequireReceiver);
            }
            if(PhotonNetwork.PlayerList.Length > maxPlayers)
            {
                PhotonNetwork.LoadLevel(0);
            }   
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


