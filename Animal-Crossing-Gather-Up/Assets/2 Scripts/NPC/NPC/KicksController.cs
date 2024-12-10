using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class KicksController : DialogController, INPCDialog
{
    public NPCDialogData kicksDialogData;

    private void Awake()
    {
        SetDialogData(kicksDialogData);
    }

    protected override void Start()
    {
        base.Start();
        ResetDialog();
    }


    public void NPCDialogStart()
    {
        string[] kicksOptions = { "거래", "그냥" };
        UIManager.Instance.optionUI.SetOptions(kicksOptions);
        UIManager.Instance.ShowDialog();
        DialogStart(kicksDialogData.dialogTexts, kicksDialogData.dialogIndex);
    }

    protected override void SelectedOption()
    {
        if (kicksDialogData.currentOption == "거래")
        {
            AfterSelectedOption();
            DialogStart(kicksDialogData.nextDialogTexts, kicksDialogData.dialogIndex);

            string[] kicksOptions = { "거래할래!" };
            UIManager.Instance.optionUI.SetOptions(kicksOptions);
        }

        else if (kicksDialogData.currentOption == "그냥")
        {
            AfterSelectedOption();
            DialogStart(kicksDialogData.thirdDialogTexts, kicksDialogData.dialogIndex);

            string[] kicksOptions = { "안녕" };
            UIManager.Instance.optionUI.SetOptions(kicksOptions);
        }

        else if (kicksDialogData.currentOption == "거래할래!")
        {
            ResetDialog();
        }

        else if (kicksDialogData.currentOption == "안녕")
        {
            ResetDialog();
        }
    }
}
