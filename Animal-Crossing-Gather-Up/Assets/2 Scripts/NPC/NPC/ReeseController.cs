using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReeseController : DialogController, INPCDialog
{
    public NPCDialogData reeseDialogData;

    private void Awake()
    {
        reeseDialogData.isChooseActive = false;
        reeseDialogData.isEnterActive = false;
        for (int i = 0; i < reeseDialogData.dialogIndex.Length; i++)
        {
            reeseDialogData.dialogIndex[i] = 0;
        }
    }

    protected override void Start()
    {
        base.Start();
        dialogData = reeseDialogData;
        reeseDialogData.currentOption = "";
        reeseDialogData.currentOption = UIManager.Instance.optionUI.currentOption;
        string[] reeseOptions = { "���� �׽�Ʈ" };
        UIManager.Instance.optionUI.SetOptions(reeseOptions);
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
        string[] reeseOptions = { "���� �׽�Ʈ" };
        UIManager.Instance.optionUI.SetOptions(reeseOptions);
        UIManager.Instance.dialogUI.dialogPanel.SetActive(true);
        DialogStart(reeseDialogData.dialogTexts, reeseDialogData.dialogIndex[0]);
        print("���� ��ȭ ����");
    }

    public void SelectedOptionAfter()
    {
        if (reeseDialogData.currentOption == "���� �׽�Ʈ")
        {
            dialogData.isChooseActive = false;
            UIManager.Instance.optionUI.PanelActive(reeseDialogData.isChooseActive);

            DialogStart(reeseDialogData.nextDialogTexts, reeseDialogData.dialogIndex[1]);

            string[] reeseOptions = { "���� �׽�Ʈ ��" };
            UIManager.Instance.optionUI.SetOptions(reeseOptions);
        }

        //else if (pascalDialogData.currentOption == "test2")
        //{
        //    dialogData.isChooseActive = false;
        //    UIManager.Instance.optionUI.PanelActive(pascalDialogData.isChooseActive);

        //    DialogStart(pascalDialogData.thirdDialogTexts, pascalDialogData.dialogIndex[2]);

        //    string[] roadriOptions = { "�׽�Ʈ 2 ��" };
        //    UIManager.Instance.optionUI.SetOptions(roadriOptions);
        //}

        else if (reeseDialogData.currentOption == "���� �׽�Ʈ ��")
        {
            EndDialog();
            print("���� �׽�Ʈ �Ϸ�");
        }

        //else if (pascalDialogData.currentOption == "�׽�Ʈ 2 ��")
        //{
        //    EndDialog();
        //    print("������ 2 �ɼ� �׽�Ʈ �Ϸ�");
        //}
    }
}
