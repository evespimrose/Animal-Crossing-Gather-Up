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
        string[] daisyOptions = { "����?", "��, ������" };
        UIManager.Instance.optionUI.SetOptions(daisyOptions);
        UIManager.Instance.ShowDialog();
        DialogStart(daisyDialogData.dialogTexts, daisyDialogData.dialogIndex);
    }

    protected override void SelectedOption()
    {
        if (daisyDialogData.currentOption == "����?")
        {
            AfterSelectedOption();
            DialogStart(daisyDialogData.nextDialogTexts, daisyDialogData.dialogIndex);

            string[] daisyOptions = { "����" };
            UIManager.Instance.optionUI.SetOptions(daisyOptions);
        }

        else if (daisyDialogData.currentOption == "��, ������")
        {
            AfterSelectedOption();
            DialogStart(daisyDialogData.thirdDialogTexts, daisyDialogData.dialogIndex);

            string[] daisyOptions = { "��!" };
            UIManager.Instance.optionUI.SetOptions(daisyOptions);
        }

        else if (daisyDialogData.currentOption == "����")
        {
            AfterSelectedOption();
            ResetDialog();
        }

        else if (daisyDialogData.currentOption == "��!")
        {
            AfterSelectedOption();
            ResetDialog();
        }
    }
}
