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
        pascalDialogData.currentOption = "";
        pascalDialogData.currentOption = UIManager.Instance.optionUI.currentOption;
        string[] pascalOptions = { "�Ľ�Į �׽�Ʈ" };
        UIManager.Instance.optionUI.SetOptions(pascalOptions);
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
        string[] pascalOptions = { "�Ľ�Į �׽�Ʈ" };
        UIManager.Instance.optionUI.SetOptions(pascalOptions);
        UIManager.Instance.dialogUI.dialogPanel.SetActive(true);
        DialogStart(pascalDialogData.dialogTexts, pascalDialogData.dialogIndex[0]);
        print("�Ľ�Į ��ȭ ����");
    }

    public void SelectedOptionAfter()
    {
        if (pascalDialogData.currentOption == "�Ľ�Į �׽�Ʈ")
        {
            dialogData.isChooseActive = false;
            UIManager.Instance.optionUI.PanelActive(pascalDialogData.isChooseActive);

            DialogStart(pascalDialogData.nextDialogTexts, pascalDialogData.dialogIndex[1]);

            string[] pascalOptions = { "�Ľ�Į �׽�Ʈ ��" };
            UIManager.Instance.optionUI.SetOptions(pascalOptions);
        }

        //else if (pascalDialogData.currentOption == "test2")
        //{
        //    dialogData.isChooseActive = false;
        //    UIManager.Instance.optionUI.PanelActive(pascalDialogData.isChooseActive);

        //    DialogStart(pascalDialogData.thirdDialogTexts, pascalDialogData.dialogIndex[2]);

        //    string[] roadriOptions = { "�׽�Ʈ 2 ��" };
        //    UIManager.Instance.optionUI.SetOptions(roadriOptions);
        //}

        else if (pascalDialogData.currentOption == "�Ľ�Į �׽�Ʈ ��")
        {
            EndDialog();
            print("�Ľ�Į �׽�Ʈ �Ϸ�");
        }

        //else if (pascalDialogData.currentOption == "�׽�Ʈ 2 ��")
        //{
        //    EndDialog();
        //    print("������ 2 �ɼ� �׽�Ʈ �Ϸ�");
        //}
    }
}
