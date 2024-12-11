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

    public void Collect()
    {
        GetValue();

        GameManager.Instance.RemoveBug(info); // SingletonManager<> ��ӹ���?�Ŵ���

        BugInfo bInfo = info;

        bInfo.basePrice += Random.Range(-1, info.basePrice);

        StartCoroutine(WaitForActingAndCollectCoroutine(bInfo));

        Destroy(gameObject);
    }
    public int GetValue() => info.basePrice;

    private IEnumerator WaitForActingAndCollectCoroutine(BugInfo bInfo)
    {
        yield return new WaitUntil(() => !GameManager.Instance.player.animReciever.isActing);

        GameManager.Instance.player.CollectItemWithCeremony(bInfo);
    }
}
