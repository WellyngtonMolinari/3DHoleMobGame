using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using Cinemachine;

[InitializeOnLoad]
public class CameraManagerEditor
{
    static CameraManagerEditor()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            CameraManager cameraManager = GetCameraManagerInstance();
            if (cameraManager != null)
            {
                cameraManager.playerCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = cameraManager.originalFollowOffset;
            }
        }
    }

    private static CameraManager GetCameraManagerInstance()
    {
        CameraManager[] cameraManagers = GameObject.FindObjectsOfType<CameraManager>();
        if (cameraManagers.Length > 0)
        {
            return cameraManagers[0];
        }
        return null;
    }
}
