using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseIslandManager : MonoBehaviour, IBugManager
{
    [SerializeField] protected int maxBugs = 3;
    [SerializeField] protected float spawnInterval = 5f;

    protected List<BugSpawner> spawners = new List<BugSpawner>();
    protected int currentBugCount;

    protected virtual void Start()
    {
        InitializeSpawners();
        if (spawners.Count > 0)  // 스포너가 있는지 확인
        {
            StartCoroutine(SpawnRoutine());
        }
        else
        {
            Debug.LogError("No BugSpawners found!");
        }
    }

    protected virtual void InitializeSpawners()
    {
        spawners.AddRange(GetComponentsInChildren<BugSpawner>());
        foreach (var spawner in spawners)
        {
            spawner.Initialize(this);
        }
    }

    protected virtual IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (CanSpawnBug())
            {
                TrySpawnBugOnRandomSpawner();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    protected virtual void TrySpawnBugOnRandomSpawner()
    {
        var availableSpawners = spawners.Where(s => s.CurrentBug == null).ToList();
        if (availableSpawners.Count == 0) return;

        int randomIndex = Random.Range(0, availableSpawners.Count);
        availableSpawners[randomIndex].TrySpawnBug();
    }

    public virtual bool CanSpawnBug() => currentBugCount < maxBugs;
    public virtual void AddBug() => currentBugCount++;
    public virtual void RemoveBug() => currentBugCount = Mathf.Max(0, currentBugCount - 1);
    public abstract void CatchBug(Bug bug);
}
