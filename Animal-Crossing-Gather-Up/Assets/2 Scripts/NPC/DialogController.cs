using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
//using Unity.Android.Types;



public class DialogController : MonoBehaviour
{
	protected NPCPanelUI uiManager;
	protected OptionUI optionui;
	protected Coroutine currentCoroutine;

	protected NPCDialogData dialogData;
	protected string[] activeDialogTexts;
	protected int activeDialogIndex;

	protected virtual void Start()
	{
		uiManager = FindObjectOfType<NPCPanelUI>();
		optionui = FindObjectOfType<OptionUI>();
		uiManager.dialogPanel.SetActive(false);
		optionui.optionPanel.SetActive(false);
		uiManager.enterPanel.SetActive(false);

	}

	public void DialogStart(string[] setDialogTexts, int dialogIndexCount)
	{
		dialogData.isChooseActive = false;
		activeDialogTexts = setDialogTexts;
		activeDialogIndex = dialogIndexCount;
		FirstTextStart(setDialogTexts, dialogIndexCount);
	}

	public void EndDialog()
	{
		dialogData.isChooseActive = false;
		for (int i = 0; i < dialogData.dialogIndex.Length; i++)
		{
			dialogData.dialogIndex[i] = 0;
		}
		activeDialogTexts = null;
		optionui.optionPanel.SetActive(false);
		uiManager.dialogPanel.SetActive(false);
	}

	protected virtual void Update()
	{
		uiManager.enterPanel.SetActive(dialogData.isEnterActive);

		if (uiManager.dialogPanel.activeSelf && activeDialogTexts != null)
		{
			EnterDialog(activeDialogTexts, activeDialogIndex);
		}

		//플레이어와 상호작용 이런 식으로 작성 예정
		//일정 거리 안에 플레이어가 들어왔을 때 r키를 누르면 대화창 활성화 
		//if(Vector3.Distance(player.position, npc.position) < 5f))
		//   {
		//      if(GetKeyDown(KeyCode.R))
		//      {
		//          DialogStart();
		//      }
		//   }

	}

	public void FirstTextStart(string[] SetdialogTexts, int dialogIndexCount)
	{
		if (currentCoroutine == null)
		{
			string firstText = SetdialogTexts[dialogData.dialogIndex[dialogIndexCount]];
			currentCoroutine = StartCoroutine(TypingDialog(firstText));
			dialogData.dialogIndex[dialogIndexCount]++;
		}
	}

	public void EnterDialog(string[] setDialogTexts, int dialogIndexCount)
	{
		if (dialogData.dialogIndex[dialogIndexCount] >= setDialogTexts.Length)
		{
			dialogData.isChooseActive = true;
			activeDialogTexts = null;
			return;
		}

		if (dialogData.dialogIndex[dialogIndexCount] < setDialogTexts.Length && currentCoroutine == null)
		{
			if (Input.GetKeyDown(KeyCode.Space) && currentCoroutine == null)
			{
				string text = setDialogTexts[dialogData.dialogIndex[dialogIndexCount]];
				currentCoroutine = StartCoroutine(TypingDialog(text));
				dialogData.dialogIndex[dialogIndexCount]++;
			}
		}
	}

	private IEnumerator TypingDialog(string text)
	{
		dialogData.isEnterActive = false;
		uiManager.dialogText.text = "";
		foreach (char letter in text.ToCharArray())
		{
			if (Input.GetKeyDown(KeyCode.T))
			{
				uiManager.dialogText.text = text;
				break;
			}

			uiManager.dialogText.text += letter;
			yield return new WaitForSeconds(0.1f);
		}

		dialogData.isEnterActive = true;
		optionui.PanelActive(dialogData.isChooseActive);
		currentCoroutine = null;
	}

}
