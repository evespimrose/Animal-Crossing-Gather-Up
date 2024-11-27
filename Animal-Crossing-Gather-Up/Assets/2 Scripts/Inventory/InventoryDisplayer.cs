using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplayer : SingletonManager<InventoryDisplayer>
{
	public GameObject inventoryUI;

	private void Start()
	{
		inventoryUI.SetActive(false);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (inventoryUI.activeSelf)
			{
				inventoryUI.SetActive(false);
			}
		}
	}

	public void InventoryOpen()
	{
		// hierarchy's popup enable true
		inventoryUI.SetActive(true);
		// key input and cursor move

		// if choose, delegate call

		// but if full, call another delegate
	}
}
