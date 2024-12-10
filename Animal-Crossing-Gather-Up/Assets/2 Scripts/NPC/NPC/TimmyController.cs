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
        UIManager.Instance.ShowDialog();
        DialogStart(timmyDialogData.dialogTexts, timmyDialogData.dialogIndex);
    }

    public void SelectedOptionAfter()
    {

        if (timmyDialogData.currentOption == "장비를 구매할래")
        {
            AfterSelectedOption();
            ResetDialog();
            UIManager.Instance.OpenPurchasePanel();
        }

        if (timmyDialogData.currentOption == "채집물을 판매할래")
        {
            AfterSelectedOption();
            ResetDialog();
            UIManager.Instance.OpenInventory();
        }

        if (timmyDialogData.currentOption == "아무 것도 안할래")
        {
            AfterSelectedOption();
            ResetDialog();
        }
    }

}