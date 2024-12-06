using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDori : MonoBehaviour
{
	private Inventory inventory;

	private void Start()
	{
		inventory = FindObjectOfType<Inventory>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Keypad0))
		{
			inventory.RemoveItemOne(0);
		}
		else if (Input.GetKeyDown(KeyCode.Keypad1))
		{
			inventory.RemoveItemOne(1);
		}
		else if (Input.GetKeyDown(KeyCode.Keypad2))
		{
			inventory.RemoveItemOne(2);
		}
	}
}
