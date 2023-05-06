using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
public class Timer : MonoBehaviourPunCallbacks
{
    public float time=300f;
    public int MaxPlayers;

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
            GameObject control = GameObject.FindGameObjectWithTag("GameController");
            GameObject GG = GameObject.FindGameObjectWithTag("Game Over");
            GG.BroadcastMessage("GameOver",SendMessageOptions.DontRequireReceiver);
            control.BroadcastMessage("EndMatch",SendMessageOptions.DontRequireReceiver);

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
        timerText.text=PhotonNetwork.CurrentRoom.MaxPlayers.ToString();
    }
}
