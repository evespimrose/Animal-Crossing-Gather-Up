using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static BugInfo;

public class BaseIslandManager : SingletonManager<BaseIslandManager>
{
    [SerializeField] private int maxTreeBugs = 2;
    [SerializeField] private int maxFlowerBugs = 2;

    [SerializeField] protected float spawnInterval = 5f;

    private List<BugSpawner> treeSpawners = new List<BugSpawner>();
    private List<BugSpawner> flowerSpawners = new List<BugSpawner>();

    private int currentTreeBugs;
    private int currentFlowerBugs;

    protected override void Awake()
    {
        base.Awake();  
    }

    private void Start()
    {
        FindBugSpawnerByType();
        //돌이든 물고기든 타입을 각각 여기다 놓기

    }
    private void FindBugSpawnerByType()
    {
        // 모든 스포너 찾기
        var allSpawners = FindObjectsOfType<BugSpawner>();

        // 스포너의 BugInfo 타입에 따라 분류
        foreach (var spawner in allSpawners)
        {
            if (spawner.GetBugType() == BugType.TreeBug)
                treeSpawners.Add(spawner);
            else
                flowerSpawners.Add(spawner);

            spawner.Initialize();
        }

        StartCoroutine(SpawnRoutine());
    }


    

    public IEnumerator SpawnRoutine()
    {
        while (true)
        {
            // 트리 버그 스폰 시도
            if (currentTreeBugs < maxTreeBugs)
            {
                TrySpawnBugOnRandomSpawner(treeSpawners);
            }

            // 플라워 버그 스폰 시도
            if (currentFlowerBugs < maxFlowerBugs)
            {
                TrySpawnBugOnRandomSpawner(flowerSpawners);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void TrySpawnBugOnRandomSpawner(List<BugSpawner> spawnerList)
    {
        var availableSpawners = spawnerList.Where(s => s.CurrentBug == null).ToList();
        if (availableSpawners.Count == 0) return;

        int randomIndex = Random.Range(0, availableSpawners.Count);
        availableSpawners[randomIndex].TrySpawnBug();
    }

    public void AddBug(BugInfo bugInfo)
    {
        if (bugInfo.type == BugType.TreeBug)
            currentTreeBugs++;
        else
            currentFlowerBugs++;
    }

    public void RemoveBug(BugInfo bugInfo)
    {
        if (bugInfo.type == BugType.TreeBug)
            currentTreeBugs = Mathf.Max(0, currentTreeBugs - 1);
        else
            currentFlowerBugs = Mathf.Max(0, currentFlowerBugs - 1);
    }
}
