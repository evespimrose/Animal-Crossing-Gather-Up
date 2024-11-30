using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BugInfo 
{
    //버그id , 버그 이름(디스플레이상) , 프리펩 , 타입
    //가중치(확률), 값
   
    
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
