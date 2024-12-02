using UnityEngine;

public class Stone : Item
{
    public void Collect()
    {
        Debug.Log("Stone collected.");
        GameManager.Instance.inventory.AddItem(this);
    }
} 