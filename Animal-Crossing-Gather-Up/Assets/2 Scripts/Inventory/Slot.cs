using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot
{
	public Item item;
	public int stackCount;

	// when "new Slot(item)"
	public Slot(Item item)
	{
		this.item = item;
		// if item != null, stackCount == 1
		this.stackCount = item != null ? 1 : 0;
	}

	// addable return method(stackLimit, same item exist)
	public bool IsAddableItem(Item newItem)
	{
		return item == newItem && stackCount < item.stackLimit;
	}

	// add item at current slot
	public void AddItem()
	{
		stackCount++;
	}

	// return isEmpty
	public bool IsSlotEmpty()
	{
		return item == null;
	}
}
