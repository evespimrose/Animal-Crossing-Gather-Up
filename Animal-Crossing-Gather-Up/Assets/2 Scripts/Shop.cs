using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
	// �Ǹ�
	// Ű���� �Է����� Ŀ�� �̵�

	// Ű���� �Է����� Ŀ�� ��ġ�� ������ ����

	// ������ â ����

	// Ŀ�� ��ġ�� ������ �̸� ǥ��

	// ����

	// ����, ���ڸ�ä, ���˴�, ���ϼ� Ƽ��

	// SlotUI�� �Ҵ�� �����ۿ� ���� ���� ǥ�ø� �����ϵ��� �ϸ� ���ڴµ�,.

	// ����Ʈ ���⼭ ���� so ���� �Ҵ��ϰ� �������� �� �����ؾ���
	private List<Slot> purchaseSlots = new List<Slot>(4);
	public List<Item> items = new List<Item>();

	// ����
	private PurchaseUI purchaseUI;

	private void Start()
	{
		purchaseUI = FindObjectOfType<PurchaseUI>();
		for (int i = 0; i < purchaseSlots.Count; i++)
		{
			purchaseSlots[i].Initialize(items[i]);
			print(purchaseSlots[i]);
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.J))
		{
			PurchasePanelOpen();
		}
		else if (Input.GetKeyDown(KeyCode.Escape))
		{
			PurchasePanelClose();
		}
	}

	public List<Slot> GetPurchaseSlotInfo()
	{
		List<Slot> newSlots = new List<Slot>(purchaseSlots);
		return newSlots;
	}

	public void PurchasePanelOpen()
	{
		purchaseUI.PurchasePanelOpen();
	}

	public void PurchasePanelClose()
	{
		purchaseUI.PurchasePanelClose();
	}
}
