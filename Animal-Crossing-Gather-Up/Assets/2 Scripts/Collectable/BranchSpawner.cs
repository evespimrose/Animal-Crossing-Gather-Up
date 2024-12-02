using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchSpawner : MonoBehaviour
{
    public void SpawnBranch(BranchInfo branchInfo)
    {
        GameObject branchObject = Instantiate(branchInfo.prefab, transform.position, Quaternion.identity);
    }
} 