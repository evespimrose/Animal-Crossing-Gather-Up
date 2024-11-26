using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    public float moveSpeed = 5f;
    public Transform handPosition; // 장비를 장착할 위치
    public GameObject equippedTool; // 현재 장착된 도구
    //public Inventory inventory; // 플레이어의 인벤토리
    public int money; // 보유 중인 돈

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

    // 플레이어 이동 처리
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

    // 장비 장착
    public void EquipTool(GameObject tool)
    {
        if (equippedTool != null)
        {
            UnequipTool(); // 기존 장비를 벗음
        }

        equippedTool = tool;
        equippedTool.transform.SetParent(handPosition); // 손 위치에 장착
        equippedTool.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    // 장비 해제
    public void UnequipTool()
    {
        if (equippedTool != null)
        {
            //inventory.AddItem(equippedTool); // 해제된 장비를 인벤토리에 추가
            equippedTool.transform.SetParent(null); // 장비의 부모 해제
            equippedTool.SetActive(false);
            equippedTool = null;
        }
    }

    public void CollectItem(Item item)
    {
        // 채집 완료 후 이벤트 호출
        OnItemCollected?.Invoke(item);
        //Debug.Log($"Collected {item.itemName}!");
    }

    // 채집물 판매
    public void SellItem(GameObject item)
    {
        //if (inventory.RemoveItem(item)) // 인벤토리에서 아이템 제거 성공 시
        //{
        //    //money += item.sellPrice; // 아이템 판매 가격만큼 돈 증가
        //    //Debug.Log($"Sold {item.name} for {item.sellPrice}. Current Money: {money}");
        //}
        //else
        //{
        //    Debug.Log("Item not found in inventory.");
        //}
    }
}
