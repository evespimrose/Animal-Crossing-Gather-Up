using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour, ICollectable
{
	//���� ���� ����
	//���� ibugmanger

	private BugInfo info;
	[SerializeField] private GameObject renderPrefab;

	public void Initialize(BugInfo buginfom)
	{
		info = buginfom;

	}

	private void OnDestroy()
	{
		Debug.Log("BugDestroy");
	}

	public void Collect()
	{
		GetValue();

		GameManager.Instance.RemoveBug(info); // SingletonManager<> ��ӹ���?�Ŵ���

		StartCoroutine(WaitForActingAndCollectCoroutine(info));
	}
	public int GetValue() => info.basePrice;

	private IEnumerator WaitForActingAndCollectCoroutine(BugInfo bInfo)
	{
		yield return new WaitUntil(() => !GameManager.Instance.player.animReciever.isActing);
		Debug.Log("WaitForActingAndCollectCoroutine");

		BugInfo bugInfo = bInfo;
		renderPrefab.SetActive(false);
		yield return StartCoroutine(GameManager.Instance.player.CollectItemWithCeremony(bugInfo));

		Destroy(gameObject);

	}
}
