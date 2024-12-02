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
            talkCount = 0; //��ȭ ī��Ʈ �ʱ�ȭ
            isDialogActive = true; //��ȭ ���� bool
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
            //optionui.ShowOptions(dialogData.nextDialogs); // �ɼ� �гο� ���� ��ȭ ǥ��
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
        dialogData = nextDialog; // ���� ��ȭ �����ͷ� ����
        optionui.SetOptions(nextOptions);
        talkCount = 0; // ��ȭ ī��Ʈ �ʱ�ȭ
        StartText(); // ù ��° �ؽ�Ʈ ����

    }

    private IEnumerator TypingDialog(string text)
    {
        dialogData.isEnterActive = false;
        uiManager.dialogText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            if (Input.GetKeyDown(KeyCode.T)) //T ������ �ڷ�ƾ ���߰� ��ü �ؽ�Ʈ
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
