using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class KicksController : DialogController, INPCDialog
{
    public NPCDialogData kicksDialogData;

    private void Awake()
    {
        dialogData = kicksDialogData;

    }

    protected override void Start()
    {
        base.Start();
        ResetDialog();
    }

    protected override void Update()
    {

        base.Update();
        if (kicksDialogData.currentOption != UIManager.Instance.optionUI.currentOption && UIManager.Instance.optionUI.currentOption != null)
        {
            kicksDialogData.currentOption = UIManager.Instance.optionUI.currentOption;
            SelectedOptionAfter();
        }
    }

    public void NPCDialogStart()
    {
        string[] kicksOptions = { "�ŷ�", "�׳�" };
        UIManager.Instance.optionUI.SetOptions(kicksOptions);
        UIManager.Instance.dialogUI.dialogPanel.SetActive(true);
        DialogStart(kicksDialogData.dialogTexts, kicksDialogData.dialogIndex);
        print("ű ��ȭ ����");
    }

    public void SelectedOptionAfter()
    {
        if (kicksDialogData.currentOption == "�ŷ�")
        {
            AfterSelectedOption();
            DialogStart(kicksDialogData.nextDialogTexts, kicksDialogData.dialogIndex);

            string[] kicksOptions = { "�ŷ��ҷ�!" };
            UIManager.Instance.optionUI.SetOptions(kicksOptions);
        }

        else if (kicksDialogData.currentOption == "�׳�")
        {
            AfterSelectedOption();
            DialogStart(kicksDialogData.thirdDialogTexts, kicksDialogData.dialogIndex);

            string[] kicksOptions = { "�ȳ�" };
            UIManager.Instance.optionUI.SetOptions(kicksOptions);
        }

        else if (kicksDialogData.currentOption == "�ŷ��ҷ�!")
        {
            ResetDialog();
        }

        else if (kicksDialogData.currentOption == "�ȳ�")
        {
            ResetDialog();
        }
    }
}
