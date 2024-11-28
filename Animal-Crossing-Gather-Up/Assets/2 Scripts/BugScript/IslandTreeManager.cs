using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandTreeManager : MonoBehaviour
{
    //SerializeField 쓰는 이유 인스펙터에서 수정 할 수 있도록 하기위해
    [SerializeField] private int maxBugsInIsland = 3;// 섬의 최대 생성 될 수 있는 벌레 한계치 
    [SerializeField] private string islandName;      // 각 섬의 이름

    private int currentBugCount = 0; // 현재 벌레의 수

    //생성이 가능 한지 확인
    private bool CanSpawnBug()
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
