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

    public string currentOption;
    private void Awake()
    {
        moriDialogData.isChooseActive = false;
        moriDialogData.isEnterActive = false;
        for (int i = 0; i < moriDialogData.talkCount.Length; i++)
        {
            moriDialogData.talkCount[i] = 0;
        }
    }

    protected override void Start()
    {
        base.Start();
        dialogData = moriDialogData;
        optionui.currentOption = "";
        string[] moriOptions = { "외출할래", "지금은 안할래" };
        optionui.SetOptions(moriOptions);
    }

    protected override void Update()
    {
        base.Update();
        currentOption = optionui.currentOption;
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
            dialogData.isChooseActive = false;
            string[] moriOptions = { "출발!" };
            optionui.SetOptions(moriOptions);
            DialogStart(moriDialogData.nextDialogTexts, moriDialogData.talkCount[1]);


        }

        if (optionui.currentOption == "지금은 안할래")
        {
            dialogData.isChooseActive = false;
            string[] moriOptions = { "대화종료" };
            optionui.SetOptions(moriOptions);
            DialogStart(moriDialogData.thirdDialogTexts, moriDialogData.talkCount[2]);

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
