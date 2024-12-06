using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using static BugInfo;

public class GameManager : SingletonManager<GameManager>
{
    [SerializeField] private int maxTreeBugs = 2;
    [SerializeField] private int maxFlowerBugs = 2;
    [SerializeField] protected float spawnInterval = 5f;

    [SerializeField] private int maxFish = 3;
    [SerializeField] protected float FishspawnInterval = 5f;

    private List<BugSpawner> treeSpawners = new List<BugSpawner>();
    private List<BugSpawner> flowerSpawners = new List<BugSpawner>();
    private List<FishSpawner> fishSpawners = new List<FishSpawner>();
    
    private int currentFish;
    private int currentTreeBugs;
    private int currentFlowerBugs;

    public Player player;
    public Inventory inventory;

    [SerializeField] private List<OakTree> oakTrees = new();
    private const float respawnTime = 86400f;
    protected override void Awake()
    {
        base.Awake();  
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���� �ʱ�ȭ
        currentFish = 0;
        currentTreeBugs = 0;
        currentFlowerBugs = 0;

        // ����Ʈ �ʱ�ȭ
        treeSpawners.Clear();
        flowerSpawners.Clear();
        fishSpawners.Clear();

        // ������ ã��
        FindBugSpawnerByType();
        FindFishSpawnerByType();
    }

    private void Start()
    {
        FindBugSpawnerByType();
        //���̵� ������ Ÿ���� ���� ����� ����
        FindFishSpawnerByType();

        StartCoroutine(RefillBranchesRoutine());

        player = FindObjectOfType<Player>();
        inventory = FindObjectOfType<Inventory>();
        player.OnItemCollected += inventory.AddItem;
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


    private void FindFishSpawnerByType()
    {
        fishSpawners.AddRange(FindObjectsOfType<FishSpawner>());
        foreach (var spawner in fishSpawners)
        {
            spawner.Initialize();
        }
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

            // ����� ���� �õ�
            if (currentFish < maxFish)
            {
                TrySpawnFishOnRandomSpawner(fishSpawners);
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

    private void TrySpawnFishOnRandomSpawner(List<FishSpawner> spawnerList)
    {
        var availableSpawners = spawnerList.Where(s => s.CurrentFish == null).ToList();
        if (availableSpawners.Count == 0) return;

        int randomIndex = Random.Range(0, availableSpawners.Count);
        availableSpawners[randomIndex].TrySpawnFish();
    }

    
    public void AddFish() => currentFish++;
    public void RemoveFish() => currentFish = Mathf.Max(0, currentFish - 1);

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
