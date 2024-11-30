using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectCommand
{
    public void Execute();
}

public class CollectCommand : ICollectCommand
{
    public virtual void Execute()
    {
       
    }
}

public class NetCollectCommand : CollectCommand
{
    public override void Execute()
    {

    }
}

public class FishingRodCollectCommand : CollectCommand
{
    public override void Execute()
    {

    }
}

public class AxeCollectCommand : CollectCommand
{
    public override void Execute()
    {

    }
}

