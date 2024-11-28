using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{   
    private IslandTreeManager islandTreeManager;

    private void Start()
    {
        islandTreeManager = GetComponent<IslandTreeManager>();//������Ʈ ��������       
    }

    //�̺κ��� �Ⱦ��� ������ �׿��� �Ŵ������ִ� currentBugCount�� ���� ����
    private void OnDestroy()
    {
        if (islandTreeManager != null)
        {
            islandTreeManager.RemoveBug();
        }
    }

    //�÷��̾� ��ȣ�ۿ�κ�
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player")) { Destroy(gameObject); }    
    //}

    //�ӽ÷� ���콺 Ŭ������ ���� ���� 
    private void OnMouseDown()
    {        
        Destroy(gameObject);
    }   
}
