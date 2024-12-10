using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public GameObject slotPrefab;   // Prefab for the slot

	// This can be extended later
	private List<Slot> slots;   // List of slots

	public GameObject[] horizontalLayoutObjects;

	public delegate void InventoryFullHandler();
	public event InventoryFullHandler OnInventoryFull;  // Event for inventory full

	private InventoryUI inventoryUI;
	private PurchaseUI purchaseUI;
	private int currentEquipIndex = -1;

	public int money = 1000;

	private Sell sell;

	private void Start()
	{
		inventoryUI = FindObjectOfType<InventoryUI>();
		purchaseUI = FindObjectOfType<PurchaseUI>();
		purchaseUI.OnSlotChoose += PurchaseSelectEnd;
		sell = FindAnyObjectByType<Sell>();
		FindObjectOfType<SellUI>().OnSell += SelectConfirm;
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
	}

	private void AddSlot(int horizontalCount)
	{
		GameObject slotObject = Instantiate(slotPrefab, horizontalLayoutObjects[horizontalCount].transform); // Instantiate the slot prefab
		Slot slot = slotObject.GetComponent<Slot>();  // Get the Slot component
		slots.Add(slot);

		// inventoryUI에게 SlotPrefab 에 부착된 SlotUI를 알려줌
		UIManager.Instance.inventoryUI.AddSlotUI(slotObject.GetComponent<SlotUI>());   // Add SlotUI to InventoryUI
	}

	// item add logic
	public void AddItem(Item item = null)
	{
		bool isAdded = false;

		// Check for existing item in slots
		foreach (Slot slot in slots)
		{
			if (slot.IsAddableItem(item))
			{
				if (slot.IsSlotEmpty())
				{
					slot.Initialize(item);
				}
				else
				{
					slot.AddItem();
				}
				isAdded = true;
				break;
			}
		}

		// If inventory is full
		if (isAdded == false)
		{
			InventoryFull();
		}
	}

	public void RemoveItemOne(int index)
	{
		if (slots[index].IsSlotEmpty() == false)
		{
			slots[index].RemoveItemOne();
		}
	}

	// private slots의 정보만 받기 위해 새로 할당받아서 복사해줘서 반환함
	// 반환한 slots들을 변경해도 inventory의 slots에 담긴 item의 데이터가 바뀌지 않음
	public List<Slot> GetSlotInfo()
	{
		List<Slot> newSlots = new List<Slot>(slots);
		return newSlots;
	}

	// inventory full method (inventory popup recycle)
	private void InventoryFull()
	{
		OnInventoryFull?.Invoke();  // Trigger the event
	}

	public void InventorySelectEnd()
	{
		// option Text, index print
		string optionText = inventoryUI.GetSelectedOptionText();
		int index = inventoryUI.GetSelectedOptionSlotIndex();

		if (optionText == "들기")
		{
			EquipTool(index);
			UIManager.Instance.CloseInventory();
		}
		else if (optionText == "근처에 두기")
		{
			RemoveItemAll(index);
			UIManager.Instance.CloseInventory();
		}
		else if (optionText == "가방에 넣기")
		{
			UnEquipTool(index);
			UIManager.Instance.CloseInventory();
		}
	}

	public void PurchaseSelectEnd()
	{
		string optionText = purchaseUI.GetSelectedOptionText();
		Slot slot = purchaseUI.GetSelectedOptionSlot();

		if (optionText == "살래!")
		{
			// check money
			if (money >= slot.Item.basePrice)
			{
				money -= slot.Item.basePrice;
				AddItem(slot.Item);
			}
			else
			{
				print("Not Enough Money!");
			}
		}
	}

	private void EquipTool(int index)
	{
		if (currentEquipIndex != -1)
		{
			// UnEquipSlot
			UnEquipTool(currentEquipIndex);
		}
		if (slots[index].Item is ToolInfo toolInfo)
		{
			toolInfo.isEquipped = true;
			GameManager.Instance.player.EquipTool(toolInfo);
		}
		slots[index].Item.optionText[0] = "가방에 넣기";
		currentEquipIndex = index;

		// Update SellUI to reflect equipped state
		FindObjectOfType<Sell>()?.UpdateFromInventory();
	}

	private void UnEquipTool(int index)
	{
		if (slots[index].Item is ToolInfo toolInfo)
		{
			toolInfo.isEquipped = false;
		}
		slots[index].Item.optionText[0] = "들기";
		currentEquipIndex = -1;

		// Update SellUI to reflect unequipped state
		FindObjectOfType<Sell>()?.UpdateFromInventory();
	}

	public void UpdateToolDurability(ToolInfo tool)
	{
		if (slots[currentEquipIndex].Item is ToolInfo toolInfo)
		{
			slots[currentEquipIndex].Item = tool;
			CheckToolDurability();
		}
	}

	private void CheckToolDurability()
	{
		if (slots[currentEquipIndex].Item is ToolInfo toolInfo)
		{
			if (toolInfo.currentDurability <= 0)
			{
				StartCoroutine(GameManager.Instance.player.UnequipAndDestroyTool());
				RemoveItemAll(currentEquipIndex);
			}
		}
	}

	private void RemoveItemAll(int index)
	{
		slots[index].RemoveItemAll();
	}

	public void SelectConfirm()
	{
		List<int> selectedSlotIndex = sell.GetSelectedSlotIndex();
		if (selectedSlotIndex.Count > 0)
		{
			for (int i = 0; i < selectedSlotIndex.Count; i++)
			{
				money += slots[selectedSlotIndex[i]].Item.basePrice * slots[selectedSlotIndex[i]].stackCount;
				RemoveItemAll(selectedSlotIndex[i]);
			}
			UIManager.Instance.CloseSellPanel();
		}
	}
}