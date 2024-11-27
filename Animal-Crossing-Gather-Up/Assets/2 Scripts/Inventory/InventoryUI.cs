using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
	public Inventory inventory; // Reference to the Inventory
	public GameObject inventoryPanel;   // Reference to the inventory UI Panel

	private void Start()
	{
		inventory = FindObjectOfType<Inventory>();
		inventory.OnInventoryFull += InventoryOpen; // Subscribe to the event
		inventoryPanel.SetActive(false);    // Hide the inventoryPanel at start
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (inventoryPanel.activeSelf)
			{
				inventoryPanel.SetActive(false);
			}
		}
	}

	public void InventoryOpen()
	{
		// hierarchy's popup enable true
		inventoryPanel.SetActive(true);
		// key input and cursor move

		// if choose, delegate call

		// but if full, call another delegate
	}
}
