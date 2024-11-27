using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public GameObject slotPrefab;   // Prefab for the slot

	// This can be extended later
	private List<Slot> slots;   // List of slots

	public GameObject[] verticalLayout;

	private void Start()
	{
		slots = new List<Slot>(20); // Initialize slots list
		for (int i = 0; i < 20; i++)
		{
			if (i < 10)
			{
				AddSlot(0);  // add empty slot
			}
			else if (i < 20)
			{
				AddSlot(1);
			}
		}
	}

	private void AddSlot(int verticalCount)
	{
		GameObject slotObject = Instantiate(slotPrefab, verticalLayout[verticalCount].transform); // Instantiate the slot prefab
		Slot slot = slotObject.GetComponent<Slot>();  // Get the Slot component
		slots.Add(slot);
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
				print($"Add {item} to existing slot. New stackCount : {slot.stackCount}");
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
				print($"Add {item} to empty slot. New stackCount : {slot.stackCount}");
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

	// inventory full method (inventory popup recycle)
	private void InventoryFull()
	{
		print("Inventroy is Full!");

		// inventory full delegate call

		// if change delegate call

		// inventory open
		InventoryDisplayer.Instance.InventoryOpen();
	}
}