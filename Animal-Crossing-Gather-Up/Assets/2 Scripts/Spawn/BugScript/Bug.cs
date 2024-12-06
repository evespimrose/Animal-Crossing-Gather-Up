using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour, ICollectable
{
   //���� ���� ����
   //���� ibugmanger

    private BugInfo info;
    

    public void Initialize(BugInfo buginfom)
    {
        info = buginfom;
      
    }

    private void OnMouseDown()
    {
        Collect();
    }

    public void Collect()
    {
        Debug.Log("BugNet - Collect");

        GetValue();
        Destroy(gameObject);
        

        BaseIslandManager.Instance.RemoveBug(info);
        GameManager.Instance.inventory.AddItem(info);
        // player.bugcollectwithceremony(gameObject); <- Destroy(gameObject);
    }
    public int GetValue() => info.basePrice;
}
