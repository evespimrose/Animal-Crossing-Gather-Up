using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBug", menuName = "Items/Bug")]
public class BugInfo : Item, ICollectableInfo
{
    //����id , ���� �̸�(���÷��̻�) , ������ , Ÿ��
    //����ġ(Ȯ��), ��
   
    
    
    public GameObject prefab;
    public BugType type;

    [Range(0.1f, 1f)]
    public float spawnWeight;

    GameObject ICollectableInfo.prefab => throw new NotImplementedException();

    public enum BugType
    {
        TreeBug,
        FlowerBug
    }

}
