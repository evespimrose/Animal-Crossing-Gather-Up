using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingChip : MonoBehaviour
{
    private Coroutine enumerator;
    public void Execute(float castingTime)
    {
        enumerator = StartCoroutine(SearchFishCoroutine(castingTime));
    }

    public void UnExecute()
    {
        StopCoroutine(enumerator);
    }

    private IEnumerator SearchFishCoroutine(float castingTime)
    {
        Debug.Log($"SearchFishCoroutine......castingTime : {castingTime}");
        yield return new WaitForSeconds(castingTime);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out OceanFish fish))
            {
                fish.Collect();
                yield break;
            }
        }
    }
}
