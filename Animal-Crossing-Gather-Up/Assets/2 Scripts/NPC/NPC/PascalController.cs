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
        string[] pascalOptions = { "���ϼ� �ֹ� �Ľ�Į", "�Ľ�Į �׽�Ʈ ��" };
        UIManager.Instance.optionUI.SetOptions(pascalOptions);
        UIManager.Instance.ShowDialog();
        DialogStart(pascalDialogData.dialogTexts, pascalDialogData.dialogIndex);
    }

    protected override void SelectedOption()
    {
        if (pascalDialogData.currentOption == "���ϼ� �ֹ� �Ľ�Į")
        {
            AfterSelectedOption();
            DialogStart(pascalDialogData.nextDialogTexts, pascalDialogData.dialogIndex);

            string[] pascalOptions = { "�Ľ�Į �׽�Ʈ �Ϸ�" };
            UIManager.Instance.optionUI.SetOptions(pascalOptions);
        }

        if (pascalDialogData.currentOption == "�Ľ�Į �׽�Ʈ ��")
        {
            AfterSelectedOption();
            DialogStart(pascalDialogData.thirdDialogTexts, pascalDialogData.dialogIndex);

            string[] pascalOptions = { "�Ϸ��Դϴ�" };
            UIManager.Instance.optionUI.SetOptions(pascalOptions);
        }

        if (pascalDialogData.currentOption == "�Ľ�Į �׽�Ʈ �Ϸ�")
        {
            ResetDialog();
        }

        if (pascalDialogData.currentOption == "�Ϸ��Դϴ�")
        {
            ResetDialog();
        }

        if (pascalDialogData.currentOption == "�Ǹ�")
        {
            ResetDialog();
        }

    }
}
