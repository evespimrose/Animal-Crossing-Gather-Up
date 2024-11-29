using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBugSpawner : MonoBehaviour
{
    [SerializeField] private List<BugPrefabData> bugPrefab;
    [SerializeField] private Transform spawnPoint;

    private IslandFlowerManager IslandManager;
    public Bug currentBug;

    private void Start()
    {
        IslandManager = GetComponentInParent<IslandFlowerManager>();
        if (IslandManager == null) return;
    }

    public void SpawnRandomBug()
    {
        if (bugPrefab.Count == 0) return;

        int randomIndex = Random.Range(0, bugPrefab.Count);
        BugPrefabData selectedBug = bugPrefab[randomIndex];

        GameObject newBugObject = Instantiate(selectedBug.prefab, spawnPoint.position, selectedBug.prefab.transform.rotation);
        newBugObject.transform.parent = transform;

        currentBug = GetComponentInChildren<Bug>();
        IslandManager.AddBug();
    }
}