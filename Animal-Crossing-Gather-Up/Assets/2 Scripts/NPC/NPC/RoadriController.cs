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
        roadriDialogData.currentOption = UIManager.Instance.optionUI.currentOption; ;
    }

    protected override void Update()
    {

        base.Update();
        if (roadriDialogData.currentOption != UIManager.Instance.optionUI.currentOption && UIManager.Instance.optionUI.currentOption != null)
        {
            roadriDialogData.currentOption = UIManager.Instance.optionUI.currentOption;
            SelectedOptionAfter();
        }
    }

    public void NPCDialogStart()
    {
        string[] roadriOptions = { "집에 갈래", "섬을 더 돌아볼래" };
        UIManager.Instance.optionUI.SetOptions(roadriOptions);
        UIManager.Instance.dialogUI.dialogPanel.SetActive(true);
        DialogStart(roadriDialogData.dialogTexts, roadriDialogData.dialogIndex[0]);
        print("로드리 대화 시작");
    }

    public void SelectedOptionAfter()
    {
        if (roadriDialogData.currentOption == "집에 갈래")
        {
            dialogData.isChooseActive = false;
            UIManager.Instance.optionUI.PanelActive(roadriDialogData.isChooseActive);

            DialogStart(roadriDialogData.nextDialogTexts, roadriDialogData.dialogIndex[1]);

            string[] roadriOptions = { "출발!" };
            UIManager.Instance.optionUI.SetOptions(roadriOptions);
        }

        else if (roadriDialogData.currentOption == "섬을 더 돌아볼래")
        {
            dialogData.isChooseActive = false;
            UIManager.Instance.optionUI.PanelActive(roadriDialogData.isChooseActive);

            DialogStart(roadriDialogData.thirdDialogTexts, roadriDialogData.dialogIndex[2]);

            string[] roadriOptions = { "대화 종료" };
            UIManager.Instance.optionUI.SetOptions(roadriOptions);
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
