using UnityEngine;

public class BugNet : Tool
{
	private CollectCommand collectCommand;

	private void Awake()
	{
		collectCommand = new BugNetCollectCommand();
	}

	public override void Execute(Vector3 position, Vector3 foward = default)
	{
		if (toolInfo.currentDurability > 0)
		{
			collectCommand.Execute(position);
			toolInfo.currentDurability--;

			GameManager.Instance.inventory.UpdateToolDurability(toolInfo);
			Debug.Log($"toolInfo.currentDurability : {toolInfo.currentDurability}");
		}
	}
}
