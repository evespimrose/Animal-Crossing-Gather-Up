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
        string[] kicksOptions = { "뭔데? 말해줘", "나중에 들을게" };
        UIManager.Instance.optionUI.SetOptions(kicksOptions);
        UIManager.Instance.ShowDialog();
        DialogStart(kicksDialogData.dialogTexts, kicksDialogData.dialogIndex);
    }

    protected override void SelectedOption()
    {
        if (kicksDialogData.dialogOption == "뭔데? 말해줘")
        {
            AfterSelectedOption();
            DialogStart(kicksDialogData.nextDialogTexts, kicksDialogData.dialogIndex);

            string[] kicksOptions = { "응... 고마워" };
            UIManager.Instance.optionUI.SetOptions(kicksOptions);
        }

        else if (kicksDialogData.dialogOption == "나중에 들을게")
        {
            AfterSelectedOption();
            DialogStart(kicksDialogData.thirdDialogTexts, kicksDialogData.dialogIndex);

            string[] kicksOptions = { "응, 나중에 봐" };
            UIManager.Instance.optionUI.SetOptions(kicksOptions);
        }

        else if (kicksDialogData.dialogOption == "응... 고마워")
        {
            AfterSelectedOption();
            ResetDialog();
        }

        else if (kicksDialogData.dialogOption == "응, 나중에 봐")
        {
            AfterSelectedOption();
            ResetDialog();
        }
    }
}
