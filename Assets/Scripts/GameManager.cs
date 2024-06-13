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

    private string gameVersion = "1"; // ���� ����

    public Button joinButton; // �� ���� ��ư

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
        // ���ӿ� �ʿ��� ����(���� ����) ����
        PhotonNetwork.GameVersion = gameVersion;
        // ������ ������ ������ ������ ���� ���� �õ�
        PhotonNetwork.ConnectUsingSettings();

        // �� ���� ��ư�� ��� ��Ȱ��ȭ
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
        // �� ���� ��ư�� Ȱ��ȭ
        joinButton.interactable = true;
        disconnectionPanel.SetActive(false);
    }

    // ������ ���� ���� ���н� �ڵ� ����
    public override void OnDisconnected(DisconnectCause cause)
    {
        // �� ���� ��ư�� ��Ȱ��ȭ
        joinButton.interactable = false;
        // ���� ���� ǥ��
        disconnectionPanel.SetActive(true);
        if (pushRetryBtn)
        {
            // ������ �������� ������ �õ�
            PhotonNetwork.ConnectUsingSettings();
            pushRetryBtn = false;
        }
    }
    public void RetryBtn()
    {
        pushRetryBtn = true;
        // ������ �������� ������ �õ�
        Debug.Log("4");
        PhotonNetwork.ConnectUsingSettings();
    }
    // �� ���� �õ�
    public void Connect()
    {
        // �ߺ� ���� �õ��� ���� ����, ���� ��ư ��� ��Ȱ��ȭ
        joinButton.interactable = false;
        Debug.Log("1");
        // ������ ������ �������̶��
        if (PhotonNetwork.IsConnected)
        {
            // �� ���� ����
            PhotonNetwork.JoinRandomRoom();
            Debug.Log("2");
        }
        else
        {
            // ������ ������ �������� �ƴ϶��, ������ ������ ���� �õ�
            disconnectionPanel.SetActive(true);
            Debug.Log("3");
        }
    }

    // (�� ���� ����)���� �� ������ ������ ��� �ڵ� ����
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // �ִ� 2���� ���� ������ ����� ����
        Debug.Log("CreateRoom");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2});
    }

    // �뿡 ���� �Ϸ�� ��� �ڵ� ����
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
