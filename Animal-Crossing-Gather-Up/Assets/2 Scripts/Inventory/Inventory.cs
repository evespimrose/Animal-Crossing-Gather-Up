using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

	public InventoryUI inventoryUI;

	public GameObject slotPrefab;   // Prefab for the slot

	// This can be extended later
	private List<Slot> slots;   // List of slots

	public GameObject[] verticalLayout;

	public delegate void InventoryFullHandler();
	public event InventoryFullHandler OnInventoryFull;  // Event for inventory full

	private void Start()
	{
		inventoryUI = FindAnyObjectByType<InventoryUI>();

		slots = new List<Slot>(20); // Initialize slots list
		for (int i = 0; i < 20; i++)
		{
			AddSlot(i < 10 ? 0 : 1);    // Add empty slots based on index
		}
	}

	private void AddSlot(int verticalCount)
	{
		GameObject slotObject = Instantiate(slotPrefab, verticalLayout[verticalCount].transform); // Instantiate the slot prefab
		Slot slot = slotObject.GetComponent<Slot>();  // Get the Slot component
		slots.Add(slot);

		// inventoryUI에게 SlotPrefab 에 부착된 SlotUI를 알려줌
		inventoryUI.AddSlotUI(slotObject.GetComponent<SlotUI>());   // Add SlotUI to InventoryUI
	}

	// item add logic
	public void AddItem(Item item)
	{
		bool isAdded = false;

		// Check for existing item in slots
		foreach (Slot slot in slots)
		{
			if (slot.IsAddableItem(item))
			{
				slot.AddItem();
				// test
				print($"Add {item.itemName} to existing slot. New stackCount : {slot.stackCount}");
				isAdded = true;
				return;
			}
		}

		// Add item to an empty slot
		foreach (Slot slot in slots)
		{
			if (slot.IsSlotEmpty())
			{
				slot.Initialize(item);  // Initialize the slot with the item

				// test
				print($"Add {item.itemName} to empty slot. New stackCount : {slot.stackCount}");
				isAdded = true;
				return;
			}
		}

		// If inventory is full
		if (isAdded == false)
		{
			InventoryFull();
		}
	}

	// private slots의 정보만 받기 위해 새로 할당받아서 복사해줘서 반환함
	// 반환한 slots들을 변경해도 inventory의 slots에 담긴 item의 데이터가 바뀌지 않음
	public List<Slot> GetSlotInfo()
	{
		List<Slot> newSlots = new List<Slot>(slots);
		return newSlots;
	}

	// inventory full method (inventory popup recycle)
	private void InventoryFull()
	{
		print("Inventroy is Full!");

		// inventory full delegate call

		// if change delegate call

		// inventory open
		//InventoryDisplayer.Instance.InventoryOpen();
		OnInventoryFull?.Invoke();  // Trigger the event
	}
}