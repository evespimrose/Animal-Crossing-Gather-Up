using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BugSpawner : MonoBehaviour
{
    [SerializeField] private List<BugInfo> bugList;
    [SerializeField] private Transform spawnPoint;
    private IBugManager manager;
    public Bug CurrentBug { get; private set; }

    public void Initialize(IBugManager bugManager)
    {
        manager = bugManager;
    }

    public bool TrySpawnBug()
    {
        if (CurrentBug != null || bugList.Count == 0) return false;
        var selectedBug = SelectRandomBug();
        SpawnBug(selectedBug);
        return true;
    }

    private BugInfo SelectRandomBug()
    {
        float totalWwight = bugList.Sum(bug => bug.spawnWeight);
        float random = Random.Range(0f, totalWwight);

        foreach (var buginfo in bugList)
        {
            random -= buginfo .spawnWeight;
            if (random < 0f)
                return buginfo;
        }
        return bugList[0];
    }

    private void SpawnBug(BugInfo buginfo)
    {
        Quaternion rotation = Quaternion.Euler(-90, -90, 0);

        var bugObject = Instantiate(buginfo.prefab, spawnPoint.position,
            rotation, transform);

        CurrentBug = bugObject.GetComponent<Bug>();
        CurrentBug.Initialize(buginfo, manager);
        manager.AddBug();
    }


}
