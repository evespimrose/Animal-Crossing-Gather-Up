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
    protected bool isDialogActive = false; // ��ȭ Ȱ��ȭ ����

    protected NPCDialogData dialogData; //scriptableObject ����

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
            //optionui.ShowOptions(dialogData.nextDialogs); // �ɼ� �гο� ���� ��ȭ ǥ��
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
    //    dialogData = nextDialog; // ���� ��ȭ �����ͷ� ����
    //    talkCount = 0; // ��ȭ ī��Ʈ �ʱ�ȭ
    //    StartText(); // ù ��° �ؽ�Ʈ ����
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
