using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class TimmyController : DialogController, INPCDialog
{
    public NPCDialogData timmyDialogData;
    private void Awake()
    {
        SetDialogData(timmyDialogData);
    }

    protected override void Start()
    {
        base.Start();
        ResetDialog();
    }

    protected override void Update()
    {
        base.Update();
    }

    public void NPCDialogStart()
    {
        string[] timmyOptions = { "��� �����ҷ�", "ä������ �Ǹ��ҷ�", "�ƹ� �͵� ���ҷ�" };
        UIManager.Instance.optionUI.SetOptions(timmyOptions);
        UIManager.Instance.ShowDialog();
        DialogStart(timmyDialogData.dialogTexts, timmyDialogData.dialogIndex);
    }

    protected override void SelectedOption()
    {

        if (timmyDialogData.currentOption == "��� �����ҷ�")
        {
            AfterSelectedOption();
            ResetDialog();
            UIManager.Instance.OpenPurchasePanel();
        }

        if (timmyDialogData.currentOption == "ä������ �Ǹ��ҷ�")
        {
            AfterSelectedOption();
            ResetDialog();
            UIManager.Instance.OpenSellPanel();
        }

        if (timmyDialogData.currentOption == "�ƹ� �͵� ���ҷ�")
        {
            AfterSelectedOption();
            ResetDialog();
        }
    }

}