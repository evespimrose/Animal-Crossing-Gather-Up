using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandFlowerManager : MonoBehaviour
{
    [SerializeField] private int maxBugsInIsland = 3;
    [SerializeField] private string islandName;

    private List<FlowerBugSpawner> flowerSpawners = new List<FlowerBugSpawner>();
    private int currentBugCount = 0;

    private void Awake()
    {
        flowerSpawners.AddRange(GetComponentsInChildren<FlowerBugSpawner>());
        if (flowerSpawners.Count == 0) return;
    }

    private void Start()
    {
        StartCoroutine(SpawnBugRoutine());
    }

    private IEnumerator SpawnBugRoutine()
    {
        while (true)
        {
            if (CanSpawnBug())
            {
                SpawnBugOnRandomFlower();
            }
            yield return new WaitForSeconds(5f);
        }
    }

    private void SpawnBugOnRandomFlower()
    {
        List<FlowerBugSpawner> availableFlowers = new List<FlowerBugSpawner>();

        for (int i = 0; i < flowerSpawners.Count; i++)
        {
            if (flowerSpawners[i].currentBug == null)
            {
                availableFlowers.Add(flowerSpawners[i]);
            }
        }
        if (availableFlowers.Count > 0)
        {
            int randomIndex = Random.Range(0, availableFlowers.Count);
            FlowerBugSpawner selectedFlower = availableFlowers[randomIndex];
            selectedFlower.SpawnRandomBug();
        }
    }

    public bool CanSpawnBug()
    {
        return currentBugCount < maxBugsInIsland;
    }

    public void AddBug()
    {
        currentBugCount++;
    }

    public void RemoveBug()
    {
        currentBugCount--;
    }
}
