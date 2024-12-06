using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class DaisyMaeController : DialogController, INPCDialog
{
    public NPCDialogData daisyDailogData;

    private void Awake()
    {
        daisyDailogData.isChooseActive = false;
        daisyDailogData.isEnterActive = false;
        for (int i = 0; i < daisyDailogData.dialogIndex.Length; i++)
        {
            daisyDailogData.dialogIndex[i] = 0;
        }
    }

    protected override void Start()
    {
        base.Start();
        dialogData = daisyDailogData;
        daisyDailogData.currentOption = "";
        daisyDailogData.currentOption = optionui.currentOption;
        string[] roadriOptions = { "test1", "test2" };
        optionui.SetOptions(roadriOptions);
    }

    protected override void Update()
    {

        base.Update();
        if (daisyDailogData.currentOption != optionui.currentOption && optionui.currentOption != null)
        {
            daisyDailogData.currentOption = optionui.currentOption;
            SelectedOptionAfter();
        }
    }

    public void NPCDialogStart()
    {
        string[] timmyOptions = { "test1", "test2" };
        optionui.SetOptions(timmyOptions);
        uiManager.dialogPanel.SetActive(true);
        DialogStart(daisyDailogData.dialogTexts, daisyDailogData.dialogIndex[0]);
        print("데이지 대화 시작");
    }

    public void SelectedOptionAfter()
    {
        if (daisyDailogData.currentOption == "test1")
        {
            dialogData.isChooseActive = false;
            optionui.PanelActive(daisyDailogData.isChooseActive);

            DialogStart(daisyDailogData.nextDialogTexts, daisyDailogData.dialogIndex[1]);

            string[] roadriOptions = { "테스트 1 끝" };
            optionui.SetOptions(roadriOptions);
        }

        else if (daisyDailogData.currentOption == "test2")
        {
            dialogData.isChooseActive = false;
            optionui.PanelActive(daisyDailogData.isChooseActive);

            DialogStart(daisyDailogData.thirdDialogTexts, daisyDailogData.dialogIndex[2]);

            string[] roadriOptions = { "테스트 2 끝" };
            optionui.SetOptions(roadriOptions);
        }

        else if (daisyDailogData.currentOption == "테스트 1 끝")
        {
            EndDialog();
            print("데이지 1 옵션 테스트 완료");
        }

        else if (daisyDailogData.currentOption == "테스트 2 끝")
        {
            EndDialog();
            print("데이지 2 옵션 테스트 완료");
        }
    }
}
