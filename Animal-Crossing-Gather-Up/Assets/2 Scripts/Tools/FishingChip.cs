using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FishingChip : MonoBehaviour
{
	private Coroutine enumerator;
	public void Execute(float castingTime)
	{
		GameManager.Instance.player.isFishing = false;
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
			GameManager.Instance.player.isFishing = false;
			yield break;
		}
		else
		{
			Debug.Log("No fish found in range.");

			yield break;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.TryGetComponent(out Player player))
			if (player.EquippedTool.TryGetComponent(out FishingPole fishingPole))
				fishingPole.UnExecute();
	}
}
