using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBug", menuName = "Items/Bug")]
public class BugInfo : Item
{
    //버그id , 버그 이름(디스플레이상) , 프리펩 , 타입
    //가중치(확률), 값
   
    
    
    public GameObject prefab;
    public BugType type;

    [Range(0.1f, 1f)]
    public float spawnWeight;
  

     public enum BugType
    {
        TreeBug,
        FlowerBug
    }

}
