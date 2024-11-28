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
	public TextMeshProUGUI itemNameText;

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

	public void SelectSlot(bool isSelected)
	{
		choiceBackground.gameObject.SetActive(isSelected);  // Activate or deactivate choice background
	}
}
