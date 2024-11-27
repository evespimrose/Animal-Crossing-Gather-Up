using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	// This can be extended later
	private List<Slot> slots;

	private void Start()
	{
		slots = new List<Slot>(20);
		for (int i = 0; i < 20; i++)
		{
			// add empty slot
			slots.Add(new Slot(null));
		}
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
		for (int i = 0; i < slots.Count; i++)
		{
			// 1. check same item
			if (slots[i].IsSlotEmpty())
			{
				// Create new slot with item
				slots[i] = new Slot(item);
				print($"Add {item} to new slot[{i}]. stackCount : {slots[i].stackCount}");
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