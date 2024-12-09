using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	public Image cursorImage;

	private void Start()
	{
		// Initialize lists and set default state
		purchasePanel.SetActive(false);
		slotUIs = new List<SlotUI>();
	}

	private void Update()
	{
		if (purchasePanel.activeSelf && isInitialized && isSelecting == false)
		{
			HandleSlotSelection();
		}
	}

	// Initialize slots and update UI once
	public void InitializeShopSlots(List<Slot> shopSlots)
	{
		// Store reference to shop slots
		slots = new List<Slot>(shopSlots);

		// Update UI components
		for (int i = 0; i < slots.Count; i++)
		{
			if (slots[i] != null && slots[i].Item != null && slotUIs[i] != null)
			{
				slotUIs[i].UpdateUI(slots[i].Item, slots[i].stackCount);
			}
		}

		isInitialized = true;
	}

	public void AddSlotUI(SlotUI slotUI)
	{
		slotUIs.Add(slotUI);
	}

	public void PurchasePanelOpen()
	{
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
		UIManager.Instance.ShowMoney();
		CursorOnSlot(cursorOnSlotIndex);  // Select the first slot by default
	}

	public void PurchasePanelClose()
	{
		if (purchasePanel.activeSelf)
		{
			UIManager.Instance.HideMoney();
			purchasePanel.SetActive(false);
		}
	}

	private void HandleSlotSelection()
	{
		int previousSlotIndex = cursorOnSlotIndex;  // Store the previous index

		if (Input.GetKeyDown(KeyCode.W))
		{
			if (cursorOnSlotIndex >= slotsPerRow)
			{
				// Move cursor up
				cursorOnSlotIndex -= slotsPerRow;   // Move up by one row
			}
			else if (cursorOnSlotIndex < 0)
			{
				cursorOnSlotIndex = slotsPerRow * totalRows - 1;  // 3
			}
		}
		else if (Input.GetKeyDown(KeyCode.S))
		{
			if (cursorOnSlotIndex < slotsPerRow * (totalRows - 1) && cursorOnSlotIndex >= 0)
			{
				// Move cursor down
				cursorOnSlotIndex += slotsPerRow;   // Move down by one row
			}
			else if (cursorOnSlotIndex >= slotsPerRow * (totalRows - 1))
			{
				cursorOnSlotIndex = -1;
			}
		}
		else if (Input.GetKeyDown(KeyCode.A) && cursorOnSlotIndex % slotsPerRow > 0)
		{
			if (cursorOnSlotIndex >= 0)
			{
				// Move cursor left
				cursorOnSlotIndex--;    // Move left by one slot
			}
		}
		else if (Input.GetKeyDown(KeyCode.D) && cursorOnSlotIndex % slotsPerRow < slotsPerRow - 1)
		{
			if (cursorOnSlotIndex >= 0)
			{
				// Move cursor right
				cursorOnSlotIndex++;    // Move right by one slot
			}
		}
		else if (Input.GetKeyDown(KeyCode.Return))
		{
			if (cursorOnSlotIndex >= 0)
			{
				SelectSlot(cursorOnSlotIndex);
			}
			else
			{
				UIManager.Instance.ClosePurchasePanel();
			}
		}

		// Only update the selection if it has changed
		if (previousSlotIndex != cursorOnSlotIndex)
		{
			if (cursorOnSlotIndex >= 0)
			{
				CursorOnSlot(cursorOnSlotIndex);
			}
			else
			{
				CursorOnQuit();
			}
		}
	}

	private void CursorOnQuit()
	{
		// Reset all slots
		foreach (SlotUI slotUI in slotUIs)
		{
			slotUI.CursorOnSlotDisplayBackground(false);
			slotUI.CursorOnSlotDisplayName(false);
			slotUI.CursorOnSlotDisplayCursor(false);
		}

		cursorImage.gameObject.SetActive(true);
	}

	private void CursorOnSlot(int index)
	{
		cursorImage.gameObject.SetActive(false);

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
