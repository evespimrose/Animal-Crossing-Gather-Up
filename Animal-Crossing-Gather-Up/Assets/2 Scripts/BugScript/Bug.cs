using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
   //버그 인포 참조
   //버그 ibugmanger

    private BugInfo info;
    private IBugManager manager;

    public void Initialize(BugInfo buginfom, IBugManager bugManager)
    {
        info = buginfom;
        manager = bugManager;
    }

    private void OnMouseDown()
    {
        manager?.CatchBug(this);
        Destroy(gameObject);                    
    }
    public int GetValue() => info.basePrice;
}
