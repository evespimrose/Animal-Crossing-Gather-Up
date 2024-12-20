using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FishingChip : MonoBehaviour
{
    private Coroutine enumerator;
    public void Execute(float castingTime)
    {
        GameManager.Instance.player.animReciever.isFishing = true;
        enumerator = StartCoroutine(SearchFishCoroutine(castingTime));
    }

    public void UnExecute()
    {
        StopCoroutine(enumerator);
    }

    private IEnumerator SearchFishCoroutine(float castingTime)
    {
        yield return new WaitForSeconds(castingTime);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f);

        List<OceanFish> fishInRange = new();

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out OceanFish fish))
            {
                fishInRange.Add(fish);
            }
        }

        if (fishInRange.Count > 0)
        {
            OceanFish randomFish = fishInRange[Random.Range(0, fishInRange.Count)];
            randomFish.Collect();
            
            yield break;
        }
        else
        {
            Debug.Log("No fish found in range.");
            GameManager.Instance.player.ActivateAnimation(null, true, 3);
            yield break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
            if (player.EquippedTool.TryGetComponent(out FishingPole fishingPole))
            {
                fishingPole.UnExecute();
            }
    }
        
}
