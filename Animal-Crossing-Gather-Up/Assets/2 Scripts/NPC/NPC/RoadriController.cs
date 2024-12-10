using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class RoadriController : DialogController, INPCDialog
{
    public NPCDialogData roadriDialogData;

    private void Awake()
    {
        dialogData = roadriDialogData;
    }
    protected override void Start()
    {
        base.Start();
        ResetDialog();
    }

    protected override void Update()
    {

        base.Update();
        if (roadriDialogData.currentOption != UIManager.Instance.optionUI.currentOption && UIManager.Instance.optionUI.currentOption != null)
        {
            roadriDialogData.currentOption = UIManager.Instance.optionUI.currentOption;
            SelectedOptionAfter();
        }
    }

    public void NPCDialogStart()
    {
        string[] roadriOptions = { "집에 갈래", "섬을 더 돌아볼래" };
        UIManager.Instance.optionUI.SetOptions(roadriOptions);
        UIManager.Instance.ShowDialog();
        DialogStart(roadriDialogData.dialogTexts, roadriDialogData.dialogIndex);
    }

    public void SelectedOptionAfter()
    {
        if (roadriDialogData.currentOption == "집에 갈래")
        {
            AfterSelectedOption();
            DialogStart(roadriDialogData.nextDialogTexts, roadriDialogData.dialogIndex);

            string[] roadriOptions = { "출발!" };
            UIManager.Instance.optionUI.SetOptions(roadriOptions);
        }

        else if (roadriDialogData.currentOption == "섬을 더 돌아볼래")
        {
            AfterSelectedOption();
            DialogStart(roadriDialogData.thirdDialogTexts, roadriDialogData.dialogIndex);

            string[] roadriOptions = { "대화 종료" };
            UIManager.Instance.optionUI.SetOptions(roadriOptions);
        }

        else if (roadriDialogData.currentOption == "출발!")
        {
            ResetDialog();
        }

        else if (roadriDialogData.currentOption == "대화 종료")
        {
            ResetDialog();
        }
    }

}
