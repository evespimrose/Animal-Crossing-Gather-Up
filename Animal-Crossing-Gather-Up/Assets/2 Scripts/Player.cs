using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
	private CharacterController characterController;
	public float moveSpeed = 5f;
	public Transform handPosition;
	public GameObject equippedTool;

	public delegate void ItemCollectedHandler(Item item);
	public event ItemCollectedHandler OnItemCollected;

	private Vector3 movement;

	// test of input item Player to Inventory
	public Item i0;
	public Item i1;
	public Item t0;
	public Item t1;

	private ITool currentTool;

	private void Start()
	{
		characterController = GetComponent<CharacterController>();
		inventoryUI = FindObjectOfType<InventoryUI>();
	}

	// test : inventory Open
	public InventoryUI inventoryUI;

	private void Update()
	{
		HandleMovement();
		HandleCollection();
		Test();
	}

	private void Test()
	{
		if (Input.GetKeyDown(KeyCode.Z))
		{
			CollectItem(i0);
		}
		else if (Input.GetKeyDown(KeyCode.X))
		{
			CollectItem(i1);
		}
		else if (Input.GetKeyDown(KeyCode.C))
		{
			CollectItem(t0);
		}
		else if (Input.GetKeyDown(KeyCode.V))
		{
			CollectItem(t1);
		}
		else if (Input.GetKeyDown(KeyCode.I))
		{
			inventoryUI.InventoryOpen();
		}
	}

	private void HandleMovement()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		movement = new Vector3(-vertical, 0, horizontal);
		if (movement.magnitude > 0.1f)
		{
			characterController.Move(moveSpeed * Time.deltaTime * movement);
			transform.forward = movement;
		}
	}

	private void HandleCollection()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Collect();
		}
	}

	public void Collect()
	{
		if (currentTool != null)
		{
			currentTool.Execute();
			
			if (currentTool.ToolInfo.currentDurability <= 0)
			{
				// 인벤토리에서 아이템 제거
				OnItemCollected?.Invoke(currentTool.ToolInfo);
				
				// 도구 해제 및 파괴
				GameObject toolToDestroy = equippedTool;
				UnequipTool();
				Destroy(toolToDestroy);
			}
		}
	}

	public void EquipTool(GameObject tool)
	{
		if (equippedTool != null)
		{
			UnequipTool();
		}

		equippedTool = tool;
		equippedTool.transform.SetParent(handPosition);
		equippedTool.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

		currentTool = equippedTool.GetComponent<ITool>();
		if (currentTool == null)
		{
			Debug.LogWarning("Equipped object does not have a valid tool component.");
		}
	}

	public void UnequipTool()
	{
		if (equippedTool != null)
		{
			//inventory.AddItem(equippedTool); 
			equippedTool.transform.SetParent(null);
			equippedTool.SetActive(false);
			equippedTool = null;
		}
	}

	public void CollectItem(Item item)
	{
		OnItemCollected?.Invoke(item);
		//invetory.OnItemCollected?.Invoke(item);
		//Debug.Log($"Collected {item.itemName}!");
	}

	// 
	public void SellItem(GameObject item)
	{
		//if (inventory.RemoveItem(item))
		//{
		//    //money += item.sellPrice;
		//}
		//else
		//{
		//    Debug.Log("Item not found in inventory.");
		//}
	}
}
