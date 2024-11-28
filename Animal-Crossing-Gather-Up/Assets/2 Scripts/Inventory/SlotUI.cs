using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
	public Image itemImage; // Reference to the UI Image for the item
	public TextMeshProUGUI stackCountText;  // Reference to the UI Text for the stackCount

	public void UpdateUI(Item item, int stackCount)
	{
		if (item != null)
		{
			itemImage.sprite = item.icon;   // Set the item icon
			stackCountText.text = stackCount > 1 ? stackCount.ToString() : "";  // Show stackCount if greater than 1
		}
		else
		{
			itemImage.sprite = null;    // Clear the image if no item
			stackCountText.text = "";   // Clear the stackCount
		}
	}
}
