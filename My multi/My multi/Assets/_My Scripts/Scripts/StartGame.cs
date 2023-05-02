using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class StartGame : MonoBehaviour
{
   private PhotonView pView;
   public AudioSource StartNoise;

   void Start()
   {
        pView=GetComponent<PhotonView>();
        
   }
   void Relocate()
   {
       StartNoise.Play();
   }
}
