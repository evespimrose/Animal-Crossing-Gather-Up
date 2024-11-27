using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
	public Item item;
	public int stackCount;

	// when "new Slot(item)"
	public void Initialize(Item newItem)
	{
		item = newItem; // Set the item
		stackCount = 1; // Initialize stackCount
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
