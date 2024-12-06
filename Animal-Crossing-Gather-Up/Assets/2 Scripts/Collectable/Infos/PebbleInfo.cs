using UnityEngine;

[CreateAssetMenu(fileName = "NewPebble", menuName = "Items/Collactable/Pebble")]
public class PebbleInfo : Item, ICollectableInfo
{
    public GameObject prefab;

    GameObject ICollectableInfo.prefab => prefab;
}