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

    [SerializeField] private List<OakTree> oakTrees = new();
    private const float respawnTime = 86400f;
    protected override void Awake()
    {
        base.Awake();  
    }

    private void Start()
    {
        FindBugSpawnerByType();
        //̵ Ÿ  Ÿ 

        StartCoroutine(RefillBranchesRoutine());
    }

    private void FindBugSpawnerByType()
    {
        // ���?������ ã��
        var allSpawners = FindObjectsOfType<BugSpawner>();

        // �������� BugInfo Ÿ�Կ� ���� �з�
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
            // Ʈ�� ���� ���� �õ�
            if (currentTreeBugs < maxTreeBugs)
            {
                TrySpawnBugOnRandomSpawner(treeSpawners);
            }

            // �ö��?���� ���� �õ�
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

    private IEnumerator RefillBranchesRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);

            foreach (var oakTree in oakTrees)
            {
                int branchesToSpawn = oakTree.maxBranches - oakTree.branchCount;
                oakTree.RefillBranches(branchesToSpawn);
            }
        }
    }
}
