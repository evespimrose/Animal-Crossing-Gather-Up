using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static BugInfo;

public class BugSpawner : MonoBehaviour
{
	[SerializeField] private List<BugInfo> bugList;
	[SerializeField] private Transform spawnPoint;

	public Bug CurrentBug { get; private set; }
	public BugType GetBugType() => bugList != null && bugList.Count > 0
		 ? bugList[0].type
		 : BugType.TreeBug;

	public void Initialize()
	{
		if (spawnPoint == null)
		{
			spawnPoint = transform.Find("SpawnPoint");
		}
	}

	public bool TrySpawnBug()
	{
		if (CurrentBug != null || bugList == null || bugList.Count == 0) return false;

		var selectedBug = SelectRandomBug();
		if (selectedBug == null) return false;

		SpawnBug(selectedBug);
		return true;
	}

	private BugInfo SelectRandomBug()
	{
		float totalWwight = bugList.Sum(bug => bug.spawnWeight);
		float random = Random.Range(0f, totalWwight);

		foreach (var buginfo in bugList)
		{
			random -= buginfo.spawnWeight;
			if (random < 0f)
				return buginfo;
		}
		return bugList[0];
	}

	private void SpawnBug(BugInfo buginfo)
	{
		Quaternion rotation = Quaternion.Euler(0, 0, 0);

		var bugObject = Instantiate(buginfo.prefab, spawnPoint.position,
				rotation, transform);
		if (!bugObject.TryGetComponent<Bug>(out Bug bug))
		{
			Destroy(bugObject);
			return;
		}


		CurrentBug = bug;
		CurrentBug.Initialize(buginfo);
		GameManager.Instance.AddBug(buginfo);
	}


}
