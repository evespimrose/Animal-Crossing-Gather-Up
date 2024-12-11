using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseOptionUI : MonoBehaviour
{
    private bool isResume = false;
    [SerializeField] private GameObject chooseFirstUI;
    [SerializeField] private GameObject chooseSecondUI;

    private void Start()
    {
        chooseFirstUI.SetActive(true);
    }
    private void OnEnable()
    {
        Time.timeScale = 0;
        isResume = true;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
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
        else if(Input.GetKeyDown(KeyCode.Return))
        {
            OptionChosen();
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }

    private void OptionChosen()
    {
        if(isResume)
            gameObject.SetActive(false);
        else
        {
            Time.timeScale = 1;

            SceneManager.LoadScene("TitleScene");
            gameObject.SetActive(false);
        }
    }

    private void ChangeChosenOption(bool param)
    {
        isResume = param;
        chooseFirstUI.SetActive(isResume);
        chooseSecondUI.SetActive(!isResume);
    }
}
