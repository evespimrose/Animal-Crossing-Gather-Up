using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PascalController : DialogController, INPCDialog
{
    public NPCDialogData pascalDialogData;
    private void Awake()
    {
        dialogData = pascalDialogData;
    }

    protected override void Start()
    {
        base.Start();
        ResetDialog();
    }

    protected override void Update()
    {
        base.Update();
        if (pascalDialogData.currentOption != UIManager.Instance.optionUI.currentOption && UIManager.Instance.optionUI.currentOption != null)
        {
            pascalDialogData.currentOption = UIManager.Instance.optionUI.currentOption;
            SelectedOptionAfter();
        }
    }

    public void NPCDialogStart()
    {
        string[] pascalOptions = { "마일섬 주민 파스칼", "파스칼 테스트 중" };
        UIManager.Instance.optionUI.SetOptions(pascalOptions);
        UIManager.Instance.dialogUI.dialogPanel.SetActive(true);
        DialogStart(pascalDialogData.dialogTexts, pascalDialogData.dialogIndex);
        print("파스칼 대화 시작");
    }

    public void SelectedOptionAfter()
    {

        if (pascalDialogData.currentOption == "마일섬 주민 파스칼")
        {
            AfterSelectedOption();
            DialogStart(pascalDialogData.nextDialogTexts, pascalDialogData.dialogIndex);

            string[] pascalOptions = { "파스칼 테스트 완료" };
            UIManager.Instance.optionUI.SetOptions(pascalOptions);
        }

        if (pascalDialogData.currentOption == "파스칼 테스트 중")
        {
            AfterSelectedOption();
            DialogStart(pascalDialogData.thirdDialogTexts, pascalDialogData.dialogIndex);

            string[] pascalOptions = { "완료입니다" };
            UIManager.Instance.optionUI.SetOptions(pascalOptions);
        }

        if (pascalDialogData.currentOption == "파스칼 테스트 완료")
        {
            ResetDialog();
        }

        if (pascalDialogData.currentOption == "완료입니다")
        {
            ResetDialog();
        }

        if (pascalDialogData.currentOption == "판매")
        {
            ResetDialog();
        }

    }
}
