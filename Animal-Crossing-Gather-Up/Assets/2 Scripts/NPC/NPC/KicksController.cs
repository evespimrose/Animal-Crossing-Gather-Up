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
        string[] kicksOptions = { "����? ������", "���߿� ������" };
        UIManager.Instance.optionUI.SetOptions(kicksOptions);
        UIManager.Instance.ShowDialog();
        DialogStart(kicksDialogData.dialogTexts, kicksDialogData.dialogIndex);
    }

    protected override void SelectedOption()
    {
        if (kicksDialogData.dialogOption == "����? ������")
        {
            AfterSelectedOption();
            DialogStart(kicksDialogData.nextDialogTexts, kicksDialogData.dialogIndex);

            string[] kicksOptions = { "��... ����" };
            UIManager.Instance.optionUI.SetOptions(kicksOptions);
        }

        else if (kicksDialogData.dialogOption == "���߿� ������")
        {
            AfterSelectedOption();
            DialogStart(kicksDialogData.thirdDialogTexts, kicksDialogData.dialogIndex);

            string[] kicksOptions = { "��, ���߿� ��" };
            UIManager.Instance.optionUI.SetOptions(kicksOptions);
        }

        else if (kicksDialogData.dialogOption == "��... ����")
        {
            AfterSelectedOption();
            ResetDialog();
        }

        else if (kicksDialogData.dialogOption == "��, ���߿� ��")
        {
            AfterSelectedOption();
            ResetDialog();
        }
    }
}
