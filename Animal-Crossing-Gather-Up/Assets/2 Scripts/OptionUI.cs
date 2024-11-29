using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionUI : MonoBehaviour
{
    public GameObject optionPanel; //옵션패널
    public TextMeshProUGUI[] optionTexts; //옵션 텍스트 
    public GameObject cursor; //커서 이미지
    public int currentIndex; //커서가 가리키고 있는 현재 인덱스optionText -> 커서용
    public string currentOption; //현재 옵션
    private int optionSize; //옵션 개수

    public void SetOptions(string[] options)
    {
        optionSize = options.Length;
        for (int i = 0; i < options.Length; i++)
        {
            optionTexts[i].text = options[i];
            optionTexts[i].gameObject.SetActive(true);
        }
    }

    public void PanelActive(bool isActive)
    {
        //option panel 활성화
        optionPanel.SetActive(isActive);
    }

    public void CursorMove()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentIndex--;
            if (currentIndex <= 0)
            {
                currentIndex = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            currentIndex++;
            if (currentIndex <= optionSize)
            {
                currentIndex = optionSize;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectOption();
        }
    }

    public string SelectOption()
    {
        currentOption = optionTexts[currentIndex].text;
        return currentOption;
    }
}