using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purchase : MonoBehaviour
{
	[Header("Shop Items")]
	private List<Slot> purchaseSlots = new List<Slot>();
	public List<Item> items = new List<Item>();

	[Header("References")]
	public GameObject[] horizontalLayoutObjects;
	public GameObject slotPrefab;   // Prefab for the slot
	private PurchaseUI purchaseUI;
	private const int slotsPerRow = 2; // Number of slots per row

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

		// Wait for PurchaseUI to be fully initialized
		yield return new WaitForSeconds(0.1f);

		// Create all slots first
		for (int i = 0; i < items.Count; i++)
		{
			AddSlot(i < slotsPerRow ? 0 : 1); // Add empty slots based on index
		}

		// Wait for slot creation to complete
		yield return new WaitForEndOfFrame();

		// Initialize slots with items
		for (int i = 0; i < items.Count && i < purchaseSlots.Count; i++)
		{
			if (items[i] != null && purchaseSlots[i] != null)
			{
				// Create a new instance of the item for each slot
				Item newItem = Instantiate(items[i]);
				purchaseSlots[i].Initialize(newItem); // Slot initialize
			}
		}

		// Initialize PurchaseUI with the slots
		purchaseUI.InitializeShopSlots(purchaseSlots);
	}

	private void AddSlot(int horizontalCount)
	{
		// Create slot object and initialize it
		GameObject slotObject = Instantiate(slotPrefab, horizontalLayoutObjects[horizontalCount].transform); // Instantiate the slotPrefab
		Slot slot = slotObject.GetComponent<Slot>();    // Get the slot component
		SlotUI slotUI = slotObject.GetComponent<SlotUI>();
		slotUI.isShopSlot = true; // display isShopSlots

		// Add the slot UI to PurchaseUI
		purchaseUI.AddSlotUI(slotUI);    // Add SlotUI to PurchaseUI
		purchaseSlots.Add(slot);
	}
}
