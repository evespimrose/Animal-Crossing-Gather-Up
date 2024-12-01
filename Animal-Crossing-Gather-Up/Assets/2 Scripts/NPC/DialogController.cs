using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;



public class DialogController : MonoBehaviour
{
    protected NPCPanelUI uiManager;
    protected OptionUI optionui;
    protected Coroutine currentCoroutine;
    protected int talkCount = 0;
    protected bool isDialogActive = false; // 대화 활성화 상태

    protected NPCDialogData dialogData; //scriptableObject 참조

    protected virtual void Start()
    {
        uiManager = FindObjectOfType<NPCPanelUI>();
        optionui = FindObjectOfType<OptionUI>();
        uiManager.dialogPanel.SetActive(false);
        optionui.optionPanel.SetActive(false);
        uiManager.enterPanel.SetActive(false);
    }


    public void DialogStart()
    {
        if (!isDialogActive) 
        {
            talkCount = 0;
            isDialogActive = true; 
            StartText();
        }
    }

    public void EndDialog()
    {
        isDialogActive = false; 
        dialogData.isChooseActive = false; 
        uiManager.dialogPanel.SetActive(false);
    }

    protected virtual void Update()
    {
        if (talkCount != 0)
        {
            if (Input.GetMouseButtonDown(0) && currentCoroutine == null)
            {
                if (talkCount < dialogData.dialogTexts.Length)
                {
                    string text = dialogData.dialogTexts[talkCount];
                    currentCoroutine = StartCoroutine(TypingDialog(text));
                    talkCount++;
                }
            }
        }

        if (talkCount == dialogData.dialogTexts.Length)
        {
            dialogData.isChooseActive = true;
            //optionui.ShowOptions(dialogData.nextDialogs); // 옵션 패널에 다음 대화 표시
        }

        uiManager.enterPanel.SetActive(dialogData.isEnterActive);
    }

    public void InteractWithNPC()
    {
        if (!isDialogActive) 
        {
            DialogStart(); 
        }
    }

    public void StartText()
    {
        if (currentCoroutine == null && talkCount < dialogData.dialogTexts.Length)
        {
            string firstText = dialogData.dialogTexts[0];
            currentCoroutine = StartCoroutine(TypingDialog(firstText));
            talkCount++;
        }

    }

    //public void StartNextDialog(NPCDialogData nextDialog)
    //{
    //    dialogData = nextDialog; // 다음 대화 데이터로 변경
    //    talkCount = 0; // 대화 카운트 초기화
    //    StartText(); // 첫 번째 텍스트 시작
    //}

    private IEnumerator TypingDialog(string text)
    {
        dialogData.isEnterActive = false;
        uiManager.dialogText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            if (Input.GetMouseButtonDown(1))
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
