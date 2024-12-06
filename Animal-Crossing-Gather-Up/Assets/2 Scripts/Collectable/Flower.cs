using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower: MonoBehaviour, ICollectable
{
    [SerializeField] public FlowerInfo flowerInfo;
    public int flowerCount = 3;
    public int maxFlowers = 3;

    public void Initialize(FlowerInfo info)
    {
        flowerInfo = info;
    }

    public void Collect()
    {
        if (flowerInfo == null || flowerCount <= 0) return;

        Debug.Log("Flower collected.");

        flowerCount--;

        FlowerInfo fInfo = flowerInfo;

        fInfo.basePrice += Random.Range(-1, flowerInfo.basePrice);

        GameManager.Instance.player.CollectItem(fInfo);
    }

    public void RefillBranches(int amount)
    {
        flowerCount = Mathf.Min(maxFlowers, flowerCount + amount);
    }
}
