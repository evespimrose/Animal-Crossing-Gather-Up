using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IslandTreeManager : MonoBehaviour
{
    //SerializeField ���� ���� �ν����Ϳ��� ���� �� �� �ֵ��� �ϱ�����
    [SerializeField] private int maxBugsInIsland = 3;// ���� �ִ� ���� �� �� �ִ� ���� �Ѱ�ġ 
    [SerializeField] private string islandName;      // �� ���� �̸�

    private List<TreebugSpawner> treeSpawners = new List<TreebugSpawner>();
    private int currentBugCount = 0; // ���� ������ ��

    private void Start()
    {
        treeSpawners.AddRange(GetComponentsInChildren<TreebugSpawner>());

        if (treeSpawners.Count == 0) return;

        //���� �ڷ�ƾ
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
        //������ ���� �ӽ� ����Ʈ
        List<TreebugSpawner> availableTrees = new List<TreebugSpawner>();

        //���� ���� ã��
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



    //������ ���� ���� Ȯ��
    public bool CanSpawnBug()
    {
        return currentBugCount < maxBugsInIsland;
    }

    //���� �߰�
    public void AddBug()
    {
        currentBugCount++;
    }
    //���� ����
    public void RemoveBug()
    {
        currentBugCount--;
    }

}
