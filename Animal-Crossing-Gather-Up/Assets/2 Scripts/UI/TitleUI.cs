using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private bool isgameStart;
    [SerializeField] private GameObject chooseFirstUI;
    [SerializeField] private GameObject chooseSecondUI;

    private void Start()
    {
        isgameStart = true;
        chooseFirstUI.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ChangeChosenOption(true);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ChangeChosenOption(false);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            OptionChosen();
        }
    }

    private void OptionChosen()
    {
        if (isgameStart)
            SceneManager.LoadScene("GameScene");
        else
            QuitGame();
    }

    private void ChangeChosenOption(bool param)
    {
        isgameStart = param;
        chooseFirstUI.SetActive(isgameStart);
        chooseSecondUI.SetActive(!isgameStart);
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
