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
		string[] daisyOptions = { "¹ºµ¥?", "¾Æ, ±¦Âú¾Æ" };
		UIManager.Instance.optionUI.SetOptions(daisyOptions);
		UIManager.Instance.ShowDialog();
		DialogStart(daisyDialogData.dialogTexts, daisyDialogData.dialogIndex);
	}

	protected override void SelectedOption()
	{
		if (daisyDialogData.dialogOption == "¹ºµ¥?")
		{
			AfterSelectedOption();
			DialogStart(daisyDialogData.nextDialogTexts, daisyDialogData.dialogIndex);

			string[] daisyOptions = { "°í¸¶¿ö" };
			UIManager.Instance.optionUI.SetOptions(daisyOptions);
		}

		else if (daisyDialogData.dialogOption == "¾Æ, ±¦Âú¾Æ")
		{
			AfterSelectedOption();
			DialogStart(daisyDialogData.thirdDialogTexts, daisyDialogData.dialogIndex);

			string[] daisyOptions = { "ÀÀ, ³ªÁß¿¡ ºÁ" };
			UIManager.Instance.optionUI.SetOptions(daisyOptions);
		}

		else if (daisyDialogData.dialogOption == "°í¸¶¿ö")
		{
			AfterSelectedOption();
			ResetDialog();
		}

		else if (daisyDialogData.dialogOption == "ÀÀ, ³ªÁß¿¡ ºÁ")
		{
			AfterSelectedOption();
			ResetDialog();
		}
	}
}
