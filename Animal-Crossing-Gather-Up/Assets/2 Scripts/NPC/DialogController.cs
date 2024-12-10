using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;



public class DialogController : MonoBehaviour, IDialogState
{
    public Coroutine currentCoroutine { get; private set; }

    protected NPCDialogData dialogData;
    protected NPCInteraction interactionNPC;

    protected string[] activeDialogTexts;
    protected int activeDialogIndex;


    protected virtual void Start()
    {
        interactionNPC = GetComponentInParent<NPCInteraction>();
        UIManager.Instance.optionUI.optionPanel.SetActive(false);
        UIManager.Instance.DialogPanelOff();
    }

    public void DialogStart(string[] dialogTexts, int dialogIndexCount)
    {
        dialogData.isChooseActive = false;
        activeDialogTexts = dialogTexts;
        activeDialogIndex = dialogIndexCount;
        DialogActive(dialogTexts, dialogIndexCount);
    }

    public void ResetDialog()
    {
        interactionNPC.isDialogActive = false;
        dialogData.isChooseActive = false;
        dialogData.dialogIndex = 0;
        activeDialogTexts = null;
        UIManager.Instance.optionUI.optionPanel.SetActive(false);
        UIManager.Instance.dialogUI.dialogPanel.SetActive(false);
    }

    protected virtual void Update()
    {
        if (UIManager.Instance.dialogUI.dialogPanel.activeSelf && activeDialogTexts != null)
        {
            DialogActive(activeDialogTexts, activeDialogIndex);
        }
    }

    public void DialogActive(string[] setDialogTexts, int dialogIndexCount)
    {

        if (dialogData.dialogIndex == 0 && currentCoroutine == null)
        {
            activeDialogTexts = setDialogTexts;
            string text = setDialogTexts[dialogData.dialogIndex];
            currentCoroutine = StartCoroutine(TypingDialog(text));
            dialogData.dialogIndex++;
        }


        if (Input.GetKeyDown(KeyCode.Space) && currentCoroutine == null)
        {
            string dialogText = setDialogTexts[dialogData.dialogIndex];
            currentCoroutine = StartCoroutine(TypingDialog(dialogText));
            dialogData.dialogIndex++;
        }


        if (dialogData.dialogIndex >= setDialogTexts.Length)
        {
            dialogData.dialogIndex = 0;
            dialogData.isChooseActive = true;
            activeDialogTexts = null;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResetDialog();
        }
    }

    private IEnumerator TypingDialog(string text)
    {
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

        UIManager.Instance.dialogUI.enterPanel.SetActive(true);
        UIManager.Instance.optionUI.PanelActive(dialogData.isChooseActive);
        currentCoroutine = null;
    }

    public void AfterSelectedOption()
    {
        dialogData.isChooseActive = false;
        UIManager.Instance.optionUI.PanelActive(dialogData.isChooseActive);
    }
}