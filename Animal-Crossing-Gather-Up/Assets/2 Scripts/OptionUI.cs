using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionUI : MonoBehaviour
{
    public GameObject optionPanel; //�ɼ��г�
    public TextMeshProUGUI[] optionTexts; //�ɼ� �ؽ�Ʈ 
    public GameObject cursor; //Ŀ�� �̹���
    public GameObject underline; //���� �̹���
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
        cursor.SetActive(isActive);
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
            if (currentIndex >= optionSize)
            {
                currentIndex = optionSize - 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectOption();
        }

        SelecteedOptionPosition();
    }

    public string SelectOption()
    {
        currentOption = optionTexts[currentIndex].text;
        optionPanel.SetActive(false);

        print($"���� �ɼ�: {currentOption}");
        return currentOption;
    }


    private void SelecteedOptionPosition()
    {
        TextMeshProUGUI selectedOption = optionTexts[currentIndex];
        float optionWidth = selectedOption.preferredWidth;

        Vector3 cursorPos = selectedOption.transform.position;
        cursorPos.x += optionWidth + 60;
        cursor.transform.position = cursorPos;

        Vector3 underlinePos = selectedOption.transform.position;
        underlinePos.x += optionWidth + 20f;
        underline.transform.position = underlinePos;

        RectTransform underlineRect = underline.GetComponent<RectTransform>();
        underlineRect.sizeDelta = new Vector2(optionWidth - 20f, underlineRect.sizeDelta.y);
    }

}