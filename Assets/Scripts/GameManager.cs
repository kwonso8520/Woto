using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Realtime;

public enum WeaponType
{
    Claymore,
    Bow,
    DoubleBlades,
    DualBlades,
    Fighter,
    Hammer,
    Magic,
    Pistol,
    Rapier,
    Spear,
    Staff,
    SwordShield
};


public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;
    public GameObject[] playerPrefab;
    WeaponManager weaponManager;
    public int myWeapon;
    Scene scene;
    private Vector3 playerPos = new Vector3(4, 0, 0);

    private string gameVersion = "1"; // 게임 버전

    public Button joinButton; // 룸 접속 버튼

    public GameObject disconnectionPanel;

    public bool pushRetryBtn;

    public GameObject camera;

    public Vector3 cameraPos = new Vector3(0, 4, -10);

    private float GameTime = 600;

    public GameObject cube;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        // 접속에 필요한 정보(게임 버전) 설정
        PhotonNetwork.GameVersion = gameVersion;
        // 설정한 정보를 가지고 마스터 서버 접속 시도
        PhotonNetwork.ConnectUsingSettings();

        // 룸 접속 버튼을 잠시 비활성화
        joinButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        scene = SceneManager.GetActiveScene();
        GameTime -= Time.deltaTime;
        if(GameTime <= 0)
        {
            
        }
    }
    public void LoadGameScene()
    {
        PhotonNetwork.LoadLevel("GameScene");
    }
  
    public void LoadTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public override void OnConnectedToMaster()
    {
        // 룸 접속 버튼을 활성화
        joinButton.interactable = true;
        disconnectionPanel.SetActive(false);
    }

    // 마스터 서버 접속 실패시 자동 실행
    public override void OnDisconnected(DisconnectCause cause)
    {
        // 룸 접속 버튼을 비활성화
        joinButton.interactable = false;
        // 접속 정보 표시
        disconnectionPanel.SetActive(true);
        if (pushRetryBtn)
        {
            // 마스터 서버로의 재접속 시도
            PhotonNetwork.ConnectUsingSettings();
            pushRetryBtn = false;
        }
    }
    public void RetryBtn()
    {
        pushRetryBtn = true;
        // 마스터 서버로의 재접속 시도
        Debug.Log("4");
        PhotonNetwork.ConnectUsingSettings();
    }
    // 룸 접속 시도
    public void Connect()
    {
        // 중복 접속 시도를 막기 위해, 접속 버튼 잠시 비활성화
        joinButton.interactable = false;
        Debug.Log("1");
        // 마스터 서버에 접속중이라면
        if (PhotonNetwork.IsConnected)
        {
            // 룸 접속 실행
            PhotonNetwork.JoinRandomRoom();
            Debug.Log("2");
        }
        else
        {
            // 마스터 서버에 접속중이 아니라면, 마스터 서버에 접속 시도
            disconnectionPanel.SetActive(true);
            Debug.Log("3");
        }
    }

    // (빈 방이 없어)랜덤 룸 참가에 실패한 경우 자동 실행
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // 최대 2명을 수용 가능한 빈방을 생성
        Debug.Log("CreateRoom");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2});
    }

    // 룸에 참가 완료된 경우 자동 실행
    public override void OnJoinedRoom()
    {
        Debug.Log("JoinedRoom");
        PhotonNetwork.LoadLevel("WeaponSelectScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
