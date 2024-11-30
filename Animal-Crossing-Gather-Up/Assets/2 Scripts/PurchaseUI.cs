using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseUI : MonoBehaviour
{
	public GameObject purchasePanel;
	public List<SlotUI> slotUIs = new List<SlotUI>(); // List of SlotUI components

	private int cursorOnSlotIndex = 0;  // Index of the currently selected slot
	private const int slotsPerRow = 4; // Number of slots per row
	private const int totalRows = 2;    // Total number of rows

	private void Start()
	{
		purchasePanel.SetActive(false);
	}

	private void Update()
	{
		if (purchasePanel.activeSelf)
		{
			HandleSlotSelection();
		}
	}

	private void UpdateAllSlotUIs()
	{
		for (int i = 0; i < slotUIs.Count; i++)
		{
			//slotUIs[i].UpdateUI(slot)
		}
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
		if (slotUIs[index].itemNameText.text != "")
		{
			slotUIs[index].CursorOnSlotDisplayName(true);
		}
	}
}
