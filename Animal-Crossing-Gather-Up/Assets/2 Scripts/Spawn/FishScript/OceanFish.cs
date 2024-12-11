using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanFish : MonoBehaviour, ICollectable
{
	private FishInfo info;
	private Vector3 spawnPoint;
	public bool isSearched;
	[SerializeField] GameObject renderPrefab;

	public void Initialize(FishInfo fishInfo)
	{

		info = fishInfo;
		spawnPoint = transform.position;
		StartCoroutine(WanderingRoutine());
	}

	public void Collect()
	{
		GetValue();
		GameManager.Instance.RemoveFish();

		FishInfo fishInfo = info;

		fishInfo.basePrice += Random.Range(-1, info.basePrice);

		StartCoroutine(WaitForActingAndCollectCoroutine(fishInfo));
	}

	private IEnumerator WaitForActingAndCollectCoroutine(FishInfo fInfo)
	{
		yield return new WaitUntil(() => !GameManager.Instance.player.animReciever.isActing);
		Debug.Log("WaitForActingAndCollectCoroutine");

		FishInfo fishInfo = fInfo;
		renderPrefab.gameObject.SetActive(false);

		yield return StartCoroutine(GameManager.Instance.player.CollectItemWithCeremony(fishInfo));

		Destroy(gameObject);

	}
	public int GetValue() => info.basePrice;

	private IEnumerator WanderingRoutine()
	{
		while (true)
		{
			Vector3 randomPoint = spawnPoint + Random.insideUnitSphere * info.wanderRadius;
			randomPoint.y = spawnPoint.y;

			Vector3 startPos = transform.position;
			float journeyLength = Vector3.Distance(startPos, randomPoint);
			float startTime = Time.time;

			while (Vector3.Distance(transform.position, randomPoint) > 0.1f)
			{
				float distanceCovered = (Time.time - startTime) * info.moveSpeed;
				float fractionOfJourney = distanceCovered / journeyLength;

				transform.position = Vector3.Lerp(startPos, randomPoint, fractionOfJourney);

				if ((randomPoint - transform.position).normalized != Vector3.zero)
				{
					transform.rotation = Quaternion.LookRotation((randomPoint - transform.position).normalized);
				}
				yield return null;
			}
			yield return new WaitForSeconds(Random.Range(1f, 3f));

		}
	}
}
