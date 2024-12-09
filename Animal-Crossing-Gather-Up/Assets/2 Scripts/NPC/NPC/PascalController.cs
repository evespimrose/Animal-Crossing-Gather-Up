using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PascalController : DialogController, INPCDialog
{
    public NPCDialogData pascalDialogData;
    private void Awake()
    {
        pascalDialogData.isChooseActive = false;
        pascalDialogData.isEnterActive = false;
        for (int i = 0; i < pascalDialogData.dialogIndex.Length; i++)
        {
            pascalDialogData.dialogIndex[i] = 0;
        }
    }

    protected override void Start()
    {
        base.Start();
        dialogData = pascalDialogData;
        pascalDialogData.currentOption = UIManager.Instance.optionUI.currentOption;
        string[] moriOptions = { "마일섬 주민 파스칼", "파스칼 테스트 중" };
        UIManager.Instance.optionUI.SetOptions(moriOptions);
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
        string[] moriOptions = { "마일섬 주민 파스칼", "파스칼 테스트 중" };
        UIManager.Instance.optionUI.SetOptions(moriOptions);
        UIManager.Instance.dialogUI.dialogPanel.SetActive(true);
        DialogStart(pascalDialogData.dialogTexts, pascalDialogData.dialogIndex[0]);
        print("파스칼 대화 시작");
    }

    public void SelectedOptionAfter()
    {

        if (pascalDialogData.currentOption == "마일섬 주민 파스칼")
        {
            dialogData.isChooseActive = false;
            UIManager.Instance.optionUI.PanelActive(pascalDialogData.isChooseActive);

            DialogStart(pascalDialogData.nextDialogTexts, pascalDialogData.dialogIndex[1]);

            string[] moriOptions = { "파스칼 테스트 완료" };
            UIManager.Instance.optionUI.SetOptions(moriOptions);
        }

        if (pascalDialogData.currentOption == "파스칼 테스트 중")
        {
            dialogData.isChooseActive = false;
            UIManager.Instance.optionUI.PanelActive(pascalDialogData.isChooseActive);

            DialogStart(pascalDialogData.thirdDialogTexts, pascalDialogData.dialogIndex[2]);

            string[] moriOptions = { "완료입니다" };
            UIManager.Instance.optionUI.SetOptions(moriOptions);
        }

        if (pascalDialogData.currentOption == "파스칼 테스트 완료")
        {
            EndDialog();
            print("파스칼 테스트 끝났습니다");
        }

        if (pascalDialogData.currentOption == "완료입니다")
        {
            EndDialog();
            print("파스칼 테스트 완료입니다");
        }

        if (pascalDialogData.currentOption == "판매")
        {
            EndDialog();
            print("채집물 판매 완료");
        }

    }
}
