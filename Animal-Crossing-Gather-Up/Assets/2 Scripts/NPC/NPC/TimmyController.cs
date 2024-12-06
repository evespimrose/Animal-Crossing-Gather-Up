using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class TimmyController : DialogController, INPCDialog
{
    public NPCDialogData timmyDialogData;
    private void Awake()
    {
        timmyDialogData.isChooseActive = false;
        timmyDialogData.isEnterActive = false;
        for (int i = 0; i < timmyDialogData.dialogIndex.Length; i++)
        {
            timmyDialogData.dialogIndex[i] = 0;
        }
    }

    protected override void Start()
    {
        base.Start();
        dialogData = timmyDialogData;
        timmyDialogData.currentOption = UIManager.Instance.optionUI.currentOption;
        string[] moriOptions = { "�����ҷ�", "������ ���ҷ�" };
       UIManager.Instance.optionUI.SetOptions(moriOptions);
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
        string[] moriOptions = { "��� �����ҷ�", "ä������ �Ǹ��ҷ�", "�ƹ� �͵� ���ҷ�" };
        UIManager.Instance.optionUI.SetOptions(moriOptions);
        UIManager.Instance.dialogUI.dialogPanel.SetActive(true);
        DialogStart(timmyDialogData.dialogTexts, timmyDialogData.dialogIndex[0]);
        print("Ƽ�� ��ȭ ����");
    }

    public void SelectedOptionAfter()
    {

        if (timmyDialogData.currentOption == "��� �����ҷ�")
        {
            dialogData.isChooseActive = false;
            UIManager.Instance.optionUI.PanelActive(timmyDialogData.isChooseActive);

            DialogStart(timmyDialogData.nextDialogTexts, timmyDialogData.dialogIndex[1]);

            string[] moriOptions = { "����" };
            UIManager.Instance.optionUI.SetOptions(moriOptions);
        }

        if (timmyDialogData.currentOption == "ä������ �Ǹ��ҷ�")
        {
            dialogData.isChooseActive = false;
            UIManager.Instance.optionUI.PanelActive(timmyDialogData.isChooseActive);

            DialogStart(timmyDialogData.thirdDialogTexts, timmyDialogData.dialogIndex[2]);

            string[] moriOptions = { "�Ǹ�" };
            UIManager.Instance.optionUI.SetOptions(moriOptions);
        }

        if (timmyDialogData.currentOption == "�ƹ� �͵� ���ҷ�")
        {
            EndDialog();
            print("Ƽ�� ��ȭ ����");
        }

        if (timmyDialogData.currentOption == "����")
        {
            EndDialog();
            print("��� ���� �Ϸ�");
        }

        if (timmyDialogData.currentOption == "�Ǹ�")
        {
            EndDialog();
            print("ä���� �Ǹ� �Ϸ�");
        }

    }

}
