using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
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

    private void Collect()
    {
        GetValue();
        Destroy(gameObject);

        BaseIslandManager.Instance.RemoveBug(info); // SingletonManager<> ��ӹ��� �Ŵ���
        GameManager.Instance.inventory.AddItem(info);
    }
    public int GetValue() => info.basePrice;
}
