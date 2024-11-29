using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadriController : DialogController, IAreaNPC
{

    public NPCDialogData roadriDialogData;

    private void Awake()
    {
        roadriDialogData.isChooseActive = false;
        roadriDialogData.isEnterActive = false;
    }

    protected override void Start()
    {
        base.Start();
        dialogData = roadriDialogData;
        //uiManager.firstChooseText.text = "돌아갈래";
        //uiManager.thirdChooseText.text = "섬을 더 돌아볼래";
        //string[] optionTexts = { "외출", "안함" };
        //optionUI.SetOption(optionTexts);
    }



    public void AirplaneDialogStart()
    {
        uiManager.dialogPanel.SetActive(true);
        DialogStart();
    }

    public void FirstAccept()
    {
        Debug.Log("원래 섬으로 돌아감");
        //uiManager.choosePanel.SetActive(false);
        uiManager.dialogPanel.SetActive(false);
        uiManager.enterPanel.SetActive(false);
    }
    public void EndTalk()
    {
        Debug.Log("마일섬 더 투어");
        //uiManager.choosePanel.SetActive(false);
        uiManager.dialogPanel.SetActive(false);
        uiManager.enterPanel.SetActive(false);
    }

    public void InteractionPlayer()
    {

    }
}
