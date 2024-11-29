using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
	public Inventory inventory; // Reference to the Inventory
	public GameObject inventoryPanel;   // Reference to the inventory UI Panel

	// UI에 띄우기 위한 slot들?
	public List<SlotUI> slotUIs = new List<SlotUI>();   // List of SlotUI components
	private int selectedSlotIndex = 0;  // Index of the currently selected slot
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
		HandleSlotSelection();
	}

	public void InventoryOpen()
	{
		inventoryPanel.SetActive(true); // Open the inventory UI

		// key input and cursor move

		// if choose, delegate call

		// but if full, call another delegate

		UpdateAllSlotsUI(); // Update all slots when opening the inventory
		SelectSlot(selectedSlotIndex);  // Select the first slot by default
	}

	// inventory가 SlotPrefab을 Instantiate할때 SlotPrefab에 부착된 SlotUI을 받아오기 위한 함수
	public void AddSlotUI(SlotUI slotUI)
	{
		slotUIs.Add(slotUI);
	}

	// Inventory에게서 Inventory에 담긴 Slots에 담긴 item의 정보들을 받아오기 위한 함수
	private void UpdateAllSlotsUI()
	{
		// 인벤토리의 Slot의 정보들을 새로 할당받아서 받아옴
		// 이 slots를 변경해도 Inventory의 slot에 담긴 아이템이 바뀌지는 않음
		List<Slot> slots = inventory.GetSlotInfo();
		// 인벤토리의 Slot의 정보을 토대로 slotUIs를 업데이트
		for (int i = 0; i < slots.Count; i++)
		{
			slotUIs[i].UpdateUI(slots[i].item, slots[i].stackCount);
		}
	}

	private void HandleSlotSelection()
	{
		int previousSlotIndex = selectedSlotIndex;  // Store the previous index

		if (Input.GetKeyDown(KeyCode.W) && selectedSlotIndex >= slotsPerRow)
		{
			// Move selection up
			selectedSlotIndex -= slotsPerRow;   // Move up by one row
		}
		else if (Input.GetKeyDown(KeyCode.S) && selectedSlotIndex < slotsPerRow * (totalRows - 1))
		{
			// Move selection down
			selectedSlotIndex += slotsPerRow;   // Move down by one row
		}
		else if (Input.GetKeyDown(KeyCode.A) && selectedSlotIndex % slotsPerRow > 0)
		{
			// Move selection left
			selectedSlotIndex--;    // Move left by one slot
		}
		else if (Input.GetKeyDown(KeyCode.D) && selectedSlotIndex % slotsPerRow < slotsPerRow - 1)
		{
			// Move selection right
			selectedSlotIndex++;    // Move right by one slot
		}

		// Only update the selection if it has changed
		if (previousSlotIndex != selectedSlotIndex)
		{
			SelectSlot(selectedSlotIndex);
		}
	}

	private void SelectSlot(int index)
	{
		// Deselect all slots
		foreach (var slotUI in slotUIs)
		{
			slotUI.SelectSlot(false);
		}

		// Select the current slot
		slotUIs[index].SelectSlot(true);
	}
}
