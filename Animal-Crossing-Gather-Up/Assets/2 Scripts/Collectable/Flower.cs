using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower: MonoBehaviour, ICollectable
{
    [SerializeField] public FlowerInfo flowerInfo;

    public void Initialize(FlowerInfo info)
    {
        flowerInfo = info;
    }

    public void Collect()
    {
        if (flowerInfo == null) return;

        Debug.Log("Flower collected.");

        GameManager.Instance.player.Collect(flowerInfo);
        Destroy(gameObject);
    }
}
