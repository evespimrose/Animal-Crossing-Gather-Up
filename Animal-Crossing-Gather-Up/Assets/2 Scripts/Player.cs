using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private CharacterController characterController;
	public float moveSpeed = 5f;
	public Transform handPosition;
	public GameObject equippedTool;

	public delegate void ItemCollectedHandler(Item item);
	public event ItemCollectedHandler OnItemCollected;

    private Vector3 movement;

    private ICollectCommand _currentCommand;

    // test of input item Player to Inventory
    public Item i0;
    public Item i1;
    public Item t0;
    public Item t1;

    private Dictionary<string, ICollectCommand> _commands = new Dictionary<string, ICollectCommand>();

    public Tool CurrentTool;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        InitializeCommands();
    }

    private void InitializeCommands()
    {
        _commands["Net"] = new NetCollectCommand();
        _commands["FishingRod"] = new FishingRodCollectCommand();
        _commands["Axe"] = new AxeCollectCommand();
    }

    public void SetCommand(string commandName)
    {
        if (_commands.TryGetValue(commandName, out var command))
        {
            _currentCommand = command;
        }
        else
        {
            Debug.LogWarning($"Command {commandName} not found.");
        }
    }

    public void Collect() => _currentCommand?.Execute();

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

        movement = new Vector3(horizontal, 0, vertical);
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
			if (_currentCommand != null)
			{
				Collect();
			}
			else
			{
				Debug.LogWarning("No command set.");
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

		CurrentTool = tool.GetComponent<Tool>();
		UpdateCollectCommand();
	}

	private void UpdateCollectCommand()
	{
		if (CurrentTool is Axe)
		{
			SetCommand("Axe");
		}
		else if (CurrentTool is FishingRod)
		{
			SetCommand("FishingRod");
		}
		else if (CurrentTool is InsectNet)
		{
			SetCommand("Net");
		}
		else
		{
			_currentCommand = null;
			Debug.LogWarning("Unknown tool equipped.");
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
		//    //Debug.Log($"Sold {item.name} for {item.sellPrice}. Current Money: {money}");
		//}
		//else
		//{
		//    Debug.Log("Item not found in inventory.");
		//}
	}
}
