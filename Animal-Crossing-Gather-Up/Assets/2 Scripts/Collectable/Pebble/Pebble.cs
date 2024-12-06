using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pebble : MonoBehaviour, ICollectable
{
    [Header("Drop Settings")]
    [SerializeField] private DropAnimator dropAnimator;

    [SerializeField] private PebbleInfo pebbleInfo;

    public bool IsSpawning => dropAnimator.IsAnimating;

    public void Initialize(PebbleInfo info)
    {
        pebbleInfo = info;
    }

    public void Collect()
    {
        if (IsSpawning || pebbleInfo == null) return;

        Debug.Log("Pebble collected.");

        GameManager.Instance.player.Collect(pebbleInfo);

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
