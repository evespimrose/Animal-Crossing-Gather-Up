using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectCommand
{
    void Execute();
}

public class NetCollectCommand : ICollectCommand
{
    public void Execute()
    {
        Debug.Log("잠자리채 사용 채집!");
    }
}

public class FishingRodCollectCommand : ICollectCommand
{
    public void Execute()
    {
        Debug.Log("낚싯대 사용 채집!");
    }
}

public class AxeCollectCommand : ICollectCommand
{
    public void Execute()
    {
        Debug.Log("도끼 사용 채집!");
    }
}

