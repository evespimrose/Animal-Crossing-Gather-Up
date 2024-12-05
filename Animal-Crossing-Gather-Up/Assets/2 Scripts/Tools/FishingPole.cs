using UnityEngine;

public class FishingPole : MonoBehaviour, ITool
{
    [SerializeField] private ToolInfo toolInfo;
    private ICollectCommand collectCommand;

    public ToolInfo ToolInfo => toolInfo;

    private void Awake()
    {
        collectCommand = new FishingPoleCollectCommand();
    }

    public void Execute(Vector3 position)
    {
        if (toolInfo.currentDurability > 0)
        {
            collectCommand.Execute(position);
            toolInfo.currentDurability--;
        }
    }
}
