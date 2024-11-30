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

	// ����
	private PurchaseUI purchaseUI;

	private void Start()
	{
		purchaseUI = FindObjectOfType<PurchaseUI>();
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

	public void PurchasePanelOpen()
	{
		purchaseUI.PurchasePanelOpen();
	}

	public void PurchasePanelClose()
	{
		purchaseUI.PurchasePanelClose();
	}
}
