using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellUI : MonoBehaviour
{
	public GameObject sellPanel;
	private List<SlotUI> slotUIs = new List<SlotUI>();
	private int cursorOnSlotIndex = 0;
	private const int slotsPerRow = 10; // Number of slots per row
	private const int totalRows = 2;    // Total number of rows
	private Sell sell;

	public Image cursorImage;

	private void Start()
	{
		sell = FindObjectOfType<Sell>();
		sellPanel.SetActive(false);
		cursorImage.gameObject.SetActive(false);
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
		if (sell == null || slotUIs.Count == 0)
		{
			return;
		}

		List<Slot> slots = sell.GetSlots();
		if (slots == null || slots.Count == 0)
		{
			return;
		}

		int previousSlotIndex = cursorOnSlotIndex;  // Store the previous index

		if (Input.GetKeyDown(KeyCode.W))
		{
			if (cursorOnSlotIndex >= slotsPerRow)
			{
				// Move cursor up
				cursorOnSlotIndex -= slotsPerRow;   // Move up by one row
			}
			else if (cursorOnSlotIndex == -1)
			{
				cursorOnSlotIndex = slotsPerRow * totalRows - 1;  // 19
			}
		}
		else if (Input.GetKeyDown(KeyCode.S))
		{
			if (cursorOnSlotIndex < slotsPerRow * (totalRows - 1) && cursorOnSlotIndex >= 0)
			{
				// Move cursor down
				cursorOnSlotIndex += slotsPerRow;   // Move down by one row
			}
			else if (cursorOnSlotIndex >= slotsPerRow * (totalRows - 1))
			{
				cursorOnSlotIndex = -1;
			}
		}
		else if (Input.GetKeyDown(KeyCode.A) && cursorOnSlotIndex % slotsPerRow > 0)
		{
			// Move cursor left
			cursorOnSlotIndex--;    // Move left by one slot
		}
		else if (Input.GetKeyDown(KeyCode.D) && cursorOnSlotIndex % slotsPerRow < slotsPerRow - 1 && cursorOnSlotIndex >= 0)
		{
			// Move cursor right
			cursorOnSlotIndex++;    // Move right by one slot
		}
		else if (Input.GetKeyDown(KeyCode.Return))
		{
			if (cursorOnSlotIndex >= 0)
			{
				// Select CursorOnSlot
				SelectSlot(cursorOnSlotIndex);
			}
			else
			{
				SelectConfirm();
			}
		}

		// Only update the selection if it has changed
		if (previousSlotIndex != cursorOnSlotIndex)
		{
			if (cursorOnSlotIndex >= 0)
			{
				CursorOnSlot(cursorOnSlotIndex);
			}
			else
			{
				CursorOnConfirm();
			}
		}
	}

	private void SelectConfirm()
	{
		print("Selected confirm panel");
	}

	private void CursorOnConfirm()
	{
		List<Slot> slots = sell.GetSlots();
		// Reset all slots
		foreach (SlotUI slotUI in slotUIs)
		{
			slotUI.CursorOnSlotDisplayBackground(false);
			slotUI.CursorOnSlotDisplayName(false);
			slotUI.cursorImage.gameObject.SetActive(false);
		}

		cursorImage.gameObject.SetActive(true);
		print("Cursor is on confirm panel");
	}

	private void CursorOnSlot(int index)
	{
		cursorImage.gameObject.SetActive(false);
		List<Slot> slots = sell.GetSlots();
		// Reset all slots
		foreach (SlotUI slotUI in slotUIs)
		{
			slotUI.CursorOnSlotDisplayBackground(false);
			slotUI.CursorOnSlotDisplayName(false);
			slotUI.cursorImage.gameObject.SetActive(false);
		}

		// Update current slot
		SlotUI currentSlotUI = slotUIs[index];
		currentSlotUI.CursorOnSlotDisplayBackground(true);
		currentSlotUI.cursorImage.gameObject.SetActive(true);

		if (slots[index].Item != null)
		{
			currentSlotUI.CursorOnSlotDisplayName(true);
		}
	}

	private void SelectSlot(int index)
	{
		List<Slot> slots = sell.GetSlots();
		slots[index].isSelected = !slots[index].isSelected;
		slotUIs[index].SelectSlotAtSell(slots[index].isSelected);
	}

	public void AddSlotUI(SlotUI slotUI)
	{
		slotUIs.Add(slotUI);
	}

	public void UpdateUI()
	{
		if (sell == null || slotUIs == null)
		{
			return;
		}

		List<Slot> slots = sell.GetSlots();
		if (slots != null)
		{
			for (int i = 0; i < slots.Count && i < slotUIs.Count; i++)
			{
				SlotUI slotUI = slotUIs[i];
				Slot slot = slots[i];

				// Update item and count
				slotUIs[i].UpdateUI(slots[i].Item, slots[i].stackCount);

				// Update UI states
				if (slot.Item == null)
				{
					slotUI.itemImage.gameObject.SetActive(false);
					slotUI.choiceBackground.gameObject.SetActive(false);
					slotUI.multiChoiceBackground.gameObject.SetActive(false);
					slotUI.itemInfo.SetActive(false);
				}
				else
				{
					slotUI.itemImage.gameObject.SetActive(true);
				}
			}

			// Update cursor position
			if (slotUIs.Count > 0)
			{
				CursorOnSlot(cursorOnSlotIndex);
			}
		}
	}

	public void SellPanelOpen()
	{
		if (!sellPanel.activeSelf)
		{
			sellPanel.SetActive(true);
			UIManager.Instance.ShowMoney();
			cursorOnSlotIndex = 0;

			if (sell != null)
			{
				sell.UpdateFromInventory();
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

		// Reset all UI states
		foreach (SlotUI slotUI in slotUIs)
		{
			slotUI.cursorImage.gameObject.SetActive(false);
			slotUI.choiceBackground.gameObject.SetActive(false);
			slotUI.multiChoiceBackground.gameObject.SetActive(false);
			slotUI.itemInfo.SetActive(false);
		}
	}
}
