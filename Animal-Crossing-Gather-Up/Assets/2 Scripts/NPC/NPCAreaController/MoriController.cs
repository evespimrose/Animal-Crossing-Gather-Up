using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class MoriController : DialogController, IAreaNPC
{
    private NPCState moriState;
    public NPCDialogData moriDialogData;

    private void Awake()
    {
        moriDialogData.isChooseActive = false;
        moriDialogData.isEnterActive = false;
    }
    protected override void Start()
    {
        base.Start();
        dialogData = moriDialogData;
        string[] moriOptions = { "<mark=#FFDA3750>외출할래</mark>", "<mark=#FFDA3750>지금은 안할래</mark>" };
        optionui.SetOptions(moriOptions);
    }


    public void AirplaneDialogStart()
    {
        uiManager.dialogPanel.SetActive(true);
        DialogStart();
    }

    public void FirstAccept()
    {
        Debug.Log("마일섬 출발");
        optionui.optionPanel.SetActive(false);
        uiManager.dialogPanel.SetActive(false);
        uiManager.enterPanel.SetActive(false);
    }
    public void EndTalk()
    {
        Debug.Log("모리와의 대화 종료");
        optionui.optionPanel.SetActive(false);
        uiManager.dialogPanel.SetActive(false);
        uiManager.enterPanel.SetActive(false);
    }

    public void InteractionPlayer()
    {
        moriState.LookAtPlayer();
    }
}
