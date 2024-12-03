using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour, ICollectable
{
    private BranchInfo branchInfo;
    
    public void Spawn(BranchInfo bInfo)
    {
        branchInfo = bInfo;
    }
    public void Collect()
    {
        Debug.Log("Branch collected.");
        GameManager.Instance.inventory.AddItem(branchInfo);
        Destroy(gameObject);
    }
}
