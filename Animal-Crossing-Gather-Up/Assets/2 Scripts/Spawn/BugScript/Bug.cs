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

        

        GameManager.Instance.RemoveBug(info); // SingletonManager<> ��ӹ���?�Ŵ���

        BugInfo bInfo = info;

        bInfo.basePrice += Random.Range(-1, info.basePrice);

        GameManager.Instance.player.CollectItemWithCeremony(bInfo);

        Destroy(gameObject);
    }
    public int GetValue() => info.basePrice;
}
