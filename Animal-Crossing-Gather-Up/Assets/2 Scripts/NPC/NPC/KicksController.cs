using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class KicksController : DialogController, INPCDialog
{
    public NPCDialogData timmyDialogData;

    private void Awake()
    {
        timmyDialogData.isChooseActive = false;
        timmyDialogData.isEnterActive = false;
        for (int i = 0; i < timmyDialogData.dialogIndex.Length; i++)
        {
            timmyDialogData.dialogIndex[i] = 0;
        }
    }

    protected override void Start()
    {
        base.Start();
        dialogData = timmyDialogData;
        timmyDialogData.currentOption = "";
        timmyDialogData.currentOption = UIManager.Instance.optionUI.currentOption;
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
        string[] timmyOptions = { "거래", "그냥" };
        UIManager.Instance.optionUI.SetOptions(timmyOptions);
        UIManager.Instance.dialogUI.dialogPanel.SetActive(true);
        DialogStart(timmyDialogData.dialogTexts, timmyDialogData.dialogIndex[0]);
        print("킥 대화 시작");
    }

    public void SelectedOptionAfter()
    {
        if (timmyDialogData.currentOption == "거래")
        {
            dialogData.isChooseActive = false;
            UIManager.Instance.optionUI.PanelActive(timmyDialogData.isChooseActive);

            DialogStart(timmyDialogData.nextDialogTexts, timmyDialogData.dialogIndex[1]);

            string[] roadriOptions = { "거래할래!" };
            UIManager.Instance.optionUI.SetOptions(roadriOptions);
        }

        else if (timmyDialogData.currentOption == "그냥")
        {
            dialogData.isChooseActive = false;
            UIManager.Instance.optionUI.PanelActive(timmyDialogData.isChooseActive);

            DialogStart(timmyDialogData.thirdDialogTexts, timmyDialogData.dialogIndex[2]);

            string[] roadriOptions = { "안녕" };
            UIManager.Instance.optionUI.SetOptions(roadriOptions);
        }

        else if (timmyDialogData.currentOption == "거래할래!")
        {
            EndDialog();
            print("킥에게서 물품 구매 완료");
        }

        else if (timmyDialogData.currentOption == "안녕")
        {
            EndDialog();
            print("킥 대화 종료");
        }
    }
}
