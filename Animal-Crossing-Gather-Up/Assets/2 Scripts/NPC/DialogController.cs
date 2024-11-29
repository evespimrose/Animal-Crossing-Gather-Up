using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;



public class DialogController : MonoBehaviour
{
    protected NPCPanelUI uiManager;
    protected Coroutine currentCoroutine;
    protected int talkCount = 0;

    protected NPCDialogData dialogData; //scriptableObject ÂüÁ¶

    protected virtual void Start()
    {
        uiManager = FindObjectOfType<NPCPanelUI>();
        uiManager.dialogPanel.SetActive(false);
        uiManager.choosePanel.SetActive(false);
        uiManager.enterPanel.SetActive(false);
    }


    public void DialogStart()
    {
        StartText();
    }

    protected void Update()
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
                    if (talkCount == dialogData.dialogTexts.Length)
                    {
                        dialogData.isChooseActive = true;
                    }
                }
            }
        }
        uiManager.enterPanel.SetActive(dialogData.isEnterActive);
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
        uiManager.choosePanel.SetActive(dialogData.isChooseActive);
        currentCoroutine = null;
    }

}
