using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
//using Unity.Android.Types;



public class DialogController : MonoBehaviour, IDialogState
{
    public Coroutine currentCoroutine { get; private set; }

    protected NPCDialogData dialogData;
    protected NPCInteraction interactionNPC;

    protected string[] activeDialogTexts;
    protected int activeDialogIndex;


    protected virtual void Start()
    {
        interactionNPC = FindObjectOfType<NPCInteraction>();
        UIManager.Instance.dialogUI.dialogPanel.SetActive(false);
        UIManager.Instance.optionUI.optionPanel.SetActive(false);
        UIManager.Instance.dialogUI.enterPanel.SetActive(false);
    }


    public void DialogStart(string[] setDialogTexts, int dialogIndexCount)
    {
        dialogData.isChooseActive = false;
        activeDialogTexts = setDialogTexts;
        activeDialogIndex = dialogIndexCount;
        FirstText(setDialogTexts, dialogIndexCount);
    }

    public void EndDialog()
    {
        interactionNPC.isDialogActive = false;
        dialogData.isChooseActive = false;
        for (int i = 0; i < dialogData.dialogIndex.Length; i++)
        {
            dialogData.dialogIndex[i] = 0;
        }
        activeDialogTexts = null;
        UIManager.Instance.optionUI.optionPanel.SetActive(false);
        UIManager.Instance.dialogUI.dialogPanel.SetActive(false);
    }

    protected virtual void Update()
    {
        //if (uiManager != null && uiManager.enterPanel != null)
        //{
        //    print($"isEnterActive: {dialogData.isEnterActive}");
        //    uiManager.enterPanel.SetActive(dialogData.isEnterActive);
        //}

        if (UIManager.Instance.dialogUI.dialogPanel.activeSelf && activeDialogTexts != null)
        {
            EnterDialog(activeDialogTexts, activeDialogIndex);
        }
    }

    public void FirstText(string[] SetdialogTexts, int dialogIndexCount)
    {
        if (currentCoroutine == null)
        {
            if (dialogData.dialogIndex[dialogIndexCount] >= SetdialogTexts.Length)
            {
                dialogData.dialogIndex[dialogIndexCount] = 0;
            }
            string firstText = SetdialogTexts[dialogData.dialogIndex[dialogIndexCount]];
            currentCoroutine = StartCoroutine(TypingDialog(firstText));
            dialogData.dialogIndex[dialogIndexCount]++;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            interactionNPC.isDialogActive = false;
            dialogData.isChooseActive = false;
            for (int i = 0; i < dialogData.dialogIndex.Length; i++)
            {
                dialogData.dialogIndex[i] = 0;
            }
            activeDialogTexts = null;
            UIManager.Instance.optionUI.optionPanel.SetActive(false);
            UIManager.Instance.dialogUI.dialogPanel.SetActive(false);
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
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                interactionNPC.isDialogActive = false;
                dialogData.isChooseActive = false;
                for (int i = 0; i < dialogData.dialogIndex.Length; i++)
                {
                    dialogData.dialogIndex[i] = 0;
                }
                activeDialogTexts = null;
                UIManager.Instance.optionUI.optionPanel.SetActive(false);
                UIManager.Instance.dialogUI.dialogPanel.SetActive(false);
            }
        }
    }

    private IEnumerator TypingDialog(string text)
    {
        dialogData.isEnterActive = false;
        UIManager.Instance.dialogUI.enterPanel.SetActive(false);
        UIManager.Instance.dialogUI.dialogText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                UIManager.Instance.dialogUI.dialogText.text = text;
                break;
            }

            UIManager.Instance.dialogUI.dialogText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }

        dialogData.isEnterActive = true;
        UIManager.Instance.dialogUI.enterPanel.SetActive(true);
        UIManager.Instance.optionUI.PanelActive(dialogData.isChooseActive);
        currentCoroutine = null;
    }

}
