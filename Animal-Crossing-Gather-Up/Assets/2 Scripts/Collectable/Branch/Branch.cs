using System.Collections;
using UnityEngine;

public class Branch : MonoBehaviour, ICollectable
{
    [Header("Drop Settings")]
    [SerializeField] private DropAnimator dropAnimator;
    
    private BranchInfo branchInfo;
    
    public bool IsSpawning => dropAnimator.IsAnimating;

    public void Initialize(BranchInfo info)
    {
        branchInfo = info;
    }

    public void Collect()
    {
        if (IsSpawning || branchInfo == null) return;
        
        Debug.Log("Branch collected.");
        //GameManager.Instance.inventory.AddItem(branchInfo);
        GameManager.Instance.player.Collect(branchInfo);
        Destroy(gameObject);
    }

    public void Spawn(BranchInfo bInfo)
    {
        branchInfo = bInfo;
        if (dropAnimator != null)
        {
            dropAnimator.StartDropAnimation();
        }
    }
}
