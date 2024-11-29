using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class MoriController : DialogController, IDialog
{

    public NPCDialogData moriDialogData;

    private void Awake()
    {
        moriDialogData.isChooseActive = false;
        moriDialogData.isEnterActive = false;
    }
    protected override void Start()
    {
        base.Start();
        dialogData = moriDialogData;
        uiManager.firstChooseText.text = "외출할래";
        uiManager.thirdChooseText.text = "지금은 안할래";
        //string[] optionTexts = { "외출", "안함" };
        //optionUI.SetOption(optionTexts);
    }

    public void AirplaneDialogStart()
    {
        uiManager.dialogPanel.SetActive(true);
        DialogStart();
    }

    public void FirstAccept()
    {
        Debug.Log("마일섬 출발");
        uiManager.choosePanel.SetActive(false);
        uiManager.dialogPanel.SetActive(false);
        uiManager.enterPanel.SetActive(false);
    }
    public void EndTalk()
    {
        Debug.Log("모리와의 대화 종료");
        uiManager.choosePanel.SetActive(false);
        uiManager.dialogPanel.SetActive(false);
        uiManager.enterPanel.SetActive(false);
    }
}
