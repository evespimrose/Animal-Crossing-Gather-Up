using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaisyMaeController : DialogController, INPCDialog
{
	public NPCDialogData daisyDialogData;

	private void Awake()
	{
		SetDialogData(daisyDialogData);
	}

	protected override void Start()
	{
		base.Start();
		ResetDialog();
		npcDialogOption = daisyDialogData.dialogOption;
	}

	public void NPCDialogStart()
	{
		string[] daisyOptions = { "����?", "��, ������" };
		UIManager.Instance.optionUI.SetOptions(daisyOptions);
		UIManager.Instance.ShowDialog();
		DialogStart(daisyDialogData.dialogTexts, daisyDialogData.dialogIndex);
	}

	protected override void SelectedOption()
	{
		if (daisyDialogData.dialogOption == "����?")
		{
			AfterSelectedOption();
			DialogStart(daisyDialogData.nextDialogTexts, daisyDialogData.dialogIndex);

			string[] daisyOptions = { "����" };
			UIManager.Instance.optionUI.SetOptions(daisyOptions);
		}

		else if (daisyDialogData.dialogOption == "��, ������")
		{
			AfterSelectedOption();
			DialogStart(daisyDialogData.thirdDialogTexts, daisyDialogData.dialogIndex);

			string[] daisyOptions = { "��, ���߿� ��" };
			UIManager.Instance.optionUI.SetOptions(daisyOptions);
		}

		else if (daisyDialogData.dialogOption == "����")
		{
			AfterSelectedOption();
			ResetDialog();
		}

		else if (daisyDialogData.dialogOption == "��, ���߿� ��")
		{
			AfterSelectedOption();
			ResetDialog();
		}
	}
}
