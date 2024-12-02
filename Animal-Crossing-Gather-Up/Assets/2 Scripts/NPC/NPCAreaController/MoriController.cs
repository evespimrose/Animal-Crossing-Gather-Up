using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class MoriController : DialogController
{
    private NPCState moriState;
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
        string[] moriOptions = { "외출할래", "지금은 안할래" };
        optionui.SetOptions(moriOptions);
    }

    protected override void Update()
    {
        base.Update();
        optionui.CursorMove();
        SelectedOptionAfter();
    }

    public void MoriDialogStart()
    {
        uiManager.dialogPanel.SetActive(true);
        DialogStart(moriDialogData.dialogTexts, moriDialogData.talkCount[0]);
    }

    public void SelectedOptionAfter()
    {
        if (optionui.currentOption == "외출할래")
        {
            FirstTextStart(moriDialogData.nextDialogTexts, moriDialogData.talkCount[1]);
            StartDialog(moriDialogData.nextDialogTexts, moriDialogData.talkCount[1]);
            string[] moriOptions = { "출발!" };
            optionui.SetOptions(moriOptions);
        }

        if (optionui.currentOption == "지금은 안할래")
        {
            FirstTextStart(moriDialogData.thirdDialogTexts, moriDialogData.talkCount[2]);
            StartDialog(moriDialogData.thirdDialogTexts, moriDialogData.talkCount[2]);
            string[] moriOptions = { "대화종료" };
            optionui.SetOptions(moriOptions);
        }

        if (optionui.currentOption == "출발!")
        {
            EndDialog();
            print("마일섬으로 출발!");
        }

        if (optionui.currentOption == "대화종료")
        {
            EndDialog();
            print("모리 대화 종료");
        }
    }

}
