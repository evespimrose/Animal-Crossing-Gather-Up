using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
	// 판매
	// 키보드 입력으로 커서 이동

	// 키보드 입력으로 커서 위치의 아이템 선택

	// 선택지 창 오픈

	// 커서 위치의 아이템 이름 표시

	// 구매

	// 도끼, 잠자리채, 낚싯대, 마일섬 티켓

	// SlotUI는 할당된 아이템에 대한 정보 표시만 가능하도록 하면 좋겠는데,.

	// 리스트 여기서 만들어서 so 여기 할당하고 가져오는 것 구현해야함
	private List<Slot> purchaseSlots = new List<Slot>(4);
	public List<Item> items = new List<Item>();

	// 구매
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
