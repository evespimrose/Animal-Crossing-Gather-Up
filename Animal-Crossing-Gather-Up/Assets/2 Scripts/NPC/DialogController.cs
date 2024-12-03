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

    protected NPCDialogData dialogData;
    protected virtual void Start()
    {
        uiManager = FindObjectOfType<NPCPanelUI>();
        optionui = FindObjectOfType<OptionUI>();
        uiManager.dialogPanel.SetActive(false);
        optionui.optionPanel.SetActive(false);
        uiManager.enterPanel.SetActive(false);
    }

    public void DialogStart(string[] setDialogTexts, int talkCount)
    {
        FirstTextStart(setDialogTexts, talkCount);
        if (setDialogTexts.Length > 0)
        {
            EnterDialog(setDialogTexts, talkCount);
        }

    }

    public void EndDialog()
    {
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
        optionui.PanelActive(dialogData.isChooseActive);
        uiManager.enterPanel.SetActive(dialogData.isEnterActive);
    }

    public void FirstTextStart(string[] SetdialogTexts, int dialogIndex)
    {
        if (currentCoroutine == null)
        {
            string firstText = SetdialogTexts[0];
            currentCoroutine = StartCoroutine(TypingDialog(firstText));
        }
    }

    public void EnterDialog(string[] SetdialogTexts, int dialogIndex)
    {
        if (dialogIndex < SetdialogTexts.Length)
        {
            if (Input.GetKeyDown(KeyCode.Space) && currentCoroutine == null)
            {
                string text = SetdialogTexts[dialogIndex];
                currentCoroutine = StartCoroutine(TypingDialog(text));
            }
        }

        if (dialogIndex >= SetdialogTexts.Length)
        {
            dialogData.isChooseActive = true;
            dialogIndex = 0;
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
