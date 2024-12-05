using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class RoadriController : DialogController, INPCDialog
{
    public NPCDialogData roadriDialogData;

    private void Awake()
    {
        roadriDialogData.isChooseActive = false;
        roadriDialogData.isEnterActive = false;
        for (int i = 0; i < roadriDialogData.dialogIndex.Length; i++)
        {
            roadriDialogData.dialogIndex[i] = 0;
        }
    }

    protected override void Start()
    {
        base.Start();
        dialogData = roadriDialogData;
        roadriDialogData.currentOption = "";
        roadriDialogData.currentOption = optionui.currentOption;
        string[] roadriOptions = { "���ư���", "���� �� �����Ұ�" };
        optionui.SetOptions(roadriOptions);
    }

    protected override void Update()
    {

        base.Update();
        if (roadriDialogData.currentOption != optionui.currentOption && optionui.currentOption != null)
        {
            roadriDialogData.currentOption = optionui.currentOption;
            SelectedOptionAfter();
        }
    }

    public void NPCDialogStart()
    {
        string[] roadriOptions = { "���ư���", "���� �� ���ƺ���" };
        optionui.SetOptions(roadriOptions);
        uiManager.dialogPanel.SetActive(true);
        DialogStart(roadriDialogData.dialogTexts, roadriDialogData.dialogIndex[0]);
        print("�ε帮 ��ȭ ����");
    }

    public void SelectedOptionAfter()
    {
        if (roadriDialogData.currentOption == "���ư���")
        {
            dialogData.isChooseActive = false;
            optionui.PanelActive(roadriDialogData.isChooseActive);

            DialogStart(roadriDialogData.nextDialogTexts, roadriDialogData.dialogIndex[1]);

            string[] roadriOptions = { "���!" };
            optionui.SetOptions(roadriOptions);
        }

        else if (roadriDialogData.currentOption == "���� �� ���ƺ���")
        {
            dialogData.isChooseActive = false;
            optionui.PanelActive(roadriDialogData.isChooseActive);

            DialogStart(roadriDialogData.thirdDialogTexts, roadriDialogData.dialogIndex[2]);

            string[] roadriOptions = { "��ȭ ����" };
            optionui.SetOptions(roadriOptions);
        }

        else if (roadriDialogData.currentOption == "���!")
        {
            EndDialog();
            print("���ϼ� ����");
        }

        else if (roadriDialogData.currentOption == "��ȭ ����")
        {
            EndDialog();
            print("�ε帮 ��ȭ ����");
        }
    }

}
