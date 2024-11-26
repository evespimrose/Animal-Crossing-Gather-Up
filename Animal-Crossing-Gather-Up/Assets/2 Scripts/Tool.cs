using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTool", menuName = "Items/Tool")]
public class Tool : Item
{
	public int maxDurability;
	// 최대 내구도
	public int currentDurability;
	public bool isEquipped;
}
