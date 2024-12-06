using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
	private int money;
	public TextMeshProUGUI moneyText;

	private void Start()
	{
		money = FindObjectOfType<Inventory>().money;
		moneyText = GetComponentInChildren<TextMeshProUGUI>();
	}

	private void Update()
	{
		moneyText.text = money.ToString();
	}
}
