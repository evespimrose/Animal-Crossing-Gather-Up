using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReeseController : DialogController, INPCDialog
{
    public NPCDialogData reeseDialogData;
    private void Awake()
    {
        dialogData = reeseDialogData;
    }

    protected override void Start()
    {
        base.Start();
        ResetDialog();
    }

    protected override void Update()
    {
        base.Update();
        if (reeseDialogData.currentOption != UIManager.Instance.optionUI.currentOption && UIManager.Instance.optionUI.currentOption != null)
        {
            reeseDialogData.currentOption = UIManager.Instance.optionUI.currentOption;
            SelectedOptionAfter();
        }
    }

    public void NPCDialogStart()
    {
        string[] reeseOptions = { "���� ���� �׽�Ʈ", "�ϴ� ��" };
        UIManager.Instance.optionUI.SetOptions(reeseOptions);
        UIManager.Instance.dialogUI.dialogPanel.SetActive(true);
        DialogStart(reeseDialogData.dialogTexts, reeseDialogData.dialogIndex);
        print("���� ��ȭ ����");
    }

    public void SelectedOptionAfter()
    {

        if (reeseDialogData.currentOption == "���� ���� �׽�Ʈ")
        {
            AfterSelectedOption();
            DialogStart(reeseDialogData.nextDialogTexts, reeseDialogData.dialogIndex);

            string[] reeseOptions = { "���� �׽�Ʈ �Ϸ�" };
            UIManager.Instance.optionUI.SetOptions(reeseOptions);
        }

        if (reeseDialogData.currentOption == "�ϴ� ��")
        {
            AfterSelectedOption();
            DialogStart(reeseDialogData.thirdDialogTexts, reeseDialogData.dialogIndex);

            string[] reeseOptions = { "���� �׽�Ʈ �Ϸ�" };
            UIManager.Instance.optionUI.SetOptions(reeseOptions);
        }

        if (reeseDialogData.currentOption == "���� �׽�Ʈ �Ϸ�")
        {
            ResetDialog();
        }

        if (reeseDialogData.currentOption == "���� �׽�Ʈ �Ϸ�")
        {
            ResetDialog();
        }

        if (reeseDialogData.currentOption == "�Ǹ�")
        {
            ResetDialog();
        }

    }
}
