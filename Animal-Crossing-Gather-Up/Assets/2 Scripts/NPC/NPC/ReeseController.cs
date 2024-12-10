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
        string[] reeseOptions = { "����", "�Ƴ� �ƾ�" };
        UIManager.Instance.optionUI.SetOptions(reeseOptions);
        UIManager.Instance.ShowDialog();
        DialogStart(reeseDialogData.dialogTexts, reeseDialogData.dialogIndex);
    }

    public void SelectedOptionAfter()
    {

        if (reeseDialogData.currentOption == "����")
        {
            AfterSelectedOption();
            DialogStart(reeseDialogData.nextDialogTexts, reeseDialogData.dialogIndex);

            string[] reeseOptions = { "���� �׽�Ʈ �Ϸ�" };
            UIManager.Instance.optionUI.SetOptions(reeseOptions);
        }

        if (reeseDialogData.currentOption == "�Ƴ� �ƾ�")
        {
            AfterSelectedOption();
            DialogStart(reeseDialogData.thirdDialogTexts, reeseDialogData.dialogIndex);

            string[] reeseOptions = { "���� �׽�Ʈ �Ϸ�" };
            UIManager.Instance.optionUI.SetOptions(reeseOptions);
        }

        if (reeseDialogData.currentOption == "���� �׽�Ʈ �Ϸ�")
        {
            ResetDialog();
        }

        if (reeseDialogData.currentOption == "���� �׽�Ʈ �Ϸ�")
        {
            ResetDialog();
        }

    }
}
