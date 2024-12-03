using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadriController : DialogController
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

    }



    public void AirplaneDialogStart()
    {
        uiManager.dialogPanel.SetActive(true);
        DialogStart(dialogData.dialogTexts, dialogData.dialogIndex[0]);
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
