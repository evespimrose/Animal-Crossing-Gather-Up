using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class DaisyMaeController : DialogController, INPCDialog
{
    public NPCDialogData daisyDialogData;

    private void Awake()
    {
        dialogData = daisyDialogData;
    }

    protected override void Start()
    {
        base.Start();
        ResetDialog();
    }

    protected override void Update()
    {
        base.Update();
        if (daisyDialogData.currentOption != UIManager.Instance.optionUI.currentOption && UIManager.Instance.optionUI.currentOption != null)
        {
            daisyDialogData.currentOption = UIManager.Instance.optionUI.currentOption;
            SelectedOptionAfter();
        }
    }

    public void NPCDialogStart()
    {
        string[] daisyOptions = { "test1", "test2" };
        UIManager.Instance.optionUI.SetOptions(daisyOptions);
        UIManager.Instance.dialogUI.dialogPanel.SetActive(true);
        DialogStart(daisyDialogData.dialogTexts, daisyDialogData.dialogIndex);
    }

    public void SelectedOptionAfter()
    {
        if (daisyDialogData.currentOption == "test1")
        {
            AfterSelectedOption();
            DialogStart(daisyDialogData.nextDialogTexts, daisyDialogData.dialogIndex);

            string[] daisyOptions = { "데이지 1 first" };
            UIManager.Instance.optionUI.SetOptions(daisyOptions);
        }

        else if (daisyDialogData.currentOption == "test2")
        {
            AfterSelectedOption();
            DialogStart(daisyDialogData.thirdDialogTexts, daisyDialogData.dialogIndex);

            string[] daisyOptions = { "데이지 2 second" };
            UIManager.Instance.optionUI.SetOptions(daisyOptions);
        }

        else if (daisyDialogData.currentOption == "데이지 1 first")
        {
            ResetDialog();
        }

        else if (daisyDialogData.currentOption == "데이지 2 second")
        {
            ResetDialog();
        }
    }
}
