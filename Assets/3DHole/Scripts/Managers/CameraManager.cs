using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [Header("Elements")]
    public CinemachineVirtualCamera playerCamera;

    [Header("Settings")]
    [SerializeField] private float minDistance;
    [SerializeField] private float distanceMultiplier;

    public Vector3 originalFollowOffset;

    private void Start()
    {
        originalFollowOffset = GetFollowOffset();
        PlayerSize.onIncrease += PlayerSizeIncreased;
    }

    private void OnDestroy()
    {
        PlayerSize.onIncrease -= PlayerSizeIncreased;
    }

    private void PlayerSizeIncreased(float playerSize)
    {
        float distance = minDistance + (playerSize - 1) * distanceMultiplier;
        Vector3 targetCameraOffset = new Vector3(0, distance * 1.5f, -distance);

        LeanTween.value(gameObject, GetFollowOffset(), targetCameraOffset, .5f * Time.deltaTime * 60)
            .setOnUpdate((Vector3 offset) => playerCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = offset);
    }

    private Vector3 GetFollowOffset()
    {
        return playerCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
    }

    // Reset the camera's m_FollowOffset value when leaving play mode
    /*static CameraManager()
    {
        UnityEditor.EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            CameraManager cameraManager = FindObjectOfType<CameraManager>();
            if (cameraManager != null)
            {
                cameraManager.playerCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = cameraManager.originalFollowOffset;
            }
        }
    }*/


}
