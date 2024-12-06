using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
	[Header("References")]
	public Inventory inventory; // Reference to the Inventory
	[SerializeField]
	private GameObject inventoryPanel;   // Reference to the inventory UI Panel

	[Header("UI Elements")]
	// UI�� ���� ���� slot��?
	public List<SlotUI> slotUIs = new List<SlotUI>();   // List of SlotUI components
	private List<Slot> slots;
	private int cursorOnSlotIndex = 0;  // Index of the currently selected slot
	private const int slotsPerRow = 10; // Number of slots per row
	private const int totalRows = 2;    // Total number of rows

	private bool isSelecting = false;
	private string selectedOption = "";

	public delegate void SlotChooseHandler();
	public event SlotChooseHandler OnSlotChoose;

	private void Start()
	{
		// Find component
		inventory = FindObjectOfType<Inventory>();
		inventory.OnInventoryFull += OnInventoryFull; // Subscribe to the event

		if (inventoryPanel != null)
		{
			inventoryPanel.SetActive(false);    // Hide the inventoryPanel at start
		}
	}

	private void Update()
	{
		// Handle slot selection with keyboard input
		if (inventoryPanel.activeSelf && isSelecting == false)
		{
			HandleSlotSelection();
		}
	}

	private void OnInventoryFull()
	{
		UIManager.Instance.OpenInventory();
	}

	public void InventoryOpen()
	{
		inventoryPanel.SetActive(true); // Open the inventory UI

		// key input and cursor move

		// if choose, delegate call

		// but if full, call another delegate

		UpdateAllSlotUIs(); // Update all slots when opening the inventory
		CursorOnSlot(cursorOnSlotIndex);  // Select the first slot by default
		foreach (SlotUI slotUI in slotUIs)
		{
			slotUI.cursorImage.gameObject.SetActive(false);
		}
	}

	public void InventoryClose()
	{
		inventoryPanel.SetActive(false);
	}

	// inventory�� SlotPrefab�� Instantiate�Ҷ� SlotPrefab�� ������ SlotUI�� �޾ƿ��� ���� �Լ�
	public void AddSlotUI(SlotUI slotUI)
	{
		slotUIs.Add(slotUI);
	}

	// Inventory���Լ� Inventory�� ��� Slots�� ��� item�� �������� �޾ƿ��� ���� �Լ�
	public void UpdateAllSlotUIs()
	{
		// �κ��丮�� Slot�� �������� ���� �Ҵ�޾Ƽ� �޾ƿ�
		// �� slots�� �����ص� Inventory�� slot�� ��� �������� �ٲ����� ����
		slots = inventory.GetSlotInfo();
		// �κ��丮�� Slot�� ������ ���� slotUIs�� ������Ʈ
		for (int i = 0; i < slots.Count; i++)
		{
			slotUIs[i].UpdateUI(slots[i].Item, slots[i].stackCount);
		}
	}

	private void HandleSlotSelection()
	{
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
			// Select CursorOnSlot
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
		// Decursor on all slots
		foreach (SlotUI slotUI in slotUIs)
		{
			slotUI.CursorOnSlotDisplayBackground(false);
			slotUI.CursorOnSlotDisplayName(false);
		}

		// Cursor on the current slot
		slotUIs[index].CursorOnSlotDisplayBackground(true);
		if (slots[index].Item != null)
		{
			slotUIs[index].CursorOnSlotDisplayName(true);
		}
	}

	private void SelectSlot(int index)
	{
		// Select the current slot
		if (slots[index].Item != null)
		{
			isSelecting = true;
			slotUIs[index].SelectSlotAtInventory(true);
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

	public int GetSelectedOptionSlotIndex()
	{
		return cursorOnSlotIndex;
	}
}
