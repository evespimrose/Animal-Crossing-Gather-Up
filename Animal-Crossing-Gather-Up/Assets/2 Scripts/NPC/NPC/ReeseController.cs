using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReeseController : DialogController, INPCDialog
{
    public NPCDialogData reeseDialogData;
    private void Awake()
    {
        SetDialogData(reeseDialogData);
    }

    protected override void Start()
    {
        base.Start();
        ResetDialog();
    }

    protected override void Update()
    {
        base.Update();
    }

    public void NPCDialogStart()
    {
        string[] reeseOptions = { "구매", "아냐 됐어" };
        UIManager.Instance.optionUI.SetOptions(reeseOptions);
        UIManager.Instance.ShowDialog();
        DialogStart(reeseDialogData.dialogTexts, reeseDialogData.dialogIndex);
    }

    public void SelectedOptionAfter()
    {

        if (reeseDialogData.currentOption == "구매")
        {
            AfterSelectedOption();
            DialogStart(reeseDialogData.nextDialogTexts, reeseDialogData.dialogIndex);

            string[] reeseOptions = { "상점 테스트 완료" };
            UIManager.Instance.optionUI.SetOptions(reeseOptions);
        }

        if (reeseDialogData.currentOption == "아냐 됐어")
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

    }
}
