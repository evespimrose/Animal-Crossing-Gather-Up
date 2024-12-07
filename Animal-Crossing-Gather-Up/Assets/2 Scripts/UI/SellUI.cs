using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellUI : MonoBehaviour
{
	public Inventory inventory;
	public GameObject sellPanel;

	private List<Slot> slots;
	private List<SlotUI> slotUIs = new List<SlotUI>();
	private int cursorOnSlotIndex = 0;
	private const int slotsPerRow = 10; // Number of slots per row
	private const int totalRows = 2;    // Total number of rows

	private void Start()
	{
		inventory = FindObjectOfType<Inventory>();
		sellPanel.SetActive(false);
	}

	private void Update()
	{
		if (sellPanel.activeSelf)
		{
			HandleSlotSelection();
		}
	}

	private void HandleSlotSelection()
	{
		int previousSlotIndex = cursorOnSlotIndex;  // Store the previous index

		if (Input.GetKeyDown(KeyCode.W) && cursorOnSlotIndex >= slotsPerRow)
		{
			// Move cursor up
			cursorOnSlotIndex -= slotsPerRow;   // Move up by one row
		}
		else if (Input.GetKeyDown(KeyCode.S) && cursorOnSlotIndex < slotsPerRow * (totalRows - 1))
		{
			// Move cursor down
			cursorOnSlotIndex += slotsPerRow;   // Move down by one row
		}
		else if (Input.GetKeyDown(KeyCode.A) && cursorOnSlotIndex % slotsPerRow > 0)
		{
			// Move cursor left
			cursorOnSlotIndex--;    // Move left by one slot
		}
		else if (Input.GetKeyDown(KeyCode.D) && cursorOnSlotIndex % slotsPerRow < slotsPerRow - 1)
		{
			// Move cursor right
			cursorOnSlotIndex++;    // Move right by one slot
		}
		else if (Input.GetKeyDown(KeyCode.Return))
		{
			// Select CursorOnSlot
			SelectSlot(cursorOnSlotIndex);
		}

		// Only update the selection if it has changed
		if (previousSlotIndex != cursorOnSlotIndex)
		{
			CursorOnSlot(cursorOnSlotIndex);
		}
	}

	private void CursorOnSlot(int index)
	{
		// Decursor on all slots
		foreach (SlotUI slotUI in slotUIs)
		{
			slotUI.CursorOnSlotDisplayBackground(false);
			slotUI.CursorOnSlotDisplayName(false);
		}

		// Cursor on the current slot
		slotUIs[index].CursorOnSlotDisplayBackground(true);
		if (slots[index].Item != null)
		{
			slotUIs[index].CursorOnSlotDisplayName(true);
		}
	}

	private void SelectSlot(int index)
	{
		slots[index].isSelected = true != false;
		slotUIs[index].SelectSlotAtSell(slots[index].isSelected);
	}

	public void AddSlotUI(SlotUI slotUI)
	{
		slotUIs.Add(slotUI);
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

	public void UpdateAllSlotUIs()
	{
		// �κ��丮�� Slot�� �������� ���� �Ҵ�޾Ƽ� �޾ƿ�
		// �� slots�� �����ص� Inventory�� slot�� ��� �������� �ٲ����� ����
		slots = inventory.GetSlotInfo();
		// �κ��丮�� Slot�� ������ ���� slotUIs�� ������Ʈ
		for (int i = 0; i < slots.Count; i++)
		{
			slotUIs[i].UpdateUI(slots[i].Item, slots[i].stackCount);
		}
	}
}
