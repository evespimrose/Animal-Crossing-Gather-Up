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
        string[] kicksOptions = { "�ŷ�", "�׳�" };
        UIManager.Instance.optionUI.SetOptions(kicksOptions);
        UIManager.Instance.ShowDialog();
        DialogStart(kicksDialogData.dialogTexts, kicksDialogData.dialogIndex);
    }

    protected override void SelectedOption()
    {
        if (kicksDialogData.currentOption == "�ŷ�")
        {
            AfterSelectedOption();
            DialogStart(kicksDialogData.nextDialogTexts, kicksDialogData.dialogIndex);

            string[] kicksOptions = { "�ŷ��ҷ�!" };
            UIManager.Instance.optionUI.SetOptions(kicksOptions);
        }

        else if (kicksDialogData.currentOption == "�׳�")
        {
            AfterSelectedOption();
            DialogStart(kicksDialogData.thirdDialogTexts, kicksDialogData.dialogIndex);

            string[] kicksOptions = { "�ȳ�" };
            UIManager.Instance.optionUI.SetOptions(kicksOptions);
        }

        else if (kicksDialogData.currentOption == "�ŷ��ҷ�!")
        {
            ResetDialog();
        }

        else if (kicksDialogData.currentOption == "�ȳ�")
        {
            ResetDialog();
        }
    }
}
