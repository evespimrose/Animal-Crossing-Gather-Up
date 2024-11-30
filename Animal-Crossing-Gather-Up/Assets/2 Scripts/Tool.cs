using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTool", menuName = "Items/Tool")]
public class Tool : Item
{
	// TODO : Collect 발동 함수. Interface에서 슈킹


	public int maxDurability;
	// 최대 내구도
	public int currentDurability;
	public bool isEquipped;

	public CollectCommand collectCommand;
}
