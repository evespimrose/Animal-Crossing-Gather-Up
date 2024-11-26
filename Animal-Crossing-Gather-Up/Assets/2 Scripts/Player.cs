using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    public float moveSpeed = 5f;
    public Transform handPosition; 
    public GameObject equippedTool;
    //public Inventory inventory; 
    //public int money;

    private Vector3 movement;

    public delegate void ItemCollectedHandler(Item item);
    public event ItemCollectedHandler OnItemCollected;

     private ICollectCommand _currentCommand;

    public void SetCommand(ICollectCommand command) { _currentCommand = command; }
    public void Collect() => _currentCommand?.Execute();

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        OnItemCollected += AddItem;
    }

    private void Update()
    {
        HandleMovement();
        HandleCollection();
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
        //OnItemCollected?.Invoke(item);
        //invetory.OnItemCollected?.Invoke(item);
        //Debug.Log($"Collected {item.itemName}!");
    }

    public void AddItem(Item item)
    {
        //inventory.AddItem(item);
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
