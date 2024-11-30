using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BugInfo 
{
    //����id , ���� �̸�(���÷��̻�) , ������ , Ÿ��
    //����ġ(Ȯ��), ��
   
    
    public string displayName;
    public GameObject prefab;
    public BugType type;

    [Range(0.1f, 1f)]
    public float spawnWeight;
    public int value;

     public enum BugType
    {
        TreeBug,
        FlowerBug
    }

}
