using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectCommand
{
    public void Execute();
}

//public class CollectCommand : ICollectCommand
//{
//    public virtual void Execute()
//    {
        
//    }
//}

public class HandFlowerCommand : ICollectCommand
{
    public void Execute()
    {
        Debug.Log("");
    }
}

public class BugNetCollectCommand : ICollectCommand
{
    public void Execute()
    {

    }
}

public class FishingRodCollectCommand : ICollectCommand
{
    public void Execute()
    {

    }
}

public class AxeCollectCommand : ICollectCommand
{
    public void Execute()
    {

    }
}

