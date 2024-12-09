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
        string[] timmyOptions = { "�ŷ�", "�׳�" };
        UIManager.Instance.optionUI.SetOptions(timmyOptions);
        UIManager.Instance.dialogUI.dialogPanel.SetActive(true);
        DialogStart(timmyDialogData.dialogTexts, timmyDialogData.dialogIndex[0]);
        print("ű ��ȭ ����");
    }

    public void SelectedOptionAfter()
    {
        if (timmyDialogData.currentOption == "�ŷ�")
        {
            dialogData.isChooseActive = false;
            UIManager.Instance.optionUI.PanelActive(timmyDialogData.isChooseActive);

            DialogStart(timmyDialogData.nextDialogTexts, timmyDialogData.dialogIndex[1]);

            string[] roadriOptions = { "�ŷ��ҷ�!" };
            UIManager.Instance.optionUI.SetOptions(roadriOptions);
        }

        else if (timmyDialogData.currentOption == "�׳�")
        {
            dialogData.isChooseActive = false;
            UIManager.Instance.optionUI.PanelActive(timmyDialogData.isChooseActive);

            DialogStart(timmyDialogData.thirdDialogTexts, timmyDialogData.dialogIndex[2]);

            string[] roadriOptions = { "�ȳ�" };
            UIManager.Instance.optionUI.SetOptions(roadriOptions);
        }

        else if (timmyDialogData.currentOption == "�ŷ��ҷ�!")
        {
            EndDialog();
            print("ű���Լ� ��ǰ ���� �Ϸ�");
        }

        else if (timmyDialogData.currentOption == "�ȳ�")
        {
            EndDialog();
            print("ű ��ȭ ����");
        }
    }
}
