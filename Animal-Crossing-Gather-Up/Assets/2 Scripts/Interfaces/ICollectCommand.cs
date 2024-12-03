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
        Collider[] hitColliders = Physics.OverlapSphere(Vector3.zero, 5f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out Flower flower))
            {
                //flower.Collect();
            }
        }
    }
}

public class BugNetCollectCommand : ICollectCommand
{
    public void Execute()
    {
        Collider[] hitColliders = Physics.OverlapSphere(Vector3.zero, 5f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<InsectTest>(out var bug))
            {
                //bug.Collect();
            }
        }
    }
}

public class FishingRodCollectCommand : ICollectCommand
{
    public void Execute()
    {
        Collider[] hitColliders = Physics.OverlapSphere(Vector3.zero, 5f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out Fish fish))
            {
                fish.Collect();
            }
        }
    }
}

public class AxeCollectCommand : ICollectCommand
{
    public void Execute()
    {
        Collider[] hitColliders = Physics.OverlapSphere(Vector3.zero, 5f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out OakTree tree))
            {
                tree.Collect();
            }
            else if (hitCollider.TryGetComponent(out Stone stone))
            {
                stone.Collect();
            }
        }
    }
}

