using UnityEngine;

public class BugNet : Tool
{
    private ICollectCommand collectCommand;

    private void Awake()
    {
        collectCommand = new BugNetCollectCommand();
    }

    public override void Execute(Vector3 position, Vector3 foward = default)
    {
        if (ToolInfo.currentDurability > 0)
        {
            collectCommand.Execute(position);
            ToolInfo.currentDurability--;

            GameManager.Instance.inventory.UpdateToolDurability(ToolInfo);
            Debug.Log($"toolInfo.currentDurability : {ToolInfo.currentDurability}");
        }
    }
}
