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
        string[] moriOptions = { "외출할래", "지금은 안할래" };
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
        string[] moriOptions = { "장비를 구매할래", "채집물을 판매할래", "아무 것도 안할래" };
        UIManager.Instance.optionUI.SetOptions(moriOptions);
        UIManager.Instance.dialogUI.dialogPanel.SetActive(true);
        DialogStart(timmyDialogData.dialogTexts, timmyDialogData.dialogIndex[0]);
        print("티미 대화 시작");
    }

    public void SelectedOptionAfter()
    {

        if (timmyDialogData.currentOption == "장비를 구매할래")
        {
            dialogData.isChooseActive = false;
            UIManager.Instance.optionUI.PanelActive(timmyDialogData.isChooseActive);

            DialogStart(timmyDialogData.nextDialogTexts, timmyDialogData.dialogIndex[1]);

            string[] moriOptions = { "구매" };
            UIManager.Instance.optionUI.SetOptions(moriOptions);
        }

        if (timmyDialogData.currentOption == "채집물을 판매할래")
        {
            dialogData.isChooseActive = false;
            UIManager.Instance.optionUI.PanelActive(timmyDialogData.isChooseActive);

            DialogStart(timmyDialogData.thirdDialogTexts, timmyDialogData.dialogIndex[2]);

            string[] moriOptions = { "판매" };
            UIManager.Instance.optionUI.SetOptions(moriOptions);
        }

        if (timmyDialogData.currentOption == "아무 것도 안할래")
        {
            EndDialog();
            print("티미 대화 종료");
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

    }

}
