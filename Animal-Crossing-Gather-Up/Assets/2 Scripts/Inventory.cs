using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	// This can be extended later
	private List<Slot> slots = new List<Slot>(20);
	//private bool isEmptySlotExist = true;

	// use at choice slot
	private Item currentItem;

	private void Start()
	{
		Player player = FindObjectOfType<Player>();
		if (player != null)
		{
			// event subscribe
			player.OnItemCollected += AddItem;
		}
	}

	// item add logic
	public void AddItem(Item item)
	{
		bool isAdded = false;

		// if same item exist, stackCount++
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

		// add empty slot
		for (int i = 0; i < slots.Count; i++)
		{
			// 1. check same item
			if (slots[i].IsSlotEmpty())
			{
				slots[i] = new Slot(item);
				print($"Add {item} to new slot[{i}]. stackCount : {slots[i].stackCount}");
				isAdded = true;
				return;
			}
		}

		// add failed
		if (isAdded == false)
		{
			print("Inventroy is Full!");
		}
	}

	private void InventoryOpen()
	{
		// hierarchy's popup enable true

		// key input and cursor move

		// if choose, delegate call

		// but if full, call another delegate
	}

	// item into method
	private void ItemIn()
	{
		// check slot and item Into or InventoryFull() call delegate
	}

	// inventory full method (inventory popup recycle)
	private void InventoryFull()
	{
		// inventory full delegate call

		// if change delegate call

		// inventory open
		InventoryOpen();
	}
}