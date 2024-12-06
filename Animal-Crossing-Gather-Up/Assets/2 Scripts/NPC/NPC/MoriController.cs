using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class MoriController : DialogController, INPCDialog
{
    public NPCDialogData moriDialogData;

    private void Awake()
    {
        moriDialogData.isChooseActive = false;
        moriDialogData.isEnterActive = false;
        for (int i = 0; i < moriDialogData.dialogIndex.Length; i++)
        {
            moriDialogData.dialogIndex[i] = 0;
        }
    }

    protected override void Start()
    {
        base.Start();
        dialogData = moriDialogData;
        moriDialogData.currentOption = "";
        moriDialogData.currentOption = UIManager.Instance.optionUI.currentOption;
        string[] moriOptions = { "외출할래", "지금은 안할래" };
        UIManager.Instance.optionUI.SetOptions(moriOptions);
    }

    protected override void Update()
    {
        base.Update();
        if (moriDialogData.currentOption != UIManager.Instance.optionUI.currentOption && UIManager.Instance.optionUI.currentOption != null)
        {
            moriDialogData.currentOption = UIManager.Instance.optionUI.currentOption;
            SelectedOptionAfter();
        }
    }


    public void NPCDialogStart()
    {
        string[] moriOptions = { "외출할래", "지금은 안할래" };
        UIManager.Instance.optionUI.SetOptions(moriOptions);
        UIManager.Instance.dialogUI.dialogPanel.SetActive(true);
        DialogStart(moriDialogData.dialogTexts, moriDialogData.dialogIndex[0]);
        print("모리 대화 시작");
    }

    public void SelectedOptionAfter()
    {
        if (moriDialogData.currentOption == "외출할래")
        {
            dialogData.isChooseActive = false;
            UIManager.Instance.optionUI.PanelActive(moriDialogData.isChooseActive);

            DialogStart(moriDialogData.nextDialogTexts, moriDialogData.dialogIndex[1]);

            string[] moriOptions = { "마일섬 출발!" };
            UIManager.Instance.optionUI.SetOptions(moriOptions);
        }

        else if (moriDialogData.currentOption == "지금은 안할래")
        {
            dialogData.isChooseActive = false;
            UIManager.Instance.optionUI.PanelActive(moriDialogData.isChooseActive);

            DialogStart(moriDialogData.thirdDialogTexts, moriDialogData.dialogIndex[2]);

            string[] moriOptions = { "대화 종료" };
            UIManager.Instance.optionUI.SetOptions(moriOptions);
        }

        else if (moriDialogData.currentOption == "마일섬 출발!")
        {
            EndDialog();
            print("마일섬으로 출발!");
        }

        else if (moriDialogData.currentOption == "대화 종료")
        {
            EndDialog();
            print("모리 대화 종료");
        }

    }
}


