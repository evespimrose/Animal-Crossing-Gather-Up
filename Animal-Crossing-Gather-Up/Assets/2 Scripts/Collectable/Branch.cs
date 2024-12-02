using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    private readonly BranchInfo branchInfo;
    
    public void Collect()
    {
        Debug.Log("Branch collected.");
        GameManager.Instance.inventory.AddItem(branchInfo);
    }
}
