using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{   
    private IslandTreeManager islandTreeManager;

    private void Start()
    {
        islandTreeManager = GetComponent<IslandTreeManager>();//컴포넌트 가져오기       
    }

    //이부분을 안쓰면 벌레를 죽여도 매니져에있는 currentBugCount는 줄지 않음
    private void OnDestroy()
    {
        if (islandTreeManager != null)
        {
            islandTreeManager.RemoveBug();
        }
    }

    //플레이어 상호작용부분
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player")) { Destroy(gameObject); }    
    //}

    //임시로 마우스 클릭으로 벌레 제거 
    private void OnMouseDown()
    {        
        Destroy(gameObject);
    }   
}
