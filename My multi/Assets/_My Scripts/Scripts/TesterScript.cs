using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;

public class TesterScript : MonoBehaviourPunCallbacks
{
    public List<string> team1Players = new List<string>();
    public List<string> team2Players = new List<string>();
    public int maxTeamSize;
    public int maxPlayers;
    public GameObject Timer;
    private GameObject Team1Prefab;
    private GameObject Team2Prefab;
    public Transform[] team1Spawns;
    public Transform[] team2Spawns;
    public NetworkMan2 network;
    public Material hands;
    private PhotonView pView;
    public float time = 300f;
    public TextMeshProUGUI timerText;
   // private PhotonView pVIew;


    void Start()
    {
        pView = GetComponent<PhotonView>();
        Timer.tag = "Timer";
    }

    void StartGame()
    {
        pView.RPC("StartTimer", RpcTarget.All);
    }

    public override void OnJoinedRoom()
    {
        int build = SceneManager.GetActiveScene().buildIndex;
        base.OnJoinedRoom();
        GameObject player = GameObject.FindGameObjectWithTag("MainPlayer");

        if (PhotonNetwork.CurrentRoom.PlayerCount < maxPlayers + 1 && build != 0)
        {

            PhotonNetwork.NickName = "Player " + PhotonNetwork.PlayerList.Length + "." + Random.Range(0, 500);
            int num = Random.Range(0, 2);
            network = GameObject.FindGameObjectWithTag("Manager").GetComponent<NetworkMan2>();

            if (PhotonNetwork.CurrentRoom.PlayerCount % 2 == 0)
            {
                team1Players.Add(PhotonNetwork.LocalPlayer.NickName);
                Team1Prefab = PhotonNetwork.Instantiate("Team1 Prefab", player.transform.position, player.transform.rotation);
                network.playerPrefab = Team1Prefab;
                hands.color = new Color(0.3490196f, 0f, 0f);
            }
            else if (PhotonNetwork.CurrentRoom.PlayerCount % 2 != 0)
            {
                team2Players.Add(PhotonNetwork.LocalPlayer.NickName);
                Team1Prefab = PhotonNetwork.Instantiate("Team2 Prefab", player.transform.position, player.transform.rotation);
                network.playerPrefab = Team2Prefab;
                hands.color = new Color(0.1372549f, 0.4980392f, 0.09019608f);

            }
            photonView.RPC("SyncPlayers", RpcTarget.AllBuffered, team1Players.ToArray(), team2Players.ToArray());
            if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
            {
                print("starting1");
                StartGame();
            }
        }
                /*if (pView.IsMine)
                {
                    GameObject noise = GameObject.FindGameObjectWithTag("Start");
                    GameObject ball = GameObject.FindGameObjectWithTag("Ball");
                    GameObject timer = GameObject.FindGameObjectWithTag("Timer");
                    StartGame();
                    timer.BroadcastMessage("StartGame", SendMessageOptions.DontRequireReceiver);
                    noise.BroadcastMessage("Relocate", SendMessageOptions.DontRequireReceiver);
                    ball.BroadcastMessage("StartMatch", SendMessageOptions.DontRequireReceiver);
                }
            }
            if (PhotonNetwork.PlayerList.Length > maxPlayers)
            {
                PhotonNetwork.LoadLevel(0);
            }
        }

        //photonView.RPC("OnPlayerConnected", RpcTarget.AllBuffered, player);*/
    }

    //[PunRPC]
    void OnPlayerConnected(NetworkPlayer player)
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                if (pView.IsMine)
                {
                    GameObject noise = GameObject.FindGameObjectWithTag("Start");
                    GameObject ball = GameObject.FindGameObjectWithTag("Ball");
                    GameObject timer = GameObject.FindGameObjectWithTag("Timer");
                    StartGame();
                    print("starting2");
                    timer.BroadcastMessage("StartGame", SendMessageOptions.DontRequireReceiver);
                    noise.BroadcastMessage("Relocate", SendMessageOptions.DontRequireReceiver);
                    ball.BroadcastMessage("StartMatch", SendMessageOptions.DontRequireReceiver);
                }
            }
            if (PhotonNetwork.PlayerList.Length > maxPlayers)
            {
                PhotonNetwork.LoadLevel(0);
            }
        }
    }




    [PunRPC]
    void SyncPlayers(string[] team1, string[] team2)
    {
        team1Players = new List<string>(team1);
        team1Players = new List<string>(team2);
    }

    [PunRPC]
    IEnumerator EndMatch()
    {
        yield return new WaitForSeconds(5);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }

    [PunRPC]
    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(1f);
        time -= 1;
        if (time <= 0)
        {
            timerText.text = "00:00";
            StopCoroutine(StartTimer());
            GameObject control = GameObject.FindGameObjectWithTag("GameController");
            GameObject GG = GameObject.FindGameObjectWithTag("Game Over");
            GG.BroadcastMessage("GameOver", SendMessageOptions.DontRequireReceiver);
            control.BroadcastMessage("EndMatch", SendMessageOptions.DontRequireReceiver);

        }
        else
        {
            UIChangeTimer(time);
            StartCoroutine(StartTimer());
        }
    }

    [PunRPC]
    void UIChangeTimer(float Timer)
    {
        float minutes = Mathf.FloorToInt(Timer / 60);
        float seconds = Mathf.FloorToInt(Timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }

}
