using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class DaisyMaeController : DialogController, INPCDialog
{
    public NPCDialogData daisyDailogData;

    private void Awake()
    {
        daisyDailogData.isChooseActive = false;
        daisyDailogData.isEnterActive = false;
        for (int i = 0; i < daisyDailogData.dialogIndex.Length; i++)
        {
            daisyDailogData.dialogIndex[i] = 0;
        }
    }

    protected override void Start()
    {
        base.Start();
        dialogData = daisyDailogData;
        daisyDailogData.currentOption = "";
        daisyDailogData.currentOption = UIManager.Instance.optionUI.currentOption;
    }

    protected override void Update()
    {

        base.Update();
        if (daisyDailogData.currentOption != UIManager.Instance.optionUI.currentOption && UIManager.Instance.optionUI.currentOption != null)
        {
            daisyDailogData.currentOption = UIManager.Instance.optionUI.currentOption;
            SelectedOptionAfter();
        }
    }

    public void NPCDialogStart()
    {
        string[] timmyOptions = { "test1", "test2" };
        UIManager.Instance.optionUI.SetOptions(timmyOptions);
        UIManager.Instance.dialogUI.dialogPanel.SetActive(true);
        DialogStart(daisyDailogData.dialogTexts, daisyDailogData.dialogIndex[0]);
        print("������ ��ȭ ����");
    }

    public void SelectedOptionAfter()
    {
        if (daisyDailogData.currentOption == "test1")
        {
            dialogData.isChooseActive = false;
            UIManager.Instance.optionUI.PanelActive(daisyDailogData.isChooseActive);

            DialogStart(daisyDailogData.nextDialogTexts, daisyDailogData.dialogIndex[1]);

            string[] roadriOptions = { "�׽�Ʈ 1 ��" };
            UIManager.Instance.optionUI.SetOptions(roadriOptions);
        }

        else if (daisyDailogData.currentOption == "test2")
        {
            dialogData.isChooseActive = false;
            UIManager.Instance.optionUI.PanelActive(daisyDailogData.isChooseActive);

            DialogStart(daisyDailogData.thirdDialogTexts, daisyDailogData.dialogIndex[2]);

            string[] roadriOptions = { "�׽�Ʈ 2 ��" };
            UIManager.Instance.optionUI.SetOptions(roadriOptions);
        }

        else if (daisyDailogData.currentOption == "�׽�Ʈ 1 ��")
        {
            EndDialog();
            print("������ 1 �ɼ� �׽�Ʈ �Ϸ�");
        }

        else if (daisyDailogData.currentOption == "�׽�Ʈ 2 ��")
        {
            EndDialog();
            print("������ 2 �ɼ� �׽�Ʈ �Ϸ�");
        }
    }
}
