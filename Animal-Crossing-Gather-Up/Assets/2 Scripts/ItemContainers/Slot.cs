using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
	public Item Item { get; private set; }
	public int stackCount;

	public void Initialize(Item newItem)
	{
		Item = newItem; // Set the item
		stackCount = 1; // Initialize stackCount
	}

	// addable return method(stackLimit, same item exist)
	public bool IsAddableItem(Item newItem)
	{
		return Item == newItem && stackCount < Item.stackLimit;
	}

	// add item at current slot
	public void AddItem()
	{
		stackCount++;
	}

	public void RemoveItemOne()
	{
		stackCount--;
		print($"Remove {Item.itemName}. Current Stack Count : {stackCount}");
		if (stackCount == 0)
		{
			Item = null;
			print("Item is null");
		}
	}

	public void RemoveItemAll()
	{
		Item = null;
		stackCount = 0;
	}

	// return isEmpty
	public bool IsSlotEmpty()
	{
		return Item == null;
	}
}
