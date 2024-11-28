using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IslandTreeManager : MonoBehaviour
{
    //SerializeField 쓰는 이유 인스펙터에서 수정 할 수 있도록 하기위해
    [SerializeField] private int maxBugsInIsland = 3;// 섬의 최대 생성 될 수 있는 벌레 한계치 
    [SerializeField] private string islandName;      // 각 섬의 이름

    private List<TreebugSpawner> treeSpawners = new List<TreebugSpawner>();
    private int currentBugCount = 0; // 현재 벌레의 수

    private void Start()
    {
        treeSpawners.AddRange(GetComponentsInChildren<TreebugSpawner>());

        if (treeSpawners.Count == 0) return;

        //시작 코루틴
        StartCoroutine(SpawnBugRoutine());
    }

    private IEnumerator SpawnBugRoutine()
    {
        while (true)
        {
            if (CanSpawnBug())
            {
                SpawnBugOnRandomTree();
            }
            yield return new WaitForSeconds(5f);
        }
    }

    private void SpawnBugOnRandomTree()
    {
        //나무를 담을 임시 리스트
        List<TreebugSpawner> availableTrees = new List<TreebugSpawner>();

        //벌레 없는 찾기
        for (int i = 0; i < treeSpawners.Count; i++)
        {
            if (treeSpawners[i].currentBug == null)
            {
                availableTrees.Add(treeSpawners[i]);
            }
        }
        if (availableTrees.Count > 0)
        {
            int randomIndex = Random.Range(0, availableTrees.Count);
            TreebugSpawner selectedTree = availableTrees[randomIndex];
            selectedTree.SpawnRandomBug();
        }
    }



    //생성이 가능 한지 확인
    public bool CanSpawnBug()
    {
        return currentBugCount < maxBugsInIsland;
    }

    //벌레 추가
    public void AddBug()
    {
        currentBugCount++;
    }
    //벌레 삭제
    public void RemoveBug()
    {
        currentBugCount--;
    }

}
