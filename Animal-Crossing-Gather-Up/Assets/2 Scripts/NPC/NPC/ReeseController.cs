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
        reeseDialogData.currentOption = UIManager.Instance.optionUI.currentOption;
        string[] moriOptions = { "���� ���� �׽�Ʈ", "�ϴ� ��" };
        UIManager.Instance.optionUI.SetOptions(moriOptions);
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
        string[] moriOptions = { "���� ���� �׽�Ʈ", "�ϴ� ��" };
        UIManager.Instance.optionUI.SetOptions(moriOptions);
        UIManager.Instance.dialogUI.dialogPanel.SetActive(true);
        DialogStart(reeseDialogData.dialogTexts, reeseDialogData.dialogIndex[0]);
        print("���� ��ȭ ����");
    }

    public void SelectedOptionAfter()
    {

        if (reeseDialogData.currentOption == "���� ���� �׽�Ʈ")
        {
            dialogData.isChooseActive = false;
            UIManager.Instance.optionUI.PanelActive(reeseDialogData.isChooseActive);

            DialogStart(reeseDialogData.nextDialogTexts, reeseDialogData.dialogIndex[1]);

            string[] moriOptions = { "���� �׽�Ʈ �Ϸ�" };
            UIManager.Instance.optionUI.SetOptions(moriOptions);
        }

        if (reeseDialogData.currentOption == "�ϴ� ��")
        {
            dialogData.isChooseActive = false;
            UIManager.Instance.optionUI.PanelActive(reeseDialogData.isChooseActive);

            DialogStart(reeseDialogData.thirdDialogTexts, reeseDialogData.dialogIndex[2]);

            string[] moriOptions = { "���� �׽�Ʈ �Ϸ�" };
            UIManager.Instance.optionUI.SetOptions(moriOptions);
        }

        if (reeseDialogData.currentOption == "���� �׽�Ʈ �Ϸ�")
        {
            EndDialog();
            print("������� �׽�Ʈ �Ϸ�!");
        }

        if (reeseDialogData.currentOption == "���� �׽�Ʈ �Ϸ�")
        {
            EndDialog();
            print("���� �׽�Ʈ �Ϸ�");
        }

        if (reeseDialogData.currentOption == "�Ǹ�")
        {
            EndDialog();
            print("ä���� �Ǹ� �Ϸ�");
        }

    }
}
