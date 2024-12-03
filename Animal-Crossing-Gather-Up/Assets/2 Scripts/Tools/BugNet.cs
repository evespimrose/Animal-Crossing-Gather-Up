using UnityEngine;

public class BugNet : MonoBehaviour, ITool
{
    public BugNetInfo bugNetInfo;
    private ICollectCommand collectCommand;

    private void Awake()
    {
        collectCommand = new BugNetCollectCommand();
    }

    public void Execute()
    {
        if (bugNetInfo.currentDurability > 0)
        {
            collectCommand.Execute();
            bugNetInfo.currentDurability--;
        }
    }
}
