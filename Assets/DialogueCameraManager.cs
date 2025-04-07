using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Zenject;
using Cinemachine;
using UnityEngine.SceneManagement;

public class DialogueCameraManager : MonoBehaviour
{

    public CinemachineVirtualCamera mainCinemachine;
    public CinemachineBrain cinemachineBrain;

    [YarnCommand("BackToMain")]
    public void BackToMain()
    {
        Debug.Log("backToMain Camera");
        CinemachineVirtualCamera currentCamera = cinemachineBrain.ActiveVirtualCamera as CinemachineVirtualCamera;
        if (currentCamera != null)
        {
            currentCamera.Priority = 0;
        }
        mainCinemachine.Priority = 10;
        DialogueConcurrencyManager.TryExit();
    }

    [YarnCommand("switchScene")]
    public static void SwitchScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }
}
