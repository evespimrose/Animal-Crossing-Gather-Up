using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTool", menuName = "Items/Tool")]
public class Tool : Item
{
	// TODO : Collect �ߵ� �Լ�. Interface���� ��ŷ


	public int maxDurability;
	// �ִ� ������
	public int currentDurability;
	public bool isEquipped;

	public CollectCommand collectCommand;
}
