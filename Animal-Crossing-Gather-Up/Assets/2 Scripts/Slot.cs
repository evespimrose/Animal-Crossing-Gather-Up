using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
	public Item item;
	public int stackCount;

	// when "new Slot(item)"
	public Slot(Item item)
	{
		this.item = item;
		this.stackCount = 1;
	}

	// addable return method
	public bool IsAddableItem(Item newItem)
	{
		return item == newItem && stackCount < item.stackLimit;
	}

	// add item at current slot
	public void AddItem()
	{
		stackCount++;
	}
}
