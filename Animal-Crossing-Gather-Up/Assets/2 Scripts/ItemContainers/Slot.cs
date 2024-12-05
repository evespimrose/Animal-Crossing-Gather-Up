using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
	public Item Item { get; private set; }
	public int stackCount;

	public void Initialize(Item newItem)
	{
		// If the item is a tool, create a deep copy
		if (newItem is ToolInfo toolInfo)
		{
			Item = Instantiate(toolInfo);
		}
		else
		{
			Item = newItem; // Set the item
		}
		stackCount = 1; // Initialize stackCount
	}

	// addable return method(stackLimit, same item exist)
	public bool IsAddableItem(Item newItem)
	{
		// Empty slot can always accept items
		if (Item == null)
		{
			return true;
		}

		// For non-tool items, check if they are the same instance and within stack limit
		if (ReferenceEquals(newItem, Item) && stackCount < Item.stackLimit)
		{
			return true;
		}

		return false;
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
