using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionUI : MonoBehaviour
{
	[Header("Option Panel")]
	public GameObject optionPanel; //옵션패널
	public TextMeshProUGUI[] optionTexts; //옵션 텍스트 

	[Header("Cursor Move")]
	public GameObject cursor; //커서 이미지
	public GameObject underline; //밑줄 이미지
	public int currentIndex; //커서/밑줄이 가리키고 있는 현재 인덱스optionText -> 커서용
	public string currentOption; //현재 옵션
	private int optionSize; //옵션 개수

	// set active false when start
	private void Start()
	{
		optionPanel.SetActive(false);
	}

	public void SetOptions(string[] options)
	{
		DisableAllOptionTexts();

		optionSize = options.Length;
		for (int i = 0; i < options.Length; i++)
		{
			optionTexts[i].text = options[i];
			optionTexts[i].gameObject.SetActive(true);
		}
	}

	private void DisableAllOptionTexts()
	{
		// all texts set active false
		foreach (TextMeshProUGUI optionText in optionTexts)
		{
			optionText.gameObject.SetActive(false);
		}
	}

	public void PanelActive(bool isActive)
	{
		//option panel 활성화
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

		print($"현재 옵션: {currentOption}");
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