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
    protected int nextTalkCount = 0;
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
            talkCount = 0; //대화 카운트 초기화
            isDialogActive = true; //대화 시작 bool
            StartText();
        }
    }

    public void EndDialog()
    {
        isDialogActive = false;
        dialogData.isChooseActive = false;
        optionui.optionPanel.SetActive(false);
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

        if (nextTalkCount != 0)
        {
            if (Input.GetKeyDown(KeyCode.Space) && currentCoroutine == null)
            {
                if (nextTalkCount < dialogData.nextDialogTexts.Length)
                {
                    string text = dialogData.nextDialogTexts[nextTalkCount];
                    currentCoroutine = StartCoroutine(TypingDialog(text));
                    nextTalkCount++;
                }
            }

            uiManager.enterPanel.SetActive(dialogData.isEnterActive);
        }
    }

    public void InteractWithNPC()
    {
        if (!isDialogActive)
        {
            DialogStart();
        }
    }

    public void SelectedOptionAndNextDialog()
    {
        if (currentCoroutine == null && nextTalkCount < dialogData.nextDialogTexts.Length)
        {
            string text = dialogData.nextDialogTexts[0];
            currentCoroutine = StartCoroutine(TypingDialog(text));
            nextTalkCount++;
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

    public void StartNextDialog(NPCDialogData nextDialog, string[] nextOptions)
    {
        dialogData = nextDialog; // 다음 대화 데이터로 변경
        optionui.SetOptions(nextOptions);
        talkCount = 0; // 대화 카운트 초기화
        StartText(); // 첫 번째 텍스트 시작

    }

    private IEnumerator TypingDialog(string text)
    {
        dialogData.isEnterActive = false;
        uiManager.dialogText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            if (Input.GetKeyDown(KeyCode.T)) //T 누르면 코루틴 멈추고 전체 텍스트
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
