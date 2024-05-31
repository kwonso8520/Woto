using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCamera_Platform : MonoBehaviourPun
{
    [SerializeField] [Tooltip("positions")]
    Transform[] playerTransforms;

    public float yOffset = 2.0f;
    public float minDistance = 11.0f;

    private float xMin, xMax, yMin, yMax;
    private void Start()
    {
        StartCoroutine(FindPlayer());
    }
    private void Update()
    {
        
    }
    IEnumerator FindPlayer()
    {
        while (true)
        {
            if (playerTransforms.Length >= 2) yield break;

            Debug.Log("플레이어 찾아");
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            playerTransforms = new Transform[players.Length];
            for (int i = 0; i < players.Length; i++)
            {
                playerTransforms[i] = players[i].transform;
            }
            yield return null;
        }
    }
    void LateUpdate()
    {
        /*if (!photonView.IsMine)
        {
            return;
        }*/
        if (playerTransforms.Length < 2)
        {
            Debug.Log("Have not found a player, make sure the player tag is on");
            return;
        }

            xMin = xMax = playerTransforms[0].position.x;
            yMin = yMax = playerTransforms[0].position.y;

            for (int i = 1; i < playerTransforms.Length; i++)
            {
                if (playerTransforms[i].position.x < xMin)
                    xMin = playerTransforms[i].position.x;

                if (playerTransforms[i].position.x > xMax)
                    xMax = playerTransforms[i].position.x;

                if (playerTransforms[i].position.y < yMin)
                    yMin = playerTransforms[i].position.y;

                if (playerTransforms[i].position.y > yMax)
                    yMax = playerTransforms[i].position.y;
            }
            float xMiddle = (xMax + xMin) / 2;
            float yMiddle = (yMax + yMin) / 2;
            float distance = xMax - xMin;
            if (distance < minDistance)
                distance = minDistance;

            if (Mathf.Abs(playerTransforms[0].position.x - playerTransforms[1].position.x) < 15)
                transform.position = new Vector3(xMiddle, yMiddle + yOffset, -distance);
        }
}
