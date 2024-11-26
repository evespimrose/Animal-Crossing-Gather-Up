using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    public float moveSpeed = 5f;
    public Transform handPosition; // ��� ������ ��ġ
    public GameObject equippedTool; // ���� ������ ����
    //public Inventory inventory; // �÷��̾��� �κ��丮
    public int money; // ���� ���� ��

    private Vector3 movement;

    public delegate void ItemCollectedHandler(Item item);
    public event ItemCollectedHandler OnItemCollected;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleMovement();
    }

    // �÷��̾� �̵� ó��
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

    // ��� ����
    public void EquipTool(GameObject tool)
    {
        if (equippedTool != null)
        {
            UnequipTool(); // ���� ��� ����
        }

        equippedTool = tool;
        equippedTool.transform.SetParent(handPosition); // �� ��ġ�� ����
        equippedTool.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    // ��� ����
    public void UnequipTool()
    {
        if (equippedTool != null)
        {
            //inventory.AddItem(equippedTool); // ������ ��� �κ��丮�� �߰�
            equippedTool.transform.SetParent(null); // ����� �θ� ����
            equippedTool.SetActive(false);
            equippedTool = null;
        }
    }

    public void CollectItem(Item item)
    {
        // ä�� �Ϸ� �� �̺�Ʈ ȣ��
        OnItemCollected?.Invoke(item);
        //Debug.Log($"Collected {item.itemName}!");
    }

    // ä���� �Ǹ�
    public void SellItem(GameObject item)
    {
        //if (inventory.RemoveItem(item)) // �κ��丮���� ������ ���� ���� ��
        //{
        //    //money += item.sellPrice; // ������ �Ǹ� ���ݸ�ŭ �� ����
        //    //Debug.Log($"Sold {item.name} for {item.sellPrice}. Current Money: {money}");
        //}
        //else
        //{
        //    Debug.Log("Item not found in inventory.");
        //}
    }
}
