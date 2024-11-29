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
        //uiManager.firstChooseText.text = "���ư���";
        //uiManager.thirdChooseText.text = "���� �� ���ƺ���";
        //string[] optionTexts = { "����", "����" };
        //optionUI.SetOption(optionTexts);
    }



    public void AirplaneDialogStart()
    {
        uiManager.dialogPanel.SetActive(true);
        DialogStart();
    }

    public void FirstAccept()
    {
        Debug.Log("���� ������ ���ư�");
        //uiManager.choosePanel.SetActive(false);
        uiManager.dialogPanel.SetActive(false);
        uiManager.enterPanel.SetActive(false);
    }
    public void EndTalk()
    {
        Debug.Log("���ϼ� �� ����");
        //uiManager.choosePanel.SetActive(false);
        uiManager.dialogPanel.SetActive(false);
        uiManager.enterPanel.SetActive(false);
    }

    public void InteractionPlayer()
    {

    }
}
