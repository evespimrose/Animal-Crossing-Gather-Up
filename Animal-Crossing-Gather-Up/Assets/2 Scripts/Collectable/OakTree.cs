using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OakTree : MonoBehaviour
{
    public BranchInfo branchInfo;
    public int branchCount = 0;
    public int maxBranches = 3;

    public void Initialize(BranchInfo branchInfo)
    {
        this.branchInfo = branchInfo;
    }

    private void OnMouseDown()
    {
        if (branchCount > 0)
        {
            Collect();
        }
        else
        {
            Debug.Log("No branches available to collect.");
        }
    }

    private void Collect()
    {
        Debug.Log("Branch collected from OakTree.");
        GameManager.Instance.inventory.AddItem(branchInfo);
        branchCount--;
    }

    public void RefillBranches(int amount)
    {
        branchCount = Mathf.Min(maxBranches, branchCount + amount);
        Debug.Log("Branches have been refilled on the OakTree.");
    }
} 