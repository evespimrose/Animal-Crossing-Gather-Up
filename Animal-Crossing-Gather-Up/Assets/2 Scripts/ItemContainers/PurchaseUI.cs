using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseUI : MonoBehaviour
{
	[Header("UI References")]
	public GameObject purchasePanel;
	public List<SlotUI> slotUIs; // List of SlotUI components

	[Header("Shop Data")]
	private List<Slot> slots;   // List of shop slots containing items
	private int cursorOnSlotIndex = 0;  // Index of the currently selected slot
	private const int slotsPerRow = 2; // Number of slots per row
	private const int totalRows = 2;    // Total number of rows
	private bool isInitialized = false;

	private bool isSelecting = false;
	private string selectedOption = "";

	public delegate void SlotChooseHandler();
	public event SlotChooseHandler OnSlotChoose;

	private void Start()
	{
		print("PurchaseUI: Starting initializaion");
		// Initialize lists and set default state
		purchasePanel.SetActive(false);
		slotUIs = new List<SlotUI>();
	}

	private void Update()
	{
		if (purchasePanel.activeSelf && isInitialized)
		{
			HandleSlotSelection();
		}
	}

	// Initialize slots and update UI once
	public void InitializeShopSlots(List<Slot> shopSlots)
	{
		if (shopSlots == null || shopSlots.Count == 0)
		{
			Debug.LogError("PurchaseUI: Received null or empty shop slots");
			return;
		}

		// Store reference to shop slots
		print($"PurchaseUI: Initializing with {shopSlots.Count} shop slots");
		slots = new List<Slot>(shopSlots);

		// Ensure we have both slots and UI components
		if (slotUIs.Count == 0)
		{
			Debug.LogError("PurchaseUI: No slot UIs available");
			return;
		}

		if (slotUIs.Count == 0)
		{
			Debug.LogError($"PurchaseUI: Not enough UI slots ({slotUIs.Count})");
			return;
		}

		// Update UI components
		for (int i = 0; i < slots.Count; i++)
		{
			if (slots[i] != null && slots[i].Item != null && slotUIs[i] != null)
			{
				slotUIs[i].UpdateUI(slots[i].Item, slots[i].stackCount);
				print($"PurchaseUI: Updated UI for slot {i} with item {slots[i].Item.itemName}");
			}
		}

		isInitialized = true;
		print("PurchaseUI: Initialization complete");
	}

	public void AddSlotUI(SlotUI slotUI)
	{
		if (slotUI == null)
		{
			Debug.LogError("PurchaseUI: Attempted to add null SlotUI");
			return;
		}

		slotUIs.Add(slotUI);
		print($"PurchaseUI: Added new SlotUI. Total count: {slotUIs.Count}");
	}

	public void PurchasePanelOpen()
	{
		if (isInitialized == false)
		{
			Debug.LogWarning("PurchaseUI: Cannot open panel before initializaion");
			return;
		}

		if (slotUIs == null || slotUIs.Count == 0)
		{
			Debug.LogError("PurchaseUI: No slot UIs available");
			return;
		}

		print("PurchaseUI: Opening purchase panel");
		purchasePanel.SetActive(true);

		// Reset cursor state for all slots
		foreach (SlotUI slotUI in slotUIs)
		{
			if (slotUI != null && slotUI.cursorImage != null)
			{
				slotUI.cursorImage.gameObject.SetActive(false);
			}
		}

		cursorOnSlotIndex = 0;
		CursorOnSlot(cursorOnSlotIndex);  // Select the first slot by default
	}

	public void PurchasePanelClose()
	{
		if (purchasePanel.activeSelf)
		{
			print("PurchaseUI: Closing purchase panel");
			purchasePanel.SetActive(false);
		}
	}

	private void HandleSlotSelection()
	{
		if (isInitialized == false)
		{
			return;
		}

		int previousSlotIndex = cursorOnSlotIndex;  // Store the previous index

		if (Input.GetKeyDown(KeyCode.W) && cursorOnSlotIndex >= slotsPerRow)
		{
			// Move cursor up
			cursorOnSlotIndex -= slotsPerRow;   // Move up by one row
		}
		else if (Input.GetKeyDown(KeyCode.S) && cursorOnSlotIndex < slotsPerRow * (totalRows - 1))
		{
			// Move cursor down
			cursorOnSlotIndex += slotsPerRow;   // Move down by one row
		}
		else if (Input.GetKeyDown(KeyCode.A) && cursorOnSlotIndex % slotsPerRow > 0)
		{
			// Move cursor left
			cursorOnSlotIndex--;    // Move left by one slot
		}
		else if (Input.GetKeyDown(KeyCode.D) && cursorOnSlotIndex % slotsPerRow < slotsPerRow - 1)
		{
			// Move cursor right
			cursorOnSlotIndex++;    // Move right by one slot
		}
		else if (Input.GetKeyDown(KeyCode.Return))
		{
			SelectSlot(cursorOnSlotIndex);
		}

		// Only update the selection if it has changed
		if (previousSlotIndex != cursorOnSlotIndex)
		{
			CursorOnSlot(cursorOnSlotIndex);
		}
	}

	private void CursorOnSlot(int index)
	{
		if (isInitialized == false || slotUIs == null || index >= slotUIs.Count)
		{
			Debug.LogWarning($"PurchaseUI: Invalid cursor operation. Initialized: {isInitialized}, Index: {index}, SlotUIs count: {(slotUIs != null ? slotUIs.Count : 0)}");
			return;
		}

		// Disable cursor and name display for all slots
		foreach (SlotUI slotUI in slotUIs)
		{
			if (slotUI != null)
			{
				slotUI.CursorOnSlotDisplayCursor(false);
				slotUI.CursorOnSlotDisplayName(false);
			}
		}

		// Enable cursor and name display for selected slot
		if (slotUIs[index] != null)
		{
			slotUIs[index].CursorOnSlotDisplayCursor(true);
			if (slots != null && slots.Count > index && slots[index]?.Item != null)
			{
				slotUIs[index].CursorOnSlotDisplayName(true);
			}
		}
	}

	private void SelectSlot(int index)
	{
		// Select the current slot
		if (slots[index].Item != null)
		{
			isSelecting = true;
			slotUIs[index].SelectSlotAtPurchase(true);
			StartCoroutine(WaitForSelectEndCoroutine(index));
		}
	}

	private IEnumerator WaitForSelectEndCoroutine(int index)
	{
		while (isSelecting)
		{
			selectedOption = slotUIs[index].GetSelectedOption();
			print(selectedOption);
			slotUIs[index].SetSelectedOptionInit();

			if (selectedOption == "")
			{
				yield return new WaitForEndOfFrame();
			}
			else
			{
				OnSlotChoose();
				isSelecting = false;
				selectedOption = "";
			}
		}
	}

	public string GetSelectedOptionText()
	{
		return selectedOption;
	}

	public Slot GetSelectedOptionSlot()
	{
		return slots[cursorOnSlotIndex];
	}
}
