using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
	public Inventory inventory; // Reference to the Inventory
	public GameObject inventoryPanel;   // Reference to the inventory UI Panel

	// UI�� ���� ���� slot��?
	public List<SlotUI> slotUIs = new List<SlotUI>();   // List of SlotUI components
	private int cursorOnSlotIndex = 0;  // Index of the currently selected slot
	private const int slotsPerRow = 10; // Number of slots per row
	private const int totalRows = 2;    // Total number of rows

	private void Start()
	{
		inventory = FindObjectOfType<Inventory>();
		inventory.OnInventoryFull += InventoryOpen; // Subscribe to the event
		inventoryPanel.SetActive(false);    // Hide the inventoryPanel at start
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (inventoryPanel.activeSelf)
			{
				inventoryPanel.SetActive(false);
			}
		}

		// Handle slot selection with keyboard input
		if (inventoryPanel.activeSelf)
		{
			HandleSlotSelection();
		}
	}

	public void InventoryOpen()
	{
		inventoryPanel.SetActive(true); // Open the inventory UI

		// key input and cursor move

		// if choose, delegate call

		// but if full, call another delegate

		UpdateAllSlotUIs(); // Update all slots when opening the inventory
		CursorOnSlot(cursorOnSlotIndex);  // Select the first slot by default
		foreach (SlotUI slotUI in slotUIs)
		{
			slotUI.cursorImage.gameObject.SetActive(false);
		}
	}

	// inventory�� SlotPrefab�� Instantiate�Ҷ� SlotPrefab�� ������ SlotUI�� �޾ƿ��� ���� �Լ�
	public void AddSlotUI(SlotUI slotUI)
	{
		slotUIs.Add(slotUI);
	}

	// Inventory���Լ� Inventory�� ��� Slots�� ��� item�� �������� �޾ƿ��� ���� �Լ�
	private void UpdateAllSlotUIs()
	{
		// �κ��丮�� Slot�� �������� ���� �Ҵ�޾Ƽ� �޾ƿ�
		// �� slots�� �����ص� Inventory�� slot�� ��� �������� �ٲ����� ����
		List<Slot> slots = inventory.GetSlotInfo();
		// �κ��丮�� Slot�� ������ ���� slotUIs�� ������Ʈ
		for (int i = 0; i < slots.Count; i++)
		{
			slotUIs[i].UpdateUI(slots[i].item, slots[i].stackCount);
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
		}

		// Cursor on the current slot
		slotUIs[index].CursorOnSlotDisplayBackground(true);
	}

	private void SelectSlot(int index)
	{
		// Select the current slot
		slotUIs[index].SelectSlot(true);
	}
}
