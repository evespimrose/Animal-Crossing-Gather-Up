using UnityEngine;

public class Flower : Item
{
    public void Collect()
    {
        Debug.Log("Flower collected.");
        GameManager.Instance.inventory.AddItem(this);
    }
} 