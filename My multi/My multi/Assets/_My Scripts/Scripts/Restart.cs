using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;
public class Restart : MonoBehaviour
{
    public Transform Respawn;
    public TMP_Text Team1S;
    public TMP_Text Team2S;
    public TMP_Text Team1S2;
    public TMP_Text Team2S2;

    private PhotonView pView;
    public GameControl control;
    public int num;
    public AudioSource scoreClip;
    [SerializeField]private Collider Bcollider;
    [SerializeField]private Collider ChildCollider;

    void Start()
    {
        pView=GetComponent<PhotonView>();
        scoreClip=GetComponent<AudioSource>();
        Invoke("Match",3);
        control= GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>();
        Bcollider= GetComponent<Collider>();
        ChildCollider= GetComponentInChildren<BoxCollider>();
        Bcollider.enabled=false;
        ChildCollider.enabled=false;
    }
    void Match()
    {
        int team1 = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>().Team1;
        int team2 = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>().Team2;
        Team1S.text="Jaspers: "+team1.ToString();
        Team1S2.text= "Jaspers: "+team1.ToString();
        Team2S.text="Gaels: "+team2.ToString();
        Team2S2.text= "Gaels: "+team2.ToString();
    }
    private void OnCollisionEnter(Collision other) 
    {
        if(other.transform.CompareTag("Ball"))
        {
            other.gameObject.transform.position=Respawn.position;
            StartCoroutine(AudioScore());
            if(num ==0)
            {
               if(pView.IsMine)
               {
                    pView.RPC("UpdateScore",RpcTarget.All,num);   
               }
            }
            if(num==1)
            {
                if(pView.IsMine)
                {
                    pView.RPC("UpdateScore",RpcTarget.All,num);   
                }
            }
        }
         
    }
    [PunRPC]
    void UpdateScore(int team)
    {
        if(team ==0)
        {
            control.Team1+=1;
        }
        else
        {
            control.Team2+=1;
        }
    }
    void StartGame()
    {
        Bcollider.enabled=true;
        ChildCollider.enabled=true;
    }

    void Update()
    {
        Team1S.text="Jaspers: "+control.Team1.ToString();
        Team1S2.text= "Jaspers: "+control.Team1.ToString();
        Team2S.text="Gaels: "+control.Team2.ToString();
        Team2S2.text= "Gaels: "+control.Team2.ToString();
    }


    void Relocate()
    {
            GameObject ball = GameObject.FindGameObjectWithTag("Ball");
            ball.gameObject.transform.position=Respawn.position;
    }
    IEnumerator AudioScore()
    {
        scoreClip.Play();
        yield return new WaitForSeconds(3f);
        scoreClip.Stop();
    }
}
