using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class SunucuYonetim : MonoBehaviourPunCallbacks
{

    [Header("Input Text")]
    public TMP_InputField userNameInput;
    public TMP_InputField roomNameInput;
    public GameObject loginPanel;
    public TextMeshProUGUI[] playersName;

    public List<Transform> playerSpawnPos;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Bağlantı koptu");
    }
    public override void OnConnectedToMaster()
    {

        Debug.Log("Server'e Bağlanıldı.");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {

        Debug.Log("Lobiye bağlanıldı.");

    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Odaya Girildi.");
        GameObject player = PhotonNetwork.Instantiate("Characters/Soldier Robot_1", playerSpawnPos[PhotonNetwork.PlayerList.Length - 1].position, Quaternion.identity);
        CameraControll.instance.targetPlayer = player.transform;
        CameraControll.instance.SetOffset();
    }

    //private void Update()
    //{
    //    if (PhotonNetwork.PlayerList.Length >= 2)
    //    {
    //        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
    //        {
    //            Player p = PhotonNetwork.PlayerList[i];
    //            playersName[i].text = p.NickName;
    //        }
    //    }
    //}
    public override void OnLeftLobby()
    {
        Debug.Log("Lobiden Çıkıldı.");

    }
    public override void OnLeftRoom()
    {
        Debug.Log("Odadan Çıkıldı.");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Odaya girilemedi." + message + " - " + returnCode);

    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Random Odaya girilemedi." + message + " - " + returnCode);

    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Oda oluşturulamadı." + message + " - " + returnCode);
    }
    public void CreateRoom()
    {
         PhotonNetwork.NickName = userNameInput.text;
      //  PhotonNetwork.NickName = (PhotonNetwork.PlayerList.Length - 1).ToString();
        PhotonNetwork.JoinOrCreateRoom(roomNameInput.text, new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);
        loginPanel.SetActive(false);

    }
    public void JoinRoom()
    {
        PhotonNetwork.NickName = userNameInput.text;
        //PhotonNetwork.NickName = (PhotonNetwork.PlayerList.Length - 1).ToString();
        PhotonNetwork.JoinRandomRoom();
        loginPanel.SetActive(false);

    }
}
