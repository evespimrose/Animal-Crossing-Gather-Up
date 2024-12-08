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
        reeseDialogData.currentOption = "";
        reeseDialogData.currentOption = UIManager.Instance.optionUI.currentOption;
        string[] reeseOptions = { "리즈 테스트" };
        UIManager.Instance.optionUI.SetOptions(reeseOptions);
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
        string[] reeseOptions = { "리즈 테스트" };
        UIManager.Instance.optionUI.SetOptions(reeseOptions);
        UIManager.Instance.dialogUI.dialogPanel.SetActive(true);
        DialogStart(reeseDialogData.dialogTexts, reeseDialogData.dialogIndex[0]);
        print("리즈 대화 시작");
    }

    public void SelectedOptionAfter()
    {
        if (reeseDialogData.currentOption == "리즈 테스트")
        {
            dialogData.isChooseActive = false;
            UIManager.Instance.optionUI.PanelActive(reeseDialogData.isChooseActive);

            DialogStart(reeseDialogData.nextDialogTexts, reeseDialogData.dialogIndex[1]);

            string[] reeseOptions = { "리즈 테스트 끝" };
            UIManager.Instance.optionUI.SetOptions(reeseOptions);
        }

        //else if (pascalDialogData.currentOption == "test2")
        //{
        //    dialogData.isChooseActive = false;
        //    UIManager.Instance.optionUI.PanelActive(pascalDialogData.isChooseActive);

        //    DialogStart(pascalDialogData.thirdDialogTexts, pascalDialogData.dialogIndex[2]);

        //    string[] roadriOptions = { "테스트 2 끝" };
        //    UIManager.Instance.optionUI.SetOptions(roadriOptions);
        //}

        else if (reeseDialogData.currentOption == "리즈 테스트 끝")
        {
            EndDialog();
            print("리즈 테스트 완료");
        }

        //else if (pascalDialogData.currentOption == "테스트 2 끝")
        //{
        //    EndDialog();
        //    print("데이지 2 옵션 테스트 완료");
        //}
    }
}
