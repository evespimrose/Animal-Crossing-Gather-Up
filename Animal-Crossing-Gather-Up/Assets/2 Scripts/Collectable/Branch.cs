using UnityEngine;

public class Branch : Item
{
    public void Collect()
    {
        Debug.Log("Branch collected.");
        GameManager.Instance.inventory.AddItem(this);
    }
} 