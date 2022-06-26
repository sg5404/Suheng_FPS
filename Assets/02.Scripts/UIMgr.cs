using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIMgr : MonoBehaviour
{
    // 버튼연결
    public Button startButton;

    private UnityAction action;

    void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        // StartButton 이벤트연결
        action = () => OnStartClick();
        startButton.onClick.AddListener(action);
        Screen.SetResolution(2560, 1440, true);
    }

    void OnStartClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    void OnButtonClick(string str)
    {
        Debug.Log($"Click Button : { str}");
    }
}
