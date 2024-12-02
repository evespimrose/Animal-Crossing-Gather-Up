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

    public void DialogStart(string[] dialogTexts, int talkCount)
    {
        if (!isDialogActive)
        {
            isDialogActive = true; //대화 시작 bool
            FirstTextStart(dialogTexts, talkCount);
        }
    }

    public void EndDialog()
    {
        isDialogActive = false;
        dialogData.isChooseActive = false;
        for (int i = 0; i < dialogData.talkCount.Length; i++)
        {
            dialogData.talkCount[i] = 0;
        }
        optionui.optionPanel.SetActive(false);
        uiManager.dialogPanel.SetActive(false);
    }

    protected virtual void Update()
    {
        StartDialog(dialogData.dialogTexts, dialogData.talkCount[0]);
        uiManager.enterPanel.SetActive(dialogData.isEnterActive);

    }

    public void StartDialog(string[] SetdialogTexts, int talkCount)
    {
        if (talkCount < SetdialogTexts.Length)
        {
            if (Input.GetKeyDown(KeyCode.Space) && currentCoroutine == null)
            {
                if (talkCount < SetdialogTexts.Length)
                {
                    string text = SetdialogTexts[talkCount];
                    currentCoroutine = StartCoroutine(TypingDialog(text));
                    talkCount++;
                }
            }
        }

        if (talkCount == SetdialogTexts.Length)
        {
            dialogData.isChooseActive = true;
        }

    }

    public void FirstTextStart(string[] SetdialogTexts, int talkCount)
    {
        if (currentCoroutine == null)
        {
            string firstText = SetdialogTexts[talkCount];
            currentCoroutine = StartCoroutine(TypingDialog(firstText));
            talkCount++;
        }

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
