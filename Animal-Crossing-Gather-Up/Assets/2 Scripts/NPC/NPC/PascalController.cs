using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PascalController : DialogController, INPCDialog
{
    public NPCDialogData pascalDialogData;
    private void Awake()
    {
        SetDialogData(pascalDialogData);
    }

    protected override void Start()
    {
        base.Start();
        ResetDialog();
    }

    public void NPCDialogStart()
    {
        string[] pascalOptions = { "비밀이 뭔데?", "나중에 알려줘" };
        UIManager.Instance.optionUI.SetOptions(pascalOptions);
        UIManager.Instance.ShowDialog();
        DialogStart(pascalDialogData.dialogTexts, pascalDialogData.dialogIndex);
    }

    protected override void SelectedOption()
    {
        if (pascalDialogData.dialogOption == "비밀이 뭔데?")
        {
            AfterSelectedOption();
            DialogStart(pascalDialogData.nextDialogTexts, pascalDialogData.dialogIndex);

            string[] pascalOptions = { "응, 고마워" };
            UIManager.Instance.optionUI.SetOptions(pascalOptions);
        }

        if (pascalDialogData.dialogOption == "나중에 알려줘")
        {
            AfterSelectedOption();
            DialogStart(pascalDialogData.thirdDialogTexts, pascalDialogData.dialogIndex);

            string[] pascalOptions = { "응, 나중에 봐" };
            UIManager.Instance.optionUI.SetOptions(pascalOptions);
        }

        if (pascalDialogData.dialogOption == "응, 고마워")
        {
            AfterSelectedOption();
            ResetDialog();
        }

        if (pascalDialogData.dialogOption == "응, 나중에 봐")
        {
            AfterSelectedOption();
            ResetDialog();
        }
    }
}
