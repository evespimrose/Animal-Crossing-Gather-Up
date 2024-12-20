using UnityEngine;

[CreateAssetMenu(fileName = "NewTool", menuName = "Items/Tool/Tool")]
public class ToolInfo : Item
{
	public GameObject prefab;

	public int maxDurability = 5;
	public int currentDurability;
	public bool isEquipped;
	public CollectCommand collectCommand;
	public ToolType toolType;

	private void OnEnable()
	{
		currentDurability = maxDurability;
		isEquipped = false;
		optionText[0] = "���";
	}


	public enum ToolType
	{
		None,
		Axe,
		FishingPole,
		BugNet
	};
}
