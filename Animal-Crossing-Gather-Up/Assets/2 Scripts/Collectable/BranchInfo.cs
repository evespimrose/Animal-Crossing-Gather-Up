using UnityEngine;

[CreateAssetMenu(fileName = "NewBranch", menuName = "Items/Collactable/Branch")]
public class BranchInfo : Item, ICollectableInfo
{
    public GameObject prefab;

    GameObject ICollectableInfo.prefab => prefab;
} 