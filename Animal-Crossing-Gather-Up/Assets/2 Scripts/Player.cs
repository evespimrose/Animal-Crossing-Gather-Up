using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{
	private CharacterController characterController;
	public float moveSpeed = 5f;
	public Transform handPosition;
	public GameObject equippedTool;

	public delegate void ItemCollectedHandler(Item item);
	public event ItemCollectedHandler OnItemCollected;

	private ICollectCommand _currentCommand;

	public void SetCommand(ICollectCommand command) { _currentCommand = command; }
	public void Collect() => _currentCommand?.Execute();

	private void Start()
	{
		characterController = GetComponent<CharacterController>();
	}

	private void Update()
	{
		HandleMovement();
		HandleCollection();
		Test();
	}

	// test of input item Player to Inventory
	public Item i0;
	public Item i1;
	public Item t0;
	public Item t1;
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
	}

	private void HandleMovement()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
	}

	private void HandleCollection()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Collect();
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
	}

	// ���?����
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

	// ä���� �Ǹ�
	public void SellItem(GameObject item)
	{
		//if (inventory.RemoveItem(item))
		//{
		//    //money += item.sellPrice;
		//    //Debug.Log($"Sold {item.name} for {item.sellPrice}. Current Money: {money}");
		//}
		//else
		//{
		//    Debug.Log("Item not found in inventory.");
		//}
	}
}
