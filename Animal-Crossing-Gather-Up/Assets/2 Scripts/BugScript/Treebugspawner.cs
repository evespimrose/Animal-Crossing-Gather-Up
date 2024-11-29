using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreebugSpawner : MonoBehaviour
{
    [SerializeField] private List<BugPrefabData> bugPrefab;
    //[SerializeField] private float spawnInterval = 10f; //스폰 간격
    [SerializeField] private Transform spawnPoint; //드래그엔 드롭으로 위치 지정

    private IslandTreeManager IslandManager;
    public Bug currentBug;

    private void Start() 
    {
          
        IslandManager = GetComponentInParent<IslandTreeManager>();
        if (IslandManager == null) return;       
    }


    public void SpawnRandomBug()
    { 
        if(bugPrefab.Count == 0) return;

        //랜덤으로 벌레 선택
        int randomIndex = Random.Range(0, bugPrefab.Count);
        BugPrefabData selectedBug = bugPrefab[randomIndex];


        // spawnPoint 위치에 생성하고 트리를 부모로 지정
        GameObject newBugObject = Instantiate(selectedBug.prefab, spawnPoint.position, selectedBug.prefab.transform.rotation);
        newBugObject.transform.parent = transform; // 트리를 부모로 지정

        currentBug = GetComponentInChildren<Bug>();
        IslandManager.AddBug();

    }

}
