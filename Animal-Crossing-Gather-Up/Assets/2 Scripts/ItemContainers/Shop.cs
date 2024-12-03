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
	[Header("Shop Items")]
	private List<Slot> purchaseSlots = new List<Slot>();
	public List<Item> items = new List<Item>();

	[Header("References")]
	public GameObject[] horizontalLayoutObjects;
	public GameObject slotPrefab;   // Prefab for the slot
	private PurchaseUI purchaseUI;
	private const int slotsPerRow = 2; // Number of slots per row
	private bool isInitialized = false;

	private void Start()
	{
		// Check UIManager initialization
		StartCoroutine(InitializeShop());
	}

	private IEnumerator InitializeShop()
	{
		// Wait for UIManager initialization
		while (UIManager.Instance == null || UIManager.Instance.purchaseUI == null)
		{
			yield return null;
		}

		// Set shop items to PurchaseUI
		purchaseUI = UIManager.Instance.purchaseUI;

		// Create slots and Initialize slots if UI is ready
		for (int i = 0; i < items.Count; i++)
		{
			AddSlot(i < slotsPerRow ? 0 : 1); // Add empty slots based on index
		}

		// Wait one frame to ensure all Ui components are properly initialized
		yield return null;

		// Initialize slots with items
		for (int i = 0; i < items.Count; i++)
		{
			purchaseSlots[i].Initialize(items[i]);  // slot initialize
		}

		// Pass the initialized slots to PurchaseUI
		purchaseUI.InitializeShopSlots(purchaseSlots);
		isInitialized = true;
	}

	private void Update()
	{
		if (isInitialized == false)
		{
			return;
		}

		if (Input.GetKeyDown(KeyCode.J))
		{
			PurchasePanelOpen();
		}
		else if (Input.GetKeyDown(KeyCode.Escape))
		{
			PurchasePanelClose();
		}
	}

	private void AddSlot(int horizontalCount)
	{
		// Create slot object and initialize it
		GameObject slotObject = Instantiate(slotPrefab, horizontalLayoutObjects[horizontalCount].transform); // Instantiate the slotPrefab
		Slot slot = slotObject.GetComponent<Slot>();    // Get the slot component
		SlotUI slotUI = slotObject.GetComponent<SlotUI>();

		// Ensure SlotUI is properly initialized before adding
		if (slotUI != null)
		{
			purchaseUI.AddSlotUI(slotObject.GetComponent<SlotUI>());    // Add SlotUI to PurchaseUI
		}

		purchaseSlots.Add(slot);
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
