using UnityEngine;

public class Axe : MonoBehaviour, ITool
{
    public AxeInfo axeInfo;
    private ICollectCommand collectCommand;

    private void Awake()
    {
        collectCommand = new AxeCollectCommand();
    }

    public void Execute()
    {
        if (axeInfo.currentDurability > 0)
        {
            collectCommand.Execute();
            axeInfo.currentDurability--;
        }
    }
}
