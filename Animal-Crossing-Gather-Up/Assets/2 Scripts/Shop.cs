using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
	// 판매
	// 키보드 입력으로 커서 이동

	// 키보드 입력으로 커서 위치의 아이템 선택

	// 선택지 창 오픈

	// 커서 위치의 아이템 이름 표시

	// 구매

	// 도끼, 잠자리채, 낚싯대, 마일섬 티켓

	// SlotUI는 할당된 아이템에 대한 정보 표시만 가능하도록 하면 좋겠는데,.

	// 구매
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
