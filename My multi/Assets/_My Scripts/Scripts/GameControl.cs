using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class GameControl : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI timerText;
    public static GameControl control;
    private PhotonView pView;
    public int Team1=0;
    public int Team2=0;



    void Awake() 
    {
        GameObject[] control = GameObject.FindGameObjectsWithTag("GameController");

        if(control.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        pView=GetComponent<PhotonView>();
    }

    [PunRPC]
    void EndMatch()
    {
        if (Team1==7)
        {
            timerText.text = "JASPERS WINS!";
        }
        else if (Team2 == 7)
        {
            timerText.text = "GAELS WIN!";
        }
        else
        {
            timerText.text="ITS A TIE";
        }
    }
}
