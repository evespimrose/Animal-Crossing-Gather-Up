using UnityEngine;

public class Fish : Item
{
    public void Collect()
    {
        Debug.Log("Fish collected.");
        GameManager.Instance.inventory.AddItem(this);
    }
} 