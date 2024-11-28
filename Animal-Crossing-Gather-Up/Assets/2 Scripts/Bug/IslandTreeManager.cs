using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandTreeManager : MonoBehaviour
{
    //SerializeField ���� ���� �ν����Ϳ��� ���� �� �� �ֵ��� �ϱ�����
    [SerializeField] private int maxBugsInIsland = 3;// ���� �ִ� ���� �� �� �ִ� ���� �Ѱ�ġ 
    [SerializeField] private string islandName;      // �� ���� �̸�

    private int currentBugCount = 0; // ���� ������ ��

    //������ ���� ���� Ȯ��
    private bool CanSpawnBug()
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
