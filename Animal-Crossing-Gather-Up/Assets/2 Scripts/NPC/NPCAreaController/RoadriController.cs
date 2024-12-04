using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class RoadriController : DialogController, INPCArea
{
    private NPCState moriState;
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
        string[] roadriOptions = { "���ư���", "���� �� �����Ұ�" };
        optionui.SetOptions(roadriOptions);
    }

    protected override void Update()
    {

        base.Update();
        optionui.CursorMove();
        roadriDialogData.position = gameObject.transform.position;


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

        if (roadriDialogData.currentOption == "���� �� ���ƺ���")
        {
            dialogData.isChooseActive = false;
            optionui.PanelActive(roadriDialogData.isChooseActive);

            DialogStart(roadriDialogData.thirdDialogTexts, roadriDialogData.dialogIndex[2]);

            string[] roadriOptions = { "��ȭ����" };
            optionui.SetOptions(roadriOptions);
        }

        if (roadriDialogData.currentOption == "���!")
        {
            EndDialog();
            print("���ϼ� ����");
        }

        if (roadriDialogData.currentOption == "��ȭ����")
        {
            EndDialog();
            print("�ε帮 ��ȭ ����");
        }
    }

}
