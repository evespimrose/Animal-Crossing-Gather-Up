using UnityEngine;

public class Axe : MonoBehaviour, ITool
{
    [SerializeField] private ToolInfo toolInfo;
    private ICollectCommand collectCommand;

    public ToolInfo ToolInfo => toolInfo;

    private void Awake()
    {
        collectCommand = new AxeCollectCommand();
    }

    public void Execute()
    {
        if (toolInfo.currentDurability > 0)
        {
            collectCommand.Execute();
            toolInfo.currentDurability--;
        }
    }
}
