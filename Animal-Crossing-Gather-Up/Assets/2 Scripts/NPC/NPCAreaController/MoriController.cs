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
        string[] moriOptions = { "외출할래", "지금은 안할래", "키키키ㅣ키", "푸하하ㅏ하하" };
        //텍스트에 배경색 넣기 = <mark=#FFDA3750>외출할래</mark>
        optionui.SetOptions(moriOptions);
    }

    protected override void Update()
    {
        base.Update();
        optionui.CursorMove();
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
