using UnityEngine;

[CreateAssetMenu(fileName = "NewFlower", menuName = "Items/Collactable/Flower")]
public class FlowerInfo : Item, ICollectableInfo
{
    public GameObject prefab;

    GameObject ICollectableInfo.prefab => prefab;
} 