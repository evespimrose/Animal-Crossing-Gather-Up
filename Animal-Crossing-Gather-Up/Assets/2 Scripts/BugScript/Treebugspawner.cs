using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreebugSpawner : MonoBehaviour
{
    [SerializeField] private List<BugPrefabData> bugPrefab;
    [SerializeField] private float spawnInterval = 10f; //���� ����
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

      
        //������ ���� ���� ����
        GameObject newBugObject = Instantiate(selectedBug.prefab, spawnPoint.position, selectedBug.prefab.transform.rotation);
        newBugObject.transform.SetParent(spawnPoint, worldPositionStays: true);

        currentBug = newBugObject.GetComponent<Bug>();
        IslandManager.AddBug();

    }





}
