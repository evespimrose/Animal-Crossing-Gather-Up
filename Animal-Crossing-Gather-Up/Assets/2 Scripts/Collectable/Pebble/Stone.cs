using UnityEngine;

public class Stone : MonoBehaviour, ICollectable
{
    public PebbleInfo pebbleInfo;
    public int pebbleCount = 0;
    public int maxPebbles = 3;

    public void Initialize(PebbleInfo pebbleInfo)
    {
        this.pebbleInfo = pebbleInfo;
    }

    private void OnMouseDown()
    {
        if (pebbleCount > 0)
        {
            Collect();
        }
        else
        {
            Debug.Log("No branches available to collect.");
        }
    }

    public void Collect()
    {
        if (pebbleCount <= 0 || pebbleInfo == null) return;

        GameObject pebbleObject = Instantiate(pebbleInfo.prefab, transform.position, Quaternion.identity);
        if (!pebbleObject.TryGetComponent(out Pebble pebble))
        {
            Destroy(pebbleObject);
            return;
        }
        pebble.Spawn(pebbleInfo);

        pebbleCount = Mathf.Max(0, pebbleCount - 1);
    }

    public void RefillBranches(int amount)
    {
        pebbleCount = Mathf.Min(maxPebbles, pebbleCount + amount);
    }
} 