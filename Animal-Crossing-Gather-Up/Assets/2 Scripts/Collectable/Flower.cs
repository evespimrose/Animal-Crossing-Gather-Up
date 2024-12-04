using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower: MonoBehaviour
{
    private FlowerInfo _info;

    public void Collect()
    {
        Debug.Log("Flower collected.");
        GameManager.Instance.inventory.AddItem(_info);
    }
}
