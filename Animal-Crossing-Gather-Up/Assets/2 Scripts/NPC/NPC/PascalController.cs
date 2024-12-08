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
        pascalDialogData.currentOption = "";
        pascalDialogData.currentOption = UIManager.Instance.optionUI.currentOption;
        string[] pascalOptions = { "파스칼 테스트" };
        UIManager.Instance.optionUI.SetOptions(pascalOptions);
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
        string[] pascalOptions = { "파스칼 테스트" };
        UIManager.Instance.optionUI.SetOptions(pascalOptions);
        UIManager.Instance.dialogUI.dialogPanel.SetActive(true);
        DialogStart(pascalDialogData.dialogTexts, pascalDialogData.dialogIndex[0]);
        print("파스칼 대화 시작");
    }

    public void SelectedOptionAfter()
    {
        if (pascalDialogData.currentOption == "파스칼 테스트")
        {
            dialogData.isChooseActive = false;
            UIManager.Instance.optionUI.PanelActive(pascalDialogData.isChooseActive);

            DialogStart(pascalDialogData.nextDialogTexts, pascalDialogData.dialogIndex[1]);

            string[] pascalOptions = { "파스칼 테스트 끝" };
            UIManager.Instance.optionUI.SetOptions(pascalOptions);
        }

        //else if (pascalDialogData.currentOption == "test2")
        //{
        //    dialogData.isChooseActive = false;
        //    UIManager.Instance.optionUI.PanelActive(pascalDialogData.isChooseActive);

        //    DialogStart(pascalDialogData.thirdDialogTexts, pascalDialogData.dialogIndex[2]);

        //    string[] roadriOptions = { "테스트 2 끝" };
        //    UIManager.Instance.optionUI.SetOptions(roadriOptions);
        //}

        else if (pascalDialogData.currentOption == "파스칼 테스트 끝")
        {
            EndDialog();
            print("파스칼 테스트 완료");
        }

        //else if (pascalDialogData.currentOption == "테스트 2 끝")
        //{
        //    EndDialog();
        //    print("데이지 2 옵션 테스트 완료");
        //}
    }
}
