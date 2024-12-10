using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PascalController : DialogController, INPCDialog
{
    public NPCDialogData pascalDialogData;
    private void Awake()
    {
        dialogData = pascalDialogData;
    }

    protected override void Start()
    {
        base.Start();
        ResetDialog();
    }

    protected override void Update()
    {
        base.Update();
        if (pascalDialogData.currentOption != UIManager.Instance.optionUI.currentOption && UIManager.Instance.optionUI.currentOption != null)
        {
            pascalDialogData.currentOption = UIManager.Instance.optionUI.currentOption;
            SelectedOptionAfter();
        }
    }

    public void NPCDialogStart()
    {
        string[] pascalOptions = { "���ϼ� �ֹ� �Ľ�Į", "�Ľ�Į �׽�Ʈ ��" };
        UIManager.Instance.optionUI.SetOptions(pascalOptions);
        UIManager.Instance.dialogUI.dialogPanel.SetActive(true);
        DialogStart(pascalDialogData.dialogTexts, pascalDialogData.dialogIndex);
        print("�Ľ�Į ��ȭ ����");
    }

    public void SelectedOptionAfter()
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
