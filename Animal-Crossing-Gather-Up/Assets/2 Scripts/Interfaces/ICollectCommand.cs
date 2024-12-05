using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface ICollectCommand
{
    public void Execute(Vector3 position);
}

//public class CollectCommand : ICollectCommand
//{
//    public virtual void Execute()
//    {

//    }
//}

public class HandFlowerCommand : ICollectCommand
{
    public void Execute(Vector3 position)
    {
        Debug.Log("HandFlowerCommand");

        Collider[] hitColliders = Physics.OverlapSphere(position, 1f);

        List<Collider> sortedColliders = hitColliders.OrderBy(collider => Vector3.Distance(position, collider.transform.position)).ToList();

        foreach (var hitCollider in sortedColliders)
        {
            if (hitCollider.TryGetComponent(out Flower flower))
            {
                flower.Collect();
                Debug.Log($"Collected {flower.name}");
                return;
            }
            else if (hitCollider.TryGetComponent(out Branch branch))
            {
                branch.Collect();
                Debug.Log($"Collected {branch.name}");
                return;
            }
            else if (hitCollider.TryGetComponent(out Pebble pebble))
            {
                pebble.Collect();
                Debug.Log($"Collected {pebble.name}");
                return;
            }
        }
    }
}


public class BugNetCollectCommand : ICollectCommand
{
    public void Execute(Vector3 position)
    {
        Debug.Log("BugNetCollectCommand");

        Collider[] hitColliders = Physics.OverlapSphere(position, 1f);
        List<Collider> sortedColliders = hitColliders.OrderBy(collider => Vector3.Distance(position, collider.transform.position)).ToList();

        foreach (var hitCollider in sortedColliders)
        {
            if (hitCollider.TryGetComponent(out Bug bug))
            {
                bug.Collect();
                return;

            }
        }
    }
}

public class FishingRodCollectCommand : ICollectCommand
{
    public void Execute(Vector3 position)
    {
        Debug.Log("FishingRodCollectCommand");

        Collider[] hitColliders = Physics.OverlapSphere(position, 1f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out Fish fish))
            {
                fish.Collect();
                return;

            }
        }
    }
}

public class AxeCollectCommand : ICollectCommand
{
    public void Execute(Vector3 position)
    {
        Debug.Log("AxeCollectCommand");
        Collider[] hitColliders = Physics.OverlapSphere(position, 1f);
        List<Collider> sortedColliders = hitColliders.OrderBy(collider => Vector3.Distance(position, collider.transform.position)).ToList();

        foreach (var hitCollider in sortedColliders)
        {
            //Debug.Log($"AxeCollectCommand - {hitCollider.name}, {hitCollider.gameObject.name}");
            if (hitCollider.TryGetComponent(out OakTree tree))
            {
                tree.Collect();
                return;
            }
            else if (hitCollider.TryGetComponent(out Stone stone))
            {
                stone.Collect();
                return;
            }
        }
    }
}

