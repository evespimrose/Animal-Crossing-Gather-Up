using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pebble : MonoBehaviour, ICollectable
{
    [Header("Drop Settings")]
    [SerializeField] private DropAnimator dropAnimator;

    private PebbleInfo pebbleInfo;

    public bool IsSpawning => dropAnimator.IsAnimating;

    public void Initialize(PebbleInfo info)
    {
        pebbleInfo = info;
    }

    public void Collect()
    {
        if (IsSpawning || pebbleInfo == null) return;

        Debug.Log("Branch collected.");
        GameManager.Instance.inventory.AddItem(pebbleInfo);
        Destroy(gameObject);
    }

    public void Spawn(PebbleInfo pInfo)
    {
        pebbleInfo = pInfo;
        if (dropAnimator != null)
        {
            dropAnimator.StartDropAnimation();
        }
    }
}
