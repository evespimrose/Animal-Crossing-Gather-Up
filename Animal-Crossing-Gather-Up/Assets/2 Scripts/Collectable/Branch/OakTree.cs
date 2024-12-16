using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OakTree : MonoBehaviour, ICollectable
{
    [SerializeField] private BranchInfo branchInfo;
    public int branchCount = 0;
    public int maxBranches = 3;

    public void Initialize(BranchInfo branchInfo)
    {
        this.branchInfo = branchInfo;
    }

    public void Collect()
    {
        Debug.Log("Tree - Collect");

        if (branchCount <= 0 || branchInfo == null) return;

        GameObject branchObject = Instantiate(branchInfo.prefab, transform.position, Quaternion.identity);
        if (!branchObject.TryGetComponent(out Branch branch))
        {
            Destroy(branchObject);
            return;
        }
        branch.Spawn(branchInfo);

        branchCount = Mathf.Max(0, branchCount - 1);    
    }

    public void RefillBranches(int amount)
    {
        branchCount = Mathf.Min(maxBranches, branchCount + amount);
    }
} 