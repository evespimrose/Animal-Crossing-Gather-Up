using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
	// �Ǹ�
	// Ű���� �Է����� Ŀ�� �̵�

	// Ű���� �Է����� Ŀ�� ��ġ�� ������ ����

	// ������ â ����

	// Ŀ�� ��ġ�� ������ �̸� ǥ��

	// ����

	// ����, ���ڸ�ä, ���˴�, ���ϼ� Ƽ��

	// SlotUI�� �Ҵ�� �����ۿ� ���� ���� ǥ�ø� �����ϵ��� �ϸ� ���ڴµ�,.

	// ����Ʈ ���⼭ ���� so ���� �Ҵ��ϰ� �������� �� �����ؾ���
	private List<Slot> purchaseSlots = new List<Slot>(4);
	public List<Item> items = new List<Item>();

	// ����
	private PurchaseUI purchaseUI;

	public GameObject[] horizontalLayoutObjects;
	public GameObject slotPrefab;   // Prefab for the slot

	private const int slotsPerRow = 2; // Number of slots per row

	private void Start()
	{
		purchaseUI = FindObjectOfType<PurchaseUI>();
		for (int i = 0; i < items.Count; i++)
		{
			AddSlot(i < slotsPerRow ? 0 : 1); // Add empty slots based on index
			purchaseSlots[i].Initialize(items[i]);  // slot initialize
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.J))
		{
			PurchasePanelOpen();
		}
		else if (Input.GetKeyDown(KeyCode.Escape))
		{
			PurchasePanelClose();
		}
	}

	private void AddSlot(int verticalCount)
	{
		GameObject slotObject = Instantiate(slotPrefab, horizontalLayoutObjects[verticalCount].transform); // Instantiate the slotPrefab
		Slot slot = slotObject.GetComponent<Slot>();    // Get the slot component
		purchaseSlots.Add(slot);

		purchaseUI.AddSlotUI(slotObject.GetComponent<SlotUI>());    // Add SlotUI to PurchaseUI
	}

	public List<Slot> GetPurchaseSlotInfo()
	{
		List<Slot> newSlots = purchaseSlots;
		return newSlots;
	}

	public void PurchasePanelOpen()
	{
		purchaseUI.PurchasePanelOpen();
	}

	public void PurchasePanelClose()
	{
		purchaseUI.PurchasePanelClose();
	}
}
