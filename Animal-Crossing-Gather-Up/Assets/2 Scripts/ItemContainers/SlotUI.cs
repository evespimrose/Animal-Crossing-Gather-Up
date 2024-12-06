using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
	[Header("UI Settings")]
	public bool isShopSlot = false; // Set at Inspector
	public Image itemImage; // Reference to the UI Image for the item
	public TextMeshProUGUI stackCountText;  // Reference to the UI Text for the stackCount
	public Image choiceBackground;  // Reference to the choice background image
	public Image cursorImage;
	public GameObject itemInfo;
	private TextMeshProUGUI itemNameText;
	private Slot currentSlot;

	private bool isInitialized = false;

	private Item pendingItem = null;
	private int pendingStackCount = 0;
	private bool hasPendingUpdate = false;

	private Color defaultItemColor;
	private Color equippedItemColor;

	public TextMeshProUGUI priceText;

	private void Awake()
	{
		currentSlot = GetComponent<Slot>();
		itemNameText = itemInfo.GetComponentInChildren<TextMeshProUGUI>();
		isInitialized = true;

		if (hasPendingUpdate)
		{
			UpdateUI(pendingItem, pendingStackCount);
			hasPendingUpdate = false;
		}

		// set color only shop slot
		if (isShopSlot)
		{
			defaultItemColor = new Color(1, 1, 1, 1);
			priceText.text = currentSlot.Item.basePrice.ToString();
		}
		else
		{
			defaultItemColor = itemImage.color;
		}
		equippedItemColor = new Color(defaultItemColor.r, defaultItemColor.g, defaultItemColor.b, 0.4f);
	}

	public void UpdateUI(Item item, int stackCount)
	{
		if (isInitialized == false)
		{
			pendingItem = item;
			pendingStackCount = stackCount;
			hasPendingUpdate = true;
			return;
		}

		if (item != null)
		{
			itemNameText.text = item.itemName;
			itemImage.gameObject.SetActive(true);
			itemImage.sprite = item.icon;   // Set the item icon
			stackCountText.text = stackCount > 1 ? stackCount.ToString() : "";  // Show stackCount if greater than 1

			// Set color based on slot type and item type
			if (isShopSlot)
			{
				itemImage.color = new Color(0, 0, 0, 1);
			}
			else if (item is ToolInfo toolInfo)
			{
				itemImage.color = toolInfo.isEquipped ? equippedItemColor : defaultItemColor;
			}
			else
			{
				itemImage.color = defaultItemColor;
			}

			choiceBackground?.gameObject.SetActive(true);    // Ensure choice Background is hidden initialy
		}
		else
		{
			itemImage.gameObject.SetActive(false);  // Hide the image if no item
			stackCountText.text = "";   // Clear the stackCount
			choiceBackground?.gameObject.SetActive(false);   // Ensure choice backgournd is hidden
		}
	}

	public void CursorOnSlotDisplayBackground(bool isCursorOn)
	{
		choiceBackground?.gameObject.SetActive(isCursorOn);  // Activate or deactivate choice background
	}

	// cursor
	public void CursorOnSlotDisplayCursor(bool isCursorOn)
	{
		cursorImage.gameObject.SetActive(isCursorOn);
	}

	// Item Name text
	public void CursorOnSlotDisplayName(bool isCursorOn)
	{
		itemInfo.gameObject.SetActive(isCursorOn);
	}

	public void SelectSlot(bool isSelect)
	{
		//optionUI.SetActive(isSelect);
		//OptionUI optionUI;
		//optionUI.SetOptions(currentSlot.item.optionText);
		// Ȱ��ȭ �ϰ�, �ٸ� Ű ������ �ٽ� ��Ȱ��ȭ
		if (currentSlot.Item != null)
		{
			UIManager.Instance.ShowOptions(currentSlot.Item.optionText);
		}
	}

	public string GetSelectedOption()
	{
		return UIManager.Instance.GetSelectedOption();
	}

	public void SetSelectedOptionInit()
	{
		UIManager.Instance.SetSelectedOptionInit();
	}
}
