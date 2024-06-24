using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;
using UnityEngine.SocialPlatforms.Impl;

public class UiManager : MonoBehaviourPun
{
    public GameObject[] player;
    public Slider[] slider;
    public GameObject player2Ui;
    public GameObject gameSetUi;
    public float GameTime = 600;
    public Text GmaeText;
    public static UiManager instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        StartCoroutine(FindPlayer());
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // 로컬 오브젝트라면 쓰기 부분이 실행됨
        if (stream.IsWriting)
        {
            stream.SendNext(GameTime);
        }
        else
        {
            // 리모트 오브젝트라면 읽기 부분이 실행됨         
            GameTime = (float)stream.ReceiveNext();
            // 동기화하여 받은 점수를 UI로 표시
            GmaeText.text = GameTime.ToString();
        }
    }

    IEnumerator FindPlayer()
    {
        while (true)
        {
            if (player.Length >= 2)
            {
                player2Ui.gameObject.SetActive(true);
                yield break;
            }

            Debug.Log("플레이어 찾아");
            player = GameObject.FindGameObjectsWithTag("Player");
            yield return null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (player.Length >= 2)
        {
            GameTime -= Time.deltaTime;
            GmaeText.text = GameTime.ToString();
            if (GameTime <= 0)
            {
                Time.timeScale = 0f;
                gameSetUi.SetActive(true);
            }
        }
    }
    [PunRPC]
    public void checkHp()
    {
        for (int i = 0; i < 2; i++)
        {
            slider[i].value = player[i].GetComponent<PlayerController_Platform>().hp;
        }
    }
    public void GameSet()
    {
        GameManager.instance.LoadTitleScene();
    }
}
