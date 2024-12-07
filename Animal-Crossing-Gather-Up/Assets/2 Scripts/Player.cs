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
    private bool isRun = false;

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

    private bool IsUIOpen => UIManager.Instance.IsAnyUIOpen();

    public GameObject EquippedTool => equippedTool;
    private Animator animator;
    private AnimReciever animReciever;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        animReciever = GetComponentInChildren<AnimReciever>();
        if (debugTool != null)
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
                UnequipTool();
        else if (Input.GetKeyDown(KeyCode.L))
        {
            CollectItemWithCeremony();
        }
    }
	
		

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        isRun = Input.GetKey(KeyCode.LeftShift);
        animator.SetBool("Run", isRun);

        movement = new Vector3(-vertical, 0, horizontal).normalized * (isRun? 2f : 1f);

        if (!IsUIOpen && !animReciever.isActing)
        {
            animator.SetFloat("speed", movement.magnitude);

            if (movement.magnitude > 0.1f)
            {
                characterController.Move(moveSpeed * Time.deltaTime * movement);

                transform.forward = movement;
            }
        }
    }

    private void HandleCollection()
    {
        if (!IsUIOpen && !animReciever.isActing && Input.GetKeyDown(KeyCode.Space))
        {
            Collect();
        }
    }

    public void Collect()
    {
        if (currentTool != null)
        {
            if (animator != null && !animReciever.isActing)
            {
                switch (currentTool.ToolInfo.toolType)
                {
                    case ToolInfo.ToolType.Axe:
                        ActivateAnimation("UseAxe");
                        break;
                    case ToolInfo.ToolType.FishingPole:
                        ActivateAnimation("UseFishingPole");
                        break;
                    case ToolInfo.ToolType.BugNet:
                        ActivateAnimation("UseBugNet");
                        break;
                    default:
                        ActivateAnimation("Idle");
                        break;
                }
            }

            if (currentTool.ToolInfo.toolType == ToolInfo.ToolType.FishingPole)
                isFishing = true;

            currentTool.Execute(transform.position, transform.forward);

            if (currentTool.ToolInfo.currentDurability <= 0)
            {
                if (currentTool.ToolInfo.toolType == ToolInfo.ToolType.FishingPole && equippedTool.TryGetComponent(out FishingPole fishingPole))
                {
                    isFishing = false;
                    fishingPole.UnExecute();
                }

                OnItemCollected?.Invoke(currentTool.ToolInfo);
                StartCoroutine(UnequipAndDestroyTool(equippedTool));
            }
        }
        else
        {
            if (animator != null && !animReciever.isActing)
            {
                handcollectCommand.Execute(transform.position);
                ActivateAnimation("ItemPickUp");
            }
        }
    }

    private IEnumerator UnequipAndDestroyTool(GameObject toolToDestroy)
    {
        yield return StartCoroutine(UnequipToolCoroutine());
        Destroy(toolToDestroy);
    }
    public void CollectItem(Item item)
    {
        OnItemCollected?.Invoke(item);
    }

    private IEnumerator RotateToFaceDirection(Vector3 targetDirection)
    {
        float rotationSpeed = 5f;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            yield return null;
        }

        transform.rotation = targetRotation;
    }

    public void CollectItemWithCeremony(Item itemInfo = null)
    {
        StartCoroutine(RotateToFaceDirection(Vector3.right)); // X축 +방향으로 회전 시작
        ActivateAnimation("ShowOff");

        // CineMachine Coroutine Active...
        StartCoroutine(CeremonyCoroutine(itemInfo));
    }

    private IEnumerator CeremonyCoroutine(Item itemInfo = null)
    {
        // CineMachine Active...
        yield return new WaitForSeconds(1f);        // Wait for CineMachine's Playtime
        yield return new WaitUntil(() => !animReciever.isActing); // Wait for Animation's End
        Debug.Log($"CeremonyCoroutine : {itemInfo.itemName}");

        //Send itemInfo to inventory
        OnItemCollected?.Invoke(itemInfo);
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
            yield return StartCoroutine(UnequipToolCoroutine());
        }

        ActivateAnimation("Arm");

        GameObject toolInstance = Instantiate(tool, handPosition.position, Quaternion.identity);
        equippedTool = toolInstance;
        equippedTool.transform.SetParent(handPosition);
        equippedTool.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        if (!equippedTool.TryGetComponent(out currentTool))
        {
            Debug.LogWarning("Equipped object does not have a valid tool component.");
        }
    }


    public void UnequipTool()
    {
        StartCoroutine(UnequipToolCoroutine());
    }
    public IEnumerator UnequipToolCoroutine()
    {
        if (equippedTool != null)
        {
            if (isFishing && equippedTool.TryGetComponent(out FishingPole fishingPole))
            {
                fishingPole.UnExecute();
                yield return new WaitUntil(() => fishingPole.isDoneFishing);
            }

            ActivateAnimation("UnArm");

            ToolInfo toolInfoCopy = currentTool.ToolInfo;
            OnItemCollected?.Invoke(toolInfoCopy);

            Destroy(equippedTool);
            equippedTool = null;
            currentTool = null;
            toolInfoCopy = null;
        }
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

    private void ActivateAnimation(string str)
    {
        animReciever.isActing = true;
        animator.SetTrigger(str);
    }
}
