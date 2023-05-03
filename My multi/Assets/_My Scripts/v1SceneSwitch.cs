using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
[System.Serializable]
public class GameRoomDescription
{
    public string RoomName;
    public int maxNumPlayers;
    public int sceneIndex;
}
public class v1SceneSwitch : MonoBehaviourPunCallbacks
{
    public List<GameRoomDescription> roomsList;
    public int SceneIndex;
    private void OnTriggerEnter(Collider c) 
    {
        if (c.transform.tag.Equals("MainPlayer"))
        {
            SceneManager.LoadScene("v1Scene");
            //SetupRoom(SceneIndex);
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
