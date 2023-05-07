using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class v1SceneSwitch : MonoBehaviourPunCallbacks
{
    public int SceneIndex;
    public NetworkManager NManage;
    private void Start() {
        NManage=GameObject.FindGameObjectWithTag("Manager").GetComponent<NetworkManager>();    
    }

    private void OnTriggerEnter(Collider c) 
    {
        if (c.transform.tag.Equals("MainPlayer"))
        {
            NManage.BroadcastMessage("SetupRoom",SceneIndex,SendMessageOptions.DontRequireReceiver);
        }
    }
    /*
    public void SetupRoom(int roomIndex)
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        print("Joining room #" + roomIndex);
        PhotonNetwork.LoadLevel(roomsList[roomIndex].sceneIndex);

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = (byte)roomsList[roomIndex].maxNumPlayers;
        options.IsOpen = true;
        options.IsVisible = true;
        PhotonNetwork.JoinOrCreateRoom(roomsList[roomIndex].RoomName, options, TypedLobby.Default);
    }*/
}
