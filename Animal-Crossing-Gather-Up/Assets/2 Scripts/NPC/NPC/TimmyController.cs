using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class TimmyController : DialogController, INPCDialog
{
    public NPCDialogData timmyDialogData;
    private void Awake()
    {
        dialogData = timmyDialogData;
    }

    protected override void Start()
    {
        base.Start();
        ResetDialog();
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
        string[] timmyOptions = { "장비를 구매할래", "채집물을 판매할래", "아무 것도 안할래" };
        UIManager.Instance.optionUI.SetOptions(timmyOptions);
        UIManager.Instance.dialogUI.dialogPanel.SetActive(true);
        DialogStart(timmyDialogData.dialogTexts, timmyDialogData.dialogIndex);
        print("티미 대화 시작");
    }

    public void SelectedOptionAfter()
    {

        if (timmyDialogData.currentOption == "장비를 구매할래")
        {
            AfterSelectedOption();
            DialogStart(timmyDialogData.nextDialogTexts, timmyDialogData.dialogIndex);

            string[] timmyOptions = { "구매" };
            UIManager.Instance.optionUI.SetOptions(timmyOptions);
        }

        if (timmyDialogData.currentOption == "채집물을 판매할래")
        {
            AfterSelectedOption();
            DialogStart(timmyDialogData.thirdDialogTexts, timmyDialogData.dialogIndex);

            string[] timmyOptions = { "판매" };
            UIManager.Instance.optionUI.SetOptions(timmyOptions);
        }

        if (timmyDialogData.currentOption == "아무 것도 안할래")
        {
            ResetDialog();
        }

        if (timmyDialogData.currentOption == "구매")
        {
            ResetDialog();
        }

        if (timmyDialogData.currentOption == "판매")
        {
            ResetDialog();
        }

    }

}