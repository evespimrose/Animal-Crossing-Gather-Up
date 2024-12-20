using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class MoriController : DialogController, INPCDialog
{
	public NPCDialogData moriDialogData;

	private void Awake()
	{
		SetDialogData(moriDialogData);
	}

	protected override void Start()
	{
		base.Start();
		ResetDialog();
	}


	public void NPCDialogStart()
	{
		string[] moriOptions = { "외출할래", "지금은 안할래" };
		UIManager.Instance.optionUI.SetOptions(moriOptions);
		UIManager.Instance.ShowDialog();
		DialogStart(moriDialogData.dialogTexts, moriDialogData.dialogIndex);
	}

	protected override void SelectedOption()
	{
		if (moriDialogData.dialogOption == "외출할래")
		{
			AfterSelectedOption();
			DialogStart(moriDialogData.nextDialogTexts, moriDialogData.dialogIndex);

			string[] moriOptions = { "마일섬 출발" };
			UIManager.Instance.optionUI.SetOptions(moriOptions);
		}

		else if (moriDialogData.dialogOption == "지금은 안할래")
		{
			AfterSelectedOption();
			DialogStart(moriDialogData.thirdDialogTexts, moriDialogData.dialogIndex);

			string[] moriOptions = { "대화 종료" };
			UIManager.Instance.optionUI.SetOptions(moriOptions);
		}

		else if (moriDialogData.dialogOption == "마일섬 출발")
		{
			// check mileTicket and remove
			if (GameManager.Instance.inventory.CheckAndUseMileTicket())
			{
                ResetDialog();
                GameSceneManager.Instance.ChangeScene("MileIsland");
			}

			else if (!GameManager.Instance.inventory.CheckAndUseMileTicket())
			{
				AfterSelectedOption();
				DialogStart(moriDialogData.fourthDialogTexts, moriDialogData.dialogIndex);

				string[] moriOptions = { "대화 종료" };
				UIManager.Instance.optionUI.SetOptions(moriOptions);
			}

		}

        else if (moriDialogData.dialogOption == "대화 종료")
        {
            ResetDialog();
        }

    }
}


