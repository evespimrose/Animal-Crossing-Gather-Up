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
    protected string npcDialogOption;

    protected virtual void Start()
    {
        interactionNPC = GetComponentInParent<NPCInteraction>();
        dialogData.dialogOption = UIManager.Instance.optionUI.npcDialogOption;
        UIManager.Instance.CloseOptions();
        UIManager.Instance.CloseDialog();
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
        UIManager.Instance.CloseOptions();
        UIManager.Instance.CloseDialog();
    }

    protected void Update()
    {
        if (UIManager.Instance.dialogUI.dialogPanel.activeSelf && activeDialogTexts != null)
        {
            DialogActive(activeDialogTexts, activeDialogIndex);
        }

        if (dialogData.dialogOption != UIManager.Instance.optionUI.npcDialogOption && UIManager.Instance.optionUI.npcDialogOption != null)
        {
            dialogData.dialogOption = UIManager.Instance.optionUI.npcDialogOption;
            SelectedOption();
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
        UIManager.Instance.optionUI.PanelActive(false);
    }

    public void SetDialogData(NPCDialogData newDialogData)
    {
        dialogData = newDialogData;
        newDialogData.dialogOption = npcDialogOption;
    }

    protected virtual void SelectedOption()
    {

    }
}