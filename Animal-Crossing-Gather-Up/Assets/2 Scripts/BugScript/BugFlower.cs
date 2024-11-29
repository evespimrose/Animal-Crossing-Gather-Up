using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugFlower : MonoBehaviour
{
    private IslandFlowerManager islandFlowerManager;

    private void Start()
    {

        // 안될거같은뎅
        islandFlowerManager = GetComponentInParent<IslandFlowerManager>();
    }

    private void OnDestroy()
    {
        if (islandFlowerManager != null)
        {
            islandFlowerManager.RemoveBug();
        }
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
