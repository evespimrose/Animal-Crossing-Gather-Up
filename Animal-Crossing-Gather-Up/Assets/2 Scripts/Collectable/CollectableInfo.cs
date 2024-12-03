using UnityEngine;

[CreateAssetMenu(fileName = "NewBranch", menuName = "Items/Collactable/Branch")]
public class BranchInfo : Item, ICollectableInfo
{
    public GameObject prefab;

    GameObject ICollectableInfo.prefab => prefab;
}

[CreateAssetMenu(fileName = "NewPebble", menuName = "Items/Collactable/Pebble")]
public class PebbleInfo : Item, ICollectableInfo
{
    public GameObject prefab;

    GameObject ICollectableInfo.prefab => prefab;
}