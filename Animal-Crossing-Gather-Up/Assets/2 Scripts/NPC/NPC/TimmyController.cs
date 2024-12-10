using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class TimmyController : DialogController, INPCDialog
{
    public NPCDialogData timmyDialogData;
    private void Awake()
    {
        dialogData = timmyDialogData;
    }

    protected override void Start()
    {
        base.Start();
        ResetDialog();
    }

    protected override void Update()
    {
        base.Update();
        if (timmyDialogData.currentOption != UIManager.Instance.optionUI.currentOption && UIManager.Instance.optionUI.currentOption != null)
        {
            timmyDialogData.currentOption = UIManager.Instance.optionUI.currentOption;
            SelectedOptionAfter();
        }
    }

    public void NPCDialogStart()
    {
        string[] timmyOptions = { "��� �����ҷ�", "ä������ �Ǹ��ҷ�", "�ƹ� �͵� ���ҷ�" };
        UIManager.Instance.optionUI.SetOptions(timmyOptions);
        UIManager.Instance.dialogUI.dialogPanel.SetActive(true);
        DialogStart(timmyDialogData.dialogTexts, timmyDialogData.dialogIndex);
        print("Ƽ�� ��ȭ ����");
    }

    public void SelectedOptionAfter()
    {

        if (timmyDialogData.currentOption == "��� �����ҷ�")
        {
            AfterSelectedOption();
            DialogStart(timmyDialogData.nextDialogTexts, timmyDialogData.dialogIndex);

            string[] timmyOptions = { "����" };
            UIManager.Instance.optionUI.SetOptions(timmyOptions);
        }

        if (timmyDialogData.currentOption == "ä������ �Ǹ��ҷ�")
        {
            AfterSelectedOption();
            DialogStart(timmyDialogData.thirdDialogTexts, timmyDialogData.dialogIndex);

            string[] timmyOptions = { "�Ǹ�" };
            UIManager.Instance.optionUI.SetOptions(timmyOptions);
        }

        if (timmyDialogData.currentOption == "�ƹ� �͵� ���ҷ�")
        {
            ResetDialog();
        }

        if (timmyDialogData.currentOption == "����")
        {
            ResetDialog();
        }

        if (timmyDialogData.currentOption == "�Ǹ�")
        {
            ResetDialog();
        }

    }

}