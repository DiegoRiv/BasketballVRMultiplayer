using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
public class Timer : MonoBehaviourPunCallbacks
{
    public float time=300f;

    public TextMeshProUGUI timerText; 

    private PhotonView pVIew;

    void Start()
    {
        pVIew=GetComponent<PhotonView>();
    }

    IEnumerator StartTimer(float time)
    {
        yield return new WaitForSeconds(1f);
        time-=1;
        if(time <=0)
        {
            timerText.text="00:00";
            StopCoroutine(StartTimer(time));
            pVIew.RPC("EndMatch",RpcTarget.AllViaServer);

        }
        else
        {
            pVIew.RPC("UIChangeTimer",RpcTarget.All,time);
            StartCoroutine(StartTimer(time));
        }
    }

    [PunRPC]
    void UIChangeTimer(float Timer)
    {
        float minutes = Mathf.FloorToInt(Timer / 60);
        float seconds = Mathf.FloorToInt(Timer % 60);
        timerText.text=string.Format("{0:00}:{1:00}",minutes,seconds);

    }
    public override void OnJoinedRoom()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount==PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            StartCoroutine(StartTimer(time));
        }
        timerText.text="Waiting for "+ (PhotonNetwork.CurrentRoom.MaxPlayers-PhotonNetwork.CurrentRoom.PlayerCount);
    }
    
    [PunRPC]

    void EndMatch()
    {
        Invoke("ToLobby",5);
    }

    void ToLobby()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel(0);
    }
}
