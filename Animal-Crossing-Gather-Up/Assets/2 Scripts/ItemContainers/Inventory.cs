using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public GameObject slotPrefab;   // Prefab for the slot

	// This can be extended later
	private List<Slot> slots;   // List of slots

	public GameObject[] horizontalLayoutObjects;
	private bool isInitialized = false;

	public delegate void InventoryFullHandler();
	public event InventoryFullHandler OnInventoryFull;  // Event for inventory full

	private void Start()
	{
		StartCoroutine(InitializeInventory());
	}

	private IEnumerator InitializeInventory()
	{
		// Waiting until UIManager is ready
		while (UIManager.Instance == null || UIManager.Instance.inventoryUI == null)
		{
			yield return null;
		}

		slots = new List<Slot>(20); // Initialize slots list

		// Initialize slot if UI is ready
		for (int i = 0; i < 20; i++)
		{
			AddSlot(i < 10 ? 0 : 1);    // Add empty slots based on index
		}

		isInitialized = true;
	}

	private void AddSlot(int horizontalCount)
	{
		GameObject slotObject = Instantiate(slotPrefab, horizontalLayoutObjects[horizontalCount].transform); // Instantiate the slot prefab
		Slot slot = slotObject.GetComponent<Slot>();  // Get the Slot component
		slots.Add(slot);

		// inventoryUI���� SlotPrefab �� ������ SlotUI�� �˷���
		UIManager.Instance.inventoryUI.AddSlotUI(slotObject.GetComponent<SlotUI>());   // Add SlotUI to InventoryUI
	}

	// item add logic
	public void AddItem(Item item = null)
	{
		if (isInitialized == false)
		{
			return;
		}

		bool isAdded = false;

		// Check for existing item in slots
		foreach (Slot slot in slots)
		{
			if (slot.IsAddableItem(item))
			{
				slot.AddItem();
				// test
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

	// private slots�� ������ �ޱ� ���� ���� �Ҵ�޾Ƽ� �������༭ ��ȯ��
	// ��ȯ�� slots���� �����ص� inventory�� slots�� ��� item�� �����Ͱ� �ٲ��� ����
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