using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class TimmyController : DialogController, INPCArea
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
        string[] moriOptions = { "외출할래", "지금은 안할래" };
        optionui.SetOptions(moriOptions);
    }

    protected override void Update()
    {
        base.Update();
        optionui.CursorMove();

        if (timmyDialogData.currentOption != optionui.currentOption && optionui.currentOption != null)
        {
            timmyDialogData.currentOption = optionui.currentOption;
            SelectedOptionAfter();
        }
        print("티미 업데이트");
    }

    public void NPCDialogStart()
    {
        string[] moriOptions = { "장비를 구매할래", "채집물을 판매할래", "아무 것도 안할래" };
        optionui.SetOptions(moriOptions);
        uiManager.dialogPanel.SetActive(true);
        DialogStart(timmyDialogData.dialogTexts, timmyDialogData.dialogIndex[0]);
        print("티미 대화 시작");
    }

    public void SelectedOptionAfter()
    {
        if (timmyDialogData.currentOption == "장비를 구매할래")
        {
            dialogData.isChooseActive = false;
            optionui.PanelActive(timmyDialogData.isChooseActive);

            DialogStart(timmyDialogData.nextDialogTexts, timmyDialogData.dialogIndex[1]);

            string[] moriOptions = { "구매" };
            optionui.SetOptions(moriOptions);
        }

        if (timmyDialogData.currentOption == "채집물을 판매할래")
        {
            dialogData.isChooseActive = false;
            optionui.PanelActive(timmyDialogData.isChooseActive);

            DialogStart(timmyDialogData.thirdDialogTexts, timmyDialogData.dialogIndex[2]);

            string[] moriOptions = { "판매" };
            optionui.SetOptions(moriOptions);
        }

        if (timmyDialogData.currentOption == "아무 것도 안할래")
        {
            dialogData.isChooseActive = false;
            optionui.PanelActive(timmyDialogData.isChooseActive);

            DialogStart(timmyDialogData.fourthDialogTexts, timmyDialogData.dialogIndex[3]);

            string[] timmyOptions = { "대화종료 " };
            optionui.SetOptions(timmyOptions);
        }

        if (timmyDialogData.currentOption == "구매")
        {
            EndDialog();
            print("장비 구매 완료");
        }

        if (timmyDialogData.currentOption == "판매")
        {
            EndDialog();
            print("채집물 판매 완료");
        }

        if (timmyDialogData.currentOption == "대화종료")
        {
            EndDialog();
            print("티미 대화 종료");
        }
    }

}
