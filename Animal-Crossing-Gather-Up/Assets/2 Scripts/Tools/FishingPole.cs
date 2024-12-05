using UnityEngine;

public class FishingPole : MonoBehaviour, ITool
{
    [SerializeField] private ToolInfo toolInfo;
    private FishingPoleCollectCommand collectCommand;

    [SerializeField] private GameObject fishingChipPrefab;
    [SerializeField] private GameObject fishingChipInstantiate;

    public ToolInfo ToolInfo => toolInfo;

    private void Awake()
    {
        collectCommand = new FishingPoleCollectCommand();
    }

    public void Execute(Vector3 position, Vector3 foward = default)
    {
        if (toolInfo.currentDurability > 0)
        {
            //collectCommand.Execute(position);
            toolInfo.currentDurability--;

            fishingChipInstantiate = Instantiate(fishingChipPrefab, position + (foward * 5f), Quaternion.identity);

            if(fishingChipInstantiate.TryGetComponent(out FishingChip fishingChip))
                fishingChip.Execute(Random.Range(1f, 8f));
        }
    }
    public void UnExecute()
    {
        if (fishingChipInstantiate.TryGetComponent(out FishingChip fishingChip))
            fishingChip.UnExecute();

        Destroy(fishingChipInstantiate);
    }
}
