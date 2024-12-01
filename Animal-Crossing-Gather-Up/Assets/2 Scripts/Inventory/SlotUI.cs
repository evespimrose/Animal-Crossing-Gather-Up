using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
	public Image itemImage; // Reference to the UI Image for the item
	public TextMeshProUGUI stackCountText;  // Reference to the UI Text for the stackCount
	public Image choiceBackground;  // Reference to the choice background image
	public Image cursorImage;
	public GameObject itemInfo;
	private TextMeshProUGUI itemNameText;
	private Slot currentSlot;

	private void Start()
	{
		currentSlot = GetComponent<Slot>();
		itemNameText = itemInfo.GetComponentInChildren<TextMeshProUGUI>();
	}

	public void UpdateUI(Item item, int stackCount)
	{
		if (item != null)
		{
			itemNameText.text = item.name;
			itemImage.gameObject.SetActive(true);
			itemImage.sprite = item.icon;   // Set the item icon
			stackCountText.text = stackCount > 1 ? stackCount.ToString() : "";  // Show stackCount if greater than 1
			choiceBackground.gameObject.SetActive(true);    // Ensure choice Background is hidden initialy
		}
		else
		{
			itemImage.gameObject.SetActive(false);  // Hide the image if no item
			stackCountText.text = "";   // Clear the stackCount
			choiceBackground.gameObject.SetActive(false);   // Ensure choice backgournd is hidden
		}
	}

	public void CursorOnSlotDisplayBackground(bool isCursorOn)
	{
		choiceBackground.gameObject.SetActive(isCursorOn);  // Activate or deactivate choice background
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
		// 활성화 하고, 다른 키 누르면 다시 비활성화
	}
}
