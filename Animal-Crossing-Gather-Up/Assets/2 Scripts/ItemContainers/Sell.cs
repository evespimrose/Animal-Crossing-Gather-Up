using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sell : Inventory
{
	protected override void AddSlot(int horizontalCount)
	{
		GameObject slotObject = Instantiate(slotPrefab, horizontalLayoutObjects[horizontalCount].transform); // Instantiate the slot prefab
		Slot slot = slotObject.GetComponent<Slot>();  // Get the Slot component
		slots.Add(slot);

		UIManager.Instance.sellUI.AddSlotUI(slotObject.GetComponent<SlotUI>()); // Add SlotUI to SellUI
	}
}
