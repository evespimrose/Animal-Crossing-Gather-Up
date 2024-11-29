using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreebugSpawner : MonoBehaviour
{
    [SerializeField] private List<BugPrefabData> bugPrefab;
    //[SerializeField] private float spawnInterval = 10f; //���� ����
    [SerializeField] private Transform spawnPoint; //�巡�׿� ������� ��ġ ����

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

        //�������� ���� ����
        int randomIndex = Random.Range(0, bugPrefab.Count);
        BugPrefabData selectedBug = bugPrefab[randomIndex];


        // spawnPoint ��ġ�� �����ϰ� Ʈ���� �θ�� ����
        GameObject newBugObject = Instantiate(selectedBug.prefab, spawnPoint.position, selectedBug.prefab.transform.rotation);
        newBugObject.transform.parent = transform; // Ʈ���� �θ�� ����

        currentBug = GetComponentInChildren<Bug>();
        IslandManager.AddBug();

    }

}
