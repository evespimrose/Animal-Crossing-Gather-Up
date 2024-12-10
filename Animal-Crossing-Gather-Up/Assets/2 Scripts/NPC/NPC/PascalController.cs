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
        string[] pascalOptions = { "����� ����?", "���߿� �˷���" };
        UIManager.Instance.optionUI.SetOptions(pascalOptions);
        UIManager.Instance.ShowDialog();
        DialogStart(pascalDialogData.dialogTexts, pascalDialogData.dialogIndex);
    }

    protected override void SelectedOption()
    {
        if (pascalDialogData.dialogOption == "����� ����?")
        {
            AfterSelectedOption();
            DialogStart(pascalDialogData.nextDialogTexts, pascalDialogData.dialogIndex);

            string[] pascalOptions = { "��, ����" };
            UIManager.Instance.optionUI.SetOptions(pascalOptions);
        }

        if (pascalDialogData.dialogOption == "���߿� �˷���")
        {
            AfterSelectedOption();
            DialogStart(pascalDialogData.thirdDialogTexts, pascalDialogData.dialogIndex);

            string[] pascalOptions = { "��, ���߿� ��" };
            UIManager.Instance.optionUI.SetOptions(pascalOptions);
        }

        if (pascalDialogData.dialogOption == "��, ����")
        {
            AfterSelectedOption();
            ResetDialog();
        }

        if (pascalDialogData.dialogOption == "��, ���߿� ��")
        {
            AfterSelectedOption();
            ResetDialog();
        }
    }
}
