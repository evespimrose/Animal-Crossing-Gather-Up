using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionUI : MonoBehaviour
{
    public GameObject optionPanel; //�ɼ��г�
    public TextMeshProUGUI[] optionTexts; //�ɼ� �ؽ�Ʈ 
    public GameObject cursor; //Ŀ�� �̹���
    public int currentIndex; //Ŀ���� ����Ű�� �ִ� ���� �ε���optionText -> Ŀ����
    public string currentOption; //���� �ɼ�
    private int optionSize; //�ɼ� ����

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
        //option panel Ȱ��ȭ
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