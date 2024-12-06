using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
	private int money;
	public GameObject moneyPanel;
	public TextMeshProUGUI moneyText;
	private RectTransform panelTransform;

	private void Start()
	{
		moneyText = GetComponentInChildren<TextMeshProUGUI>();
		panelTransform = moneyPanel.GetComponent<RectTransform>();
		moneyPanel.SetActive(false);
	}

	private void Update()
	{
		if (moneyPanel.activeSelf)
		{
			money = FindObjectOfType<Inventory>().money;
			moneyText.text = money.ToString();
		}
	}

	public void ShowMoney(Vector2 vector2)
	{
		panelTransform.localPosition = vector2;
		moneyPanel.SetActive(true);
	}

	public void HideMoney()
	{
		moneyPanel.SetActive(false);
	}
}
