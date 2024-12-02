using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseUI : MonoBehaviour
{
	public GameObject purchasePanel;
	public List<SlotUI> slotUIs; // List of SlotUI components
	private List<Slot> slots;
	public Shop shop;

	private int cursorOnSlotIndex = 0;  // Index of the currently selected slot
	private const int slotsPerRow = 2; // Number of slots per row
	private const int totalRows = 2;    // Total number of rows

	private void Start()
	{
		// Find and initialize shop reference
		shop = FindObjectOfType<Shop>();

		purchasePanel.SetActive(false);
		slots = shop.GetPurchaseSlotInfo();

		// update all slots
		for (int i = 0; i < slots.Count; i++)
		{
			slotUIs[i].UpdateUI(slots[i].Item, slots[i].stackCount);
		}
	}

	private void Update()
	{
		if (purchasePanel.activeSelf)
		{
			HandleSlotSelection();
		}
	}

	public void AddSlotUI(SlotUI slotUI)
	{
		slotUIs.Add(slotUI);
	}

	public void PurchasePanelOpen()
	{
		purchasePanel.SetActive(true);
		foreach (SlotUI slotUI in slotUIs)
		{
			slotUI.cursorImage.gameObject.SetActive(false);
		}
		CursorOnSlot(cursorOnSlotIndex);  // Select the first slot by default
	}

	public void PurchasePanelClose()
	{
		purchasePanel.SetActive(false);
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
			slotUI.CursorOnSlotDisplayCursor(false);
			slotUI.CursorOnSlotDisplayName(false);
		}

		// Cursor on the current slot
		slotUIs[index].CursorOnSlotDisplayCursor(true);
		if (slots[index].Item != null)
		{
			slotUIs[index].CursorOnSlotDisplayName(true);
		}
	}
}
