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
        string[] moriOptions = { "리즈 상점 테스트", "하는 중" };
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
        string[] moriOptions = { "리즈 상점 테스트", "하는 중" };
        UIManager.Instance.optionUI.SetOptions(moriOptions);
        UIManager.Instance.dialogUI.dialogPanel.SetActive(true);
        DialogStart(reeseDialogData.dialogTexts, reeseDialogData.dialogIndex[0]);
        print("리즈 대화 시작");
    }

    public void SelectedOptionAfter()
    {

        if (reeseDialogData.currentOption == "리즈 상점 테스트")
        {
            dialogData.isChooseActive = false;
            UIManager.Instance.optionUI.PanelActive(reeseDialogData.isChooseActive);

            DialogStart(reeseDialogData.nextDialogTexts, reeseDialogData.dialogIndex[1]);

            string[] moriOptions = { "상점 테스트 완료" };
            UIManager.Instance.optionUI.SetOptions(moriOptions);
        }

        if (reeseDialogData.currentOption == "하는 중")
        {
            dialogData.isChooseActive = false;
            UIManager.Instance.optionUI.PanelActive(reeseDialogData.isChooseActive);

            DialogStart(reeseDialogData.thirdDialogTexts, reeseDialogData.dialogIndex[2]);

            string[] moriOptions = { "리즈 테스트 완료" };
            UIManager.Instance.optionUI.SetOptions(moriOptions);
        }

        if (reeseDialogData.currentOption == "상점 테스트 완료")
        {
            EndDialog();
            print("리즈상점 테스트 완료!");
        }

        if (reeseDialogData.currentOption == "리즈 테스트 완료")
        {
            EndDialog();
            print("리즈 테스트 완료");
        }

        if (reeseDialogData.currentOption == "판매")
        {
            EndDialog();
            print("채집물 판매 완료");
        }

    }
}
