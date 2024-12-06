using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDori : MonoBehaviour
{
	private Inventory inventory;
	public string[] shopOptionTexts = { "팔고 싶어!", "물건을 보여줘!", "아무것도 아냐" };
	private bool isSelecting = false;
	private string selectedOption = "";

	private void Start()
	{
		inventory = FindObjectOfType<Inventory>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.U))
		{
			isSelecting = true;
			UIManager.Instance.ShowOptions(shopOptionTexts);
			StartCoroutine(WaitForSelectEndCoroutine());
		}
		else if (Input.GetKeyDown(KeyCode.J))
		{
			UIManager.Instance.OpenPurchasePanel();
		}
		else if (Input.GetKeyDown(KeyCode.K))
		{
			UIManager.Instance.ClosePurchasePanel();
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
			else if (selectedOption == "팔고 싶어!")
			{
				UIManager.Instance.OpenSellPanel();
				isSelecting = false;
				selectedOption = "";
			}
			else if (selectedOption == "물건을 보여줘!")
			{
				UIManager.Instance.OpenPurchasePanel();
				isSelecting = false;
				selectedOption = "";
			}
			else if (selectedOption == "아무것도 아냐")
			{
				isSelecting = false;
				selectedOption = "";
			}
		}
	}
}
