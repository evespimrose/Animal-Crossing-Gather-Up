using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class DaisyMaeController : DialogController, INPCDialog
{
    public NPCDialogData daisyDialogData;

    private void Awake()
    {
        SetDialogData(daisyDialogData);
    }

    protected override void Start()
    {
        base.Start();
        ResetDialog();
        currentOption = daisyDialogData.currentOption;
    }

    public void NPCDialogStart()
    {
        string[] daisyOptions = { "test1", "test2" };
        UIManager.Instance.optionUI.SetOptions(daisyOptions);
        UIManager.Instance.ShowDialog();
        DialogStart(daisyDialogData.dialogTexts, daisyDialogData.dialogIndex);
    }

    protected override void SelectedOption()
    {
        if (daisyDialogData.currentOption == "test1")
        {
            AfterSelectedOption();
            DialogStart(daisyDialogData.nextDialogTexts, daisyDialogData.dialogIndex);

            string[] daisyOptions = { "������ 1 �׽�Ʈ" };
            UIManager.Instance.optionUI.SetOptions(daisyOptions);
        }

        else if (daisyDialogData.currentOption == "test2")
        {
            AfterSelectedOption();
            DialogStart(daisyDialogData.thirdDialogTexts, daisyDialogData.dialogIndex);

            string[] daisyOptions = { "������ 2 �׽�Ʈ" };
            UIManager.Instance.optionUI.SetOptions(daisyOptions);
        }

        else if (daisyDialogData.currentOption == "������ 1 �׽�Ʈ")
        {
            ResetDialog();
        }

        else if (daisyDialogData.currentOption == "������ 2 �׽�Ʈ")
        {
            ResetDialog();
        }
    }
}
