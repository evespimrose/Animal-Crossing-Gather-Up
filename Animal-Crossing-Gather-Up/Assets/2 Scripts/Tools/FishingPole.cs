using UnityEngine;

public class FishingPole : MonoBehaviour, ITool
{
    [SerializeField] private ToolInfo toolInfo;
    private FishingPoleCollectCommand collectCommand;

    [SerializeField] private GameObject fishingChipPrefab;
    [SerializeField] private GameObject fishingChipInstantiate;
    public bool isDoneFishing = false;

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
            float destroyTime = Random.Range(3f, 8f);
            if (fishingChipInstantiate.TryGetComponent(out FishingChip fishingChip))
                fishingChip.Execute(destroyTime);
            Destroy(fishingChipInstantiate, destroyTime + 0.1f);
        }
    }
    public void UnExecute()
    {
        if (fishingChipInstantiate.TryGetComponent(out FishingChip fishingChip))
            fishingChip.UnExecute();

        GameManager.Instance.player.ActivateAnimation(null, true, 3);

        Destroy(fishingChipInstantiate);
    }
}
