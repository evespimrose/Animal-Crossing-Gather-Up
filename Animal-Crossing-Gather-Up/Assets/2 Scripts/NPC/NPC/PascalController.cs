using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PascalController : DialogController, INPCDialog
{
    public NPCDialogData pascalDialogData;
    private void Awake()
    {
        pascalDialogData.isChooseActive = false;
        pascalDialogData.isEnterActive = false;
        for (int i = 0; i < pascalDialogData.dialogIndex.Length; i++)
        {
            pascalDialogData.dialogIndex[i] = 0;
        }
    }

    protected override void Start()
    {
        base.Start();
        dialogData = pascalDialogData;
        pascalDialogData.currentOption = UIManager.Instance.optionUI.currentOption;
        string[] moriOptions = { "���ϼ� �ֹ� �Ľ�Į", "�Ľ�Į �׽�Ʈ ��" };
        UIManager.Instance.optionUI.SetOptions(moriOptions);
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
        string[] moriOptions = { "���ϼ� �ֹ� �Ľ�Į", "�Ľ�Į �׽�Ʈ ��" };
        UIManager.Instance.optionUI.SetOptions(moriOptions);
        UIManager.Instance.dialogUI.dialogPanel.SetActive(true);
        DialogStart(pascalDialogData.dialogTexts, pascalDialogData.dialogIndex[0]);
        print("�Ľ�Į ��ȭ ����");
    }

    public void SelectedOptionAfter()
    {

        if (pascalDialogData.currentOption == "���ϼ� �ֹ� �Ľ�Į")
        {
            dialogData.isChooseActive = false;
            UIManager.Instance.optionUI.PanelActive(pascalDialogData.isChooseActive);

            DialogStart(pascalDialogData.nextDialogTexts, pascalDialogData.dialogIndex[1]);

            string[] moriOptions = { "�Ľ�Į �׽�Ʈ �Ϸ�" };
            UIManager.Instance.optionUI.SetOptions(moriOptions);
        }

        if (pascalDialogData.currentOption == "�Ľ�Į �׽�Ʈ ��")
        {
            dialogData.isChooseActive = false;
            UIManager.Instance.optionUI.PanelActive(pascalDialogData.isChooseActive);

            DialogStart(pascalDialogData.thirdDialogTexts, pascalDialogData.dialogIndex[2]);

            string[] moriOptions = { "�Ϸ��Դϴ�" };
            UIManager.Instance.optionUI.SetOptions(moriOptions);
        }

        if (pascalDialogData.currentOption == "�Ľ�Į �׽�Ʈ �Ϸ�")
        {
            EndDialog();
            print("�Ľ�Į �׽�Ʈ �������ϴ�");
        }

        if (pascalDialogData.currentOption == "�Ϸ��Դϴ�")
        {
            EndDialog();
            print("�Ľ�Į �׽�Ʈ �Ϸ��Դϴ�");
        }

        if (pascalDialogData.currentOption == "�Ǹ�")
        {
            EndDialog();
            print("ä���� �Ǹ� �Ϸ�");
        }

    }
}
