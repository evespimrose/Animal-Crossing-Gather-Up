using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReeseController : DialogController, INPCDialog
{
    public NPCDialogData reeseDialogData;
    private void Awake()
    {
        dialogData = reeseDialogData;
    }

    protected override void Start()
    {
        base.Start();
        ResetDialog();
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
        string[] reeseOptions = { "리즈 상점 테스트", "하는 중" };
        UIManager.Instance.optionUI.SetOptions(reeseOptions);
        UIManager.Instance.dialogUI.dialogPanel.SetActive(true);
        DialogStart(reeseDialogData.dialogTexts, reeseDialogData.dialogIndex);
        print("리즈 대화 시작");
    }

    public void SelectedOptionAfter()
    {

        if (reeseDialogData.currentOption == "리즈 상점 테스트")
        {
            AfterSelectedOption();
            DialogStart(reeseDialogData.nextDialogTexts, reeseDialogData.dialogIndex);

            string[] reeseOptions = { "상점 테스트 완료" };
            UIManager.Instance.optionUI.SetOptions(reeseOptions);
        }

        if (reeseDialogData.currentOption == "하는 중")
        {
            AfterSelectedOption();
            DialogStart(reeseDialogData.thirdDialogTexts, reeseDialogData.dialogIndex);

            string[] reeseOptions = { "리즈 테스트 완료" };
            UIManager.Instance.optionUI.SetOptions(reeseOptions);
        }

        if (reeseDialogData.currentOption == "상점 테스트 완료")
        {
            ResetDialog();
        }

        if (reeseDialogData.currentOption == "리즈 테스트 완료")
        {
            ResetDialog();
        }

        if (reeseDialogData.currentOption == "판매")
        {
            ResetDialog();
        }

    }
}
