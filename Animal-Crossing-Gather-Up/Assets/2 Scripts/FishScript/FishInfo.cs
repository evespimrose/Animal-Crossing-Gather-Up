using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

[CreateAssetMenu(fileName = "NewFish", menuName = "Items/Fish")]
public class FishInfo : Item
{
    public GameObject prefab;
    [Range(0.1f, 1f)]
    public float spawnWeight;
    public float moveSpeed;
    public float wanderRadius = 3f;
    
}
