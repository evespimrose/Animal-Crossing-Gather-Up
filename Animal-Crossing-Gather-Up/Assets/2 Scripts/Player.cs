using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

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
    private float gravity = -9.81f;  // �߷� ��
    private Vector3 velocity;

    // test of input item Player to Inventory
    public Item i0;
    public Item i1;
    public Item t0;
    public Item t1;

    private ITool currentTool;

    [Header("For Debug")]
    public GameObject debugTool;

    private HandFlowerCommand handcollectCommand;
    public bool isFishing = false;

    public GameObject EquippedTool => equippedTool;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        if(debugTool != null)
            EquipTool(debugTool);
        handcollectCommand = new HandFlowerCommand();
        isFishing = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleCollection();
        ApplyGravity();
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
			// only optionPanel is not active
			if (UIManager.Instance.GetOptionActive() == false)
			{
				UIManager.Instance.ToggleInventory();
			}
		}
        else if (Input.GetKeyDown(KeyCode.M))
            if (currentTool == null)
                EquipTool(debugTool);
            else
                StartCoroutine(UnequipTool());
    }
	
		

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movement = new Vector3(-vertical, 0, horizontal).normalized;
        if (movement.magnitude > 0.1f)
        {
            characterController.Move(moveSpeed * Time.deltaTime * movement);
            //Vector3 currentPosition = transform.position;
            //transform.position = new Vector3(currentPosition.x, originalY, currentPosition.z);
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
            if(currentTool.ToolInfo.toolType == ToolInfo.ToolType.FishingPole)
                isFishing = true;

            currentTool.Execute(transform.position, transform.forward);

            if (currentTool.ToolInfo.currentDurability <= 0)
            {
                OnItemCollected?.Invoke(currentTool.ToolInfo);

                GameObject toolToDestroy = equippedTool;
                StartCoroutine(UnequipTool());
                Destroy(toolToDestroy);
                // Inventory.DestroyItem();
            }
        }
        else
        {
            handcollectCommand.Execute(transform.position);
        }
    }

    public void Collect(Item item)
    {
        OnItemCollected?.Invoke(item);
    }

    public void CollectWithCeremony(Item collectableInfo)
    {
        // CineMachine Active...
        
        StartCoroutine(CeremonyCoroutine(collectableInfo));
    }

    private IEnumerator CeremonyCoroutine(Item collectableInfo)
    {
        // CineMachine Active...
        yield return new WaitForSeconds(1f);
        Debug.Log($"CeremonyCoroutine : {collectableInfo.itemName}");

        OnItemCollected?.Invoke(collectableInfo);
        yield break;
    }

    private void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        characterController.Move(velocity * Time.deltaTime);
    }

    public void EquipTool(GameObject tool)
    {
        StartCoroutine(EquipToolCoroutine(tool));
    }
    private IEnumerator EquipToolCoroutine(GameObject tool)
    {
        if (equippedTool != null)
        {
            yield return StartCoroutine(UnequipTool());
        }

        GameObject toolInstance = Instantiate(tool, handPosition.position, Quaternion.identity);
        equippedTool = toolInstance;
        equippedTool.transform.SetParent(handPosition);
        equippedTool.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        if (!equippedTool.TryGetComponent(out currentTool))
        {
            Debug.LogWarning("Equipped object does not have a valid tool component.");
        }
    }


    public IEnumerator UnequipTool()
    {
        if (equippedTool != null)
        {
            if (isFishing && equippedTool.TryGetComponent(out FishingPole fishingPole))
            {
                fishingPole.UnExecute();
                yield return new WaitUntil(() => fishingPole.IsDoneFishing);
            }

            ToolInfo toolInfoCopy = currentTool.ToolInfo;
            OnItemCollected?.Invoke(toolInfoCopy); 

            Destroy(equippedTool);
            equippedTool = null;
            currentTool = null;
            toolInfoCopy = null;
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
