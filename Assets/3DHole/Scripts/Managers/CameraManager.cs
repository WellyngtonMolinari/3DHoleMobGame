using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private CinemachineVirtualCamera playerCamera;


    [Header(" Settings ")]
    [SerializeField] private float minDistance;
    [SerializeField] private float distanceMultiplier;

    private void Start()
    {
        PlayerSize.onIncrease += PlayerSizeIncreased;
    }

    private void OnDestroy()
    {
        PlayerSize.onIncrease += PlayerSizeIncreased;
    }

    private void PlayerSizeIncreased(float playerSize)
    {
        float distance = minDistance + (playerSize - 1) * distanceMultiplier;

        playerCamera.GetCinemachineComponent<CinemachineTransposer>().
            m_FollowOffset = new Vector3(0, distance * 1.5f, -distance);
    }
}
