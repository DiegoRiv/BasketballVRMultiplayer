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

    void Update()
    {
        if (Team1==7)
        {
            timerText.text = "GAELS WINS!";
            GameObject control = GameObject.FindGameObjectWithTag("GameController");
            GameObject GG = GameObject.FindGameObjectWithTag("Game Over");
            GG.BroadcastMessage("GameOver", SendMessageOptions.DontRequireReceiver);
            control.BroadcastMessage("EndMatch", SendMessageOptions.DontRequireReceiver);
        }
        if (Team2 == 7)
        {
            timerText.text = "JASPERS WIN!";
            GameObject control = GameObject.FindGameObjectWithTag("GameController");
            GameObject GG = GameObject.FindGameObjectWithTag("Game Over");
            GG.BroadcastMessage("GameOver", SendMessageOptions.DontRequireReceiver);
            control.BroadcastMessage("EndMatch", SendMessageOptions.DontRequireReceiver);
        }
        if (Team1 == 7 && Team2 == 7)
        {
            timerText.text = "It's a tie!";
            GameObject control = GameObject.FindGameObjectWithTag("GameController");
            GameObject GG = GameObject.FindGameObjectWithTag("Game Over");
            GG.BroadcastMessage("GameOver", SendMessageOptions.DontRequireReceiver);
            control.BroadcastMessage("EndMatch", SendMessageOptions.DontRequireReceiver);
        }
    }
}
