using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorManager : MonoBehaviourPunCallbacks
{
    private Vector3 playerPos = new Vector3(4, 0, 0);
    public GameObject[] playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab[GameManager.instance.myWeapon].name, playerPos, Quaternion.identity);
    }
}
