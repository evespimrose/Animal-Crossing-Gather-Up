using UnityEngine;

[CreateAssetMenu(fileName = "NewTool", menuName = "Items/Tool/Tool")]
public class ToolInfo : Item
{
	public int maxDurability;
	public int currentDurability;
	public bool isEquipped;
	public ICollectCommand collectCommand;

	private void OnEnable()
	{
		currentDurability = maxDurability;
		isEquipped = false;
	}
}