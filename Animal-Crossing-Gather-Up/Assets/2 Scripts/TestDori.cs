using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDori : MonoBehaviour
{
	private Inventory inventory;
	public string[] shopOptionTexts = { "�Ȱ� �;�!", "������ ������!", "�ƹ��͵� �Ƴ�" };
	private bool isSelecting = false;
	private string selectedOption = "";

	private void Start()
	{
		inventory = FindObjectOfType<Inventory>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.U) && !UIManager.Instance.IsAnyUIOpen())
		{
			isSelecting = true;
			UIManager.Instance.ShowOptions(shopOptionTexts);
			StartCoroutine(WaitForSelectEndCoroutine());
		}
		else if (Input.GetKeyDown(KeyCode.Escape))
		{
			UIManager.Instance.CloseSellPanel();
			UIManager.Instance.ClosePurchasePanel();
			//UIManager.Instance.CloseOptions();
		}
	}

	private IEnumerator WaitForSelectEndCoroutine()
	{
		while (isSelecting)
		{
			selectedOption = UIManager.Instance.GetSelectedOption();
			UIManager.Instance.SetSelectedOptionInit();

			if (selectedOption == "")
			{
				yield return new WaitForEndOfFrame();
			}
			else if (selectedOption == "�Ȱ� �;�!")
			{
				UIManager.Instance.OpenSellPanel();
				isSelecting = false;
				selectedOption = "";
			}
			else if (selectedOption == "������ ������!")
			{
				UIManager.Instance.OpenPurchasePanel();
				isSelecting = false;
				selectedOption = "";
			}
			else if (selectedOption == "�ƹ��͵� �Ƴ�")
			{
				isSelecting = false;
				selectedOption = "";
			}
		}
	}
}
