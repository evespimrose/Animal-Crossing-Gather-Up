using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower: MonoBehaviour
{
    private FlowerInfo flowerInfo;

    public void Collect()
    {
        Debug.Log("Flower collected.");
        GameManager.Instance.inventory.AddItem(flowerInfo);
    }
}
