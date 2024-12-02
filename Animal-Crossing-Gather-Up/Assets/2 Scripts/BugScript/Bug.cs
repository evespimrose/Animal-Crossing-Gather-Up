using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
   //버그 인포 참조
   //버그 ibugmanger

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

        BaseIslandManager.Instance.RemoveBug(info); // SingletonManager<> 상속받은 매니저
        GameManager.Instance.inventory.AddItem(info);
    }
    public int GetValue() => info.basePrice;
}
