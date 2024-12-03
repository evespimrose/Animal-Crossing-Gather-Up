using UnityEngine;

public class FishingRod : MonoBehaviour, ITool
{
    public FishingRodInfo fishingRodInfo;
    private ICollectCommand collectCommand;

    private void Awake()
    {
        collectCommand = new FishingRodCollectCommand();
    }

    public void Execute()
    {
        if (fishingRodInfo.currentDurability > 0)
        {
            collectCommand.Execute();
            fishingRodInfo.currentDurability--;
        }
    }
}
