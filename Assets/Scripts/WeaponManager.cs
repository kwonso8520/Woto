using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponManager : MonoBehaviour
{
    public void SelectWeapon()
    {
        GameObject weapon = EventSystem.current.currentSelectedGameObject;

        GameManager.instance.myWeapon = (int)weapon.GetComponent<WeaponButtonValue>().weaponType;
        GameManager.instance.LoadNextScene();
    }
}
