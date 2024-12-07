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
        string[] roadriOptions = { "집에 갈래", "섬을 더 돌아볼래" };
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
        string[] roadriOptions = { "집에 갈래", "섬을 더 돌아볼래" };
        optionui.SetOptions(roadriOptions);
        uiManager.dialogPanel.SetActive(true);
        DialogStart(roadriDialogData.dialogTexts, roadriDialogData.dialogIndex[0]);
        print("로드리 대화 시작");
    }

    public void SelectedOptionAfter()
    {
        if (roadriDialogData.currentOption == "집에 갈래")
        {
            dialogData.isChooseActive = false;
            optionui.PanelActive(roadriDialogData.isChooseActive);

            DialogStart(roadriDialogData.nextDialogTexts, roadriDialogData.dialogIndex[1]);

            string[] roadriOptions = { "출발!" };
            optionui.SetOptions(roadriOptions);
        }

        else if (roadriDialogData.currentOption == "섬을 더 돌아볼래")
        {
            dialogData.isChooseActive = false;
            optionui.PanelActive(roadriDialogData.isChooseActive);

            DialogStart(roadriDialogData.thirdDialogTexts, roadriDialogData.dialogIndex[2]);

            string[] roadriOptions = { "대화 종료" };
            optionui.SetOptions(roadriOptions);
        }

        else if (roadriDialogData.currentOption == "출발!")
        {
            EndDialog();
            print("집으로 출발");
        }

        else if (roadriDialogData.currentOption == "대화 종료")
        {
            EndDialog();
            print("로드리 대화 종료");
        }
    }

}
