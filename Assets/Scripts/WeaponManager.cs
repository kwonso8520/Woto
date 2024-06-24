using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;

public class WeaponManager : MonoBehaviourPunCallbacks
{
    public void SelectWeapon()
    {
        GameObject weapon = EventSystem.current.currentSelectedGameObject;

        GameManager.instance.myWeapon = (int)weapon.GetComponent<WeaponButtonValue>().weaponType;
        // ·ë Á¢¼Ó ½ÇÇà
        PhotonNetwork.JoinRandomRoom();
    }
}
