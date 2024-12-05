using UnityEngine;

public class BugNet : MonoBehaviour, ITool
{
    [SerializeField] private ToolInfo toolInfo;
    private ICollectCommand collectCommand;

    public ToolInfo ToolInfo => toolInfo;

    private void Awake()
    {
        collectCommand = new BugNetCollectCommand();
    }

    public void Execute(Vector3 position, Vector3 foward = default)
    {
        if (toolInfo.currentDurability > 0)
        {
            collectCommand.Execute(position);
            toolInfo.currentDurability--;
        }
    }
}
