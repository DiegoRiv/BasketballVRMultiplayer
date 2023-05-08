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
    //public GameObject Timer;
    public GameObject Team1Prefab;
    public GameObject Team2Prefab;
    private Transform[] team1Spawns;
    private Transform[] team2Spawns;
    public NetworkManager network;
    private GameObject player;
    public Material hands;
    private PhotonView pView;

    void Start()
    {
        pView = GetComponent<PhotonView>();
        network= GameObject.FindGameObjectWithTag("Manager").GetComponent<NetworkManager>();
        player= GameObject.FindGameObjectWithTag("MainPlayer");
    }
    


    [PunRPC]
    void SyncPlayers(string [] team1, string [] team2)
    {
        team1Players= new List<string>(team1);
        team1Players = new List<string>(team2);
    }

}


