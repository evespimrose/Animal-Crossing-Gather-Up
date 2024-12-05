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
        timmyDialogData.currentOption = optionui.currentOption;
        string[] moriOptions = { "�����ҷ�", "������ ���ҷ�" };
        optionui.SetOptions(moriOptions);
    }

    protected override void Update()
    {
        base.Update();
        if (timmyDialogData.currentOption != optionui.currentOption && optionui.currentOption != null)
        {
            timmyDialogData.currentOption = optionui.currentOption;
            SelectedOptionAfter();
        }
    }

    public void NPCDialogStart()
    {
        string[] moriOptions = { "��� �����ҷ�", "ä������ �Ǹ��ҷ�", "�ƹ� �͵� ���ҷ�" };
        optionui.SetOptions(moriOptions);
        uiManager.dialogPanel.SetActive(true);
        DialogStart(timmyDialogData.dialogTexts, timmyDialogData.dialogIndex[0]);
        print("Ƽ�� ��ȭ ����");
    }

    public void SelectedOptionAfter()
    {

        if (timmyDialogData.currentOption == "��� �����ҷ�")
        {
            dialogData.isChooseActive = false;
            optionui.PanelActive(timmyDialogData.isChooseActive);

            DialogStart(timmyDialogData.nextDialogTexts, timmyDialogData.dialogIndex[1]);

            string[] moriOptions = { "����" };
            optionui.SetOptions(moriOptions);
        }

        if (timmyDialogData.currentOption == "ä������ �Ǹ��ҷ�")
        {
            dialogData.isChooseActive = false;
            optionui.PanelActive(timmyDialogData.isChooseActive);

            DialogStart(timmyDialogData.thirdDialogTexts, timmyDialogData.dialogIndex[2]);

            string[] moriOptions = { "�Ǹ�" };
            optionui.SetOptions(moriOptions);
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
