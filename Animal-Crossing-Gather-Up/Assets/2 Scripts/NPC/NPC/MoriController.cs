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
        dialogData = moriDialogData;
    }

    protected override void Start()
    {
        base.Start();
        ResetDialog();
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
        UIManager.Instance.ShowDialog();
        DialogStart(moriDialogData.dialogTexts, moriDialogData.dialogIndex);
    }

    public void SelectedOptionAfter()
    {
        if (moriDialogData.currentOption == "외출할래")
        {
            AfterSelectedOption();
            DialogStart(moriDialogData.nextDialogTexts, moriDialogData.dialogIndex);

            string[] moriOptions = { "마일섬 출발!" };
            UIManager.Instance.optionUI.SetOptions(moriOptions);
        }

        else if (moriDialogData.currentOption == "지금은 안할래")
        {
            AfterSelectedOption();
            DialogStart(moriDialogData.thirdDialogTexts, moriDialogData.dialogIndex);

            string[] moriOptions = { "대화 종료" };
            UIManager.Instance.optionUI.SetOptions(moriOptions);
        }

        else if (moriDialogData.currentOption == "마일섬 출발!")
        {
            ResetDialog();
        }

        else if (moriDialogData.currentOption == "대화 종료")
        {
            ResetDialog();
        }

    }
}


