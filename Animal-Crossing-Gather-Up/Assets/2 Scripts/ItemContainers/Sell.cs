using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sell : SingletonManager<Sell>
{
    [Header("References")]
    public GameObject[] horizontalLayoutObjects;
    public GameObject slotPrefab;
    private Inventory inventory;
    private List<Slot> slots = new List<Slot>();

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        // Start initialization coroutine
        StartCoroutine(InitializeSell());
    }

    private IEnumerator InitializeSell()
    {
        // Wait for UIManager iniitalization
        while (UIManager.Instance == null || UIManager.Instance.sellUI == null)
        {
            yield return null;
        }

        // Wait for inventory
        while (inventory == null)
        {
            inventory = FindObjectOfType<Inventory>();
            yield return null;
        }

        // Create slots and UI
        for (int i = 0; i < 20; i++)
        {
            CreateSlot(i < 10 ? 0 : 1);
        }

        // Initial update from inventory
        UpdateFromInventory();
    }

    private void CreateSlot(int horizontalCount)
    {
        if (horizontalLayoutObjects == null || horizontalCount >= horizontalLayoutObjects.Length)
        {
            Debug.LogError("Sell: Invalid horizontal layout objects");
            return;
        }

        // Create UI GameObject with SlotUI
        GameObject slotObject = Instantiate(slotPrefab, horizontalLayoutObjects[horizontalCount].transform); // Instantiate the slot prefab
        SlotUI slotUI = slotObject.GetComponent<SlotUI>();

        // Create slot for data
        Slot slot = slotObject.GetComponent<Slot>();  // Get the Slot component
        if (slot != null && slotUI != null)
        {
            slots.Add(slot);
            slotUI.InitializeSlot(slot);

            // Initialize UI states
            slotUI.itemImage.gameObject.SetActive(false);
            slotUI.cursorImage.gameObject.SetActive(false);
            slotUI.itemInfo.gameObject.SetActive(false);
            slotUI.choiceBackground.gameObject.SetActive(false);
            slotUI.multiChoiceBackground.gameObject.SetActive(false);

            UIManager.Instance.sellUI.AddSlotUI(slotUI); // Add SlotUI to SellUI
        }
    }

    public void UpdateFromInventory()
    {
        if (inventory == null || slots == null)
        {
            return;
        }

        List<Slot> inventorySlots = inventory.GetSlotInfo();

        if (inventorySlots != null)
        {
            // Reset selection states first
            foreach (Slot slot in slots)
            {
                if (slot != null)
                {
                    slot.isSelected = false;
                }
            }

            // Update slots with inventory data
            for (int i = 0; i < slots.Count && i < inventorySlots.Count; i++)
            {
                if (inventorySlots[i].Item != null)
                {
                    // If the item is a tool, copy its equipped state
                    if (inventorySlots[i].Item is ToolInfo toolInfo)
                    {
                        if (slots[i].Item == null || !(slots[i].Item is ToolInfo))
                        {
                            slots[i].Initialize(toolInfo);
                        }
                        // Copy the equipped state
                        ((ToolInfo)slots[i].Item).isEquipped = toolInfo.isEquipped;
                    }
                    else
                    {
                        slots[i].Initialize(inventorySlots[i].Item);
                    }
                    slots[i].stackCount = inventorySlots[i].stackCount;
                }
                else
                {
                    slots[i].RemoveItemAll();
                }
            }
            // Only update UI if sell panel is active
            if (UIManager.Instance.sellUI.sellPanel.activeSelf)
            {
                UIManager.Instance.sellUI.UpdateUI();
            }
        }
    }

    public List<Slot> GetSlots()
    {
        return slots;
    }

    public List<int> GetSelectedSlotIndex()
    {
        List<int> slotIndex = new List<int>();

        // Check if slots list exists
        if (slots == null)
        {
            return slotIndex;
        }

        // Iterate through slots and collect indices of selected slots
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i] != null && slots[i].isSelected)
            {
                slotIndex.Add(i);
            }
        }
        return slotIndex;
    }
}
