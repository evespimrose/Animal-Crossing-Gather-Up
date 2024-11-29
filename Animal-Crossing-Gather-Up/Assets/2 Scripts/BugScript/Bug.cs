using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    private IslandTreeManager islandTreeManager;

    private void Start()
    {
        islandTreeManager = GetComponentInParent<IslandTreeManager>();
    }

    private void OnDestroy()
    {
        if (islandTreeManager != null)
        {
            islandTreeManager.RemoveBug();
        }
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}