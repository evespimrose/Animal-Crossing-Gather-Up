using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
	public Inventory inventory; // Reference to the Inventory
	public GameObject inventoryPanel;   // Reference to the inventory UI Panel

	// UI�� ���� ���� slot��?
	public List<SlotUI> slotUIs = new List<SlotUI>();

	private void Start()
	{
		inventory = FindObjectOfType<Inventory>();
		inventory.OnInventoryFull += InventoryOpen; // Subscribe to the event
		inventoryPanel.SetActive(false);    // Hide the inventoryPanel at start
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (inventoryPanel.activeSelf)
			{
				inventoryPanel.SetActive(false);
			}
		}
	}

	public void InventoryOpen()
	{
		inventoryPanel.SetActive(true); // Open the inventory UI
										// key input and cursor move

		// if choose, delegate call

		// but if full, call another delegate

		UpdateAllSlotsUI(); // Update all slots when opening the inventory
	}

	// inventory�� SlotPrefab�� Instantiate�Ҷ� SlotPrefab�� ������ SlotUI�� �޾ƿ��� ���� �Լ�
	public void AddSlotUI(SlotUI slotUI)
	{
		slotUIs.Add(slotUI);
	}

	// Inventory���Լ� Inventory�� ��� Slots�� ��� item�� �������� �޾ƿ��� ���� �Լ�
	private void UpdateAllSlotsUI()
	{
		// �κ��丮�� Slot�� �������� ���� �Ҵ�޾Ƽ� �޾ƿ�
		// �� slots�� �����ص� Inventory�� slot�� ��� �������� �ٲ����� ����
		List<Slot> slots = inventory.GetSlotInfo();
		// �κ��丮�� Slot�� ������ ���� slotUIs�� ������Ʈ
		for (int i = 0; i < slots.Count; i++)
		{
			slotUIs[i].UpdateUI(slots[i].item, slots[i].stackCount);
		}
	}
}
