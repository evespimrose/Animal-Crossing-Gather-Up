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
		moneyText = GetComponentInChildren<TextMeshProUGUI>();
	}

	private void Update()
	{
		money = FindObjectOfType<Inventory>().money;
		moneyText.text = money.ToString();
	}
}
