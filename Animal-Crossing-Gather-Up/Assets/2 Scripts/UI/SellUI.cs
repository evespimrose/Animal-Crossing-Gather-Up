using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellUI : InventoryUI
{
	public GameObject sellPanel;

	private void Start()
	{
		sellPanel.SetActive(false);
	}

	private void Update()
	{
		if (sellPanel.activeSelf)
		{
			HandleSlotSelection();
		}
	}

	protected override void SelectSlot(int index)
	{
		slots[index].isSelected = true != false;
		slotUIs[index].SelectSlotAtSell(!slots[index].isSelected);
	}

	public void SellPanelOpen()
	{
		if (!sellPanel.activeSelf)
		{
			sellPanel.SetActive(true);
			UIManager.Instance.ShowMoney();
			UpdateAllSlotUIs();
			CursorOnSlot(cursorOnSlotIndex);
			foreach (SlotUI slotUI in slotUIs)
			{
				slotUI.cursorImage.gameObject.SetActive(false);
			}
		}
	}

	public void SellPanelClose()
	{
		if (sellPanel.activeSelf)
		{
			UIManager.Instance.HideMoney();
			sellPanel.SetActive(false);
		}
	}
}
