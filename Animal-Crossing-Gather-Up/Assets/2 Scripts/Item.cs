using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/NotTool")]
public class Item : ScriptableObject
{
	public string itemName;
	public int stackLimit;
	public int basePrice;
	public Sprite icon;
	public string[] optionText = { "근처에 두기" };
	public string[] fullInventoryOptionText = { "바꾸기" };
}
