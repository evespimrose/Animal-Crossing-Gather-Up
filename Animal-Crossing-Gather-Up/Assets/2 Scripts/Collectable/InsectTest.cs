using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectTest : Item
{
    //private IslandTreeManager islandTreeManager;

    private void Start()
    {
        //islandTreeManager = GetComponent<IslandTreeManager>(); // IslandTreeManager 초기??
    }

    //private void OnDestroy()
    //{
    //    if (islandTreeManager != null)
    //    {
    //        islandTreeManager.RemoveBug();
    //    }
    //}

    private void OnMouseDown()
    {
        Collect(); // 마우???�릭 ??Collect ?�출
       // Destroy(gameObject);
    }

    public void Collect()
    {
        Debug.Log("Bug collected.");
        GameManager.Instance.inventory.AddItem(this);
    }
} 