using UnityEngine;

public class FishingPole : Tool
{
	private FishingPoleCollectCommand collectCommand;

	[SerializeField] private GameObject fishingChipPrefab;
	[SerializeField] private GameObject fishingChipInstantiate;
	public bool isDoneFishing = false;

	private void Awake()
	{
		collectCommand = new FishingPoleCollectCommand();
	}

	public override void Execute(Vector3 position, Vector3 foward = default)
	{
		toolInfo.currentDurability--;

		if (toolInfo.currentDurability > 0)
		{
			collectCommand.Execute(position);
			fishingChipInstantiate = Instantiate(fishingChipPrefab, position + (foward * 5f), Quaternion.identity);
			float destroyTime = Random.Range(3f, 8f);
			if (fishingChipInstantiate.TryGetComponent(out FishingChip fishingChip))
				fishingChip.Execute(destroyTime);
			Destroy(fishingChipInstantiate, destroyTime + 0.1f);
		}

		GameManager.Instance.inventory.UpdateToolDurability(toolInfo);
		Debug.Log($"toolInfo.currentDurability : {toolInfo.currentDurability}");
	}
	public void UnExecute()
	{
		if (fishingChipInstantiate.TryGetComponent(out FishingChip fishingChip))
			fishingChip.UnExecute();

		GameManager.Instance.player.ActivateAnimation(null, true, 3);

		Destroy(fishingChipInstantiate);
	}
}
