using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

[RequireComponent(typeof(CharacterController))]
public class Player : SingletonManager<Player>
{
	private CharacterController characterController;
	public float moveSpeed = 5f;
	public Transform handPosition;
	public GameObject equippedTool;

	public delegate void ItemCollectedHandler(Item item);
	public event ItemCollectedHandler OnItemCollected;

	private Vector3 movement;
	private const float V = -9.81f;
	private readonly float gravity = V;
	private Vector3 velocity;
	private bool isRun = false;

	[SerializeField] private Tool currentTool;

	private HandFlowerCommand handcollectCommand;
	public bool isMoving = false;

	private bool IsUIOpen => UIManager.Instance.IsAnyUIOpen();

	public GameObject EquippedTool => equippedTool;
	private Animator animator;
	public AnimReciever animReciever;

	[SerializeField] private ChangeCamera changeCamera;
	private bool isCloseUp = true;

	[Header("¼¼·¹¸Ó´Ï¿ë ÇÁ¸®ÆÕ")]
	[SerializeField] private GameObject squidPrefab;
	[SerializeField] private GameObject clownFishPrefab;
	[SerializeField] private GameObject lobsterPrefab;
	[SerializeField] private GameObject seaHorsePrefab;
	[SerializeField] private GameObject orcaPrefab;
	[SerializeField] private GameObject crabPrefab;
	[SerializeField] private GameObject dolphinPrefab;

	[SerializeField] private GameObject beePrefab;
	[SerializeField] private GameObject beetlePrefab;
	[SerializeField] private GameObject butterflyPrefab;
	[SerializeField] private GameObject blackSpiderPrefab;
	[SerializeField] private GameObject dragonflyPrefab;
	[SerializeField] private GameObject sandSpiderPrefab;

	protected override void Awake()
	{
		base.Awake();
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		string currentScene = SceneManager.GetActiveScene().name;
		if (currentScene == "GameScene")
		{
			characterController.enabled = false;
			transform.position = new Vector3(27.5f, 1f, -73f);
			characterController.enabled = true;
		}
		else if (currentScene == "MileIsland")
		{
			characterController.enabled = false;
			transform.position = new Vector3(136f, 1f, -47f);
			characterController.enabled = true;
		}
	}

	private void Start()
	{
		characterController = GetComponent<CharacterController>();

		if (animator == null)
			animator = GetComponentInChildren<Animator>();

		if (animReciever == null)
			animReciever = GetComponentInChildren<AnimReciever>();

		handcollectCommand = new HandFlowerCommand();
		animReciever.isFishing = false;
	}

	private void Update()
	{
		Move();
		HandleCollection();
		ApplyGravity();
		HandleKeyInput();
	}

	private void HandleKeyInput()
	{
		if (Input.GetKeyDown(KeyCode.I) && !animReciever.isActing && !isMoving)
		{
			if (UIManager.Instance.GetOptionActive() == false)
			{
				UIManager.Instance.ToggleInventory();
			}
		}

		if (Input.GetKeyDown(KeyCode.V))
		{
			if (!isCloseUp)
			{
				isCloseUp = true;

				GameManager.Instance.cam.CloseUp();
			}
			else
			{
				isCloseUp = false;

				GameManager.Instance.cam.TopView();
			}

		}

	}

	private void Move()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		isRun = Input.GetKey(KeyCode.LeftShift);
		animator.SetBool("isRun", isRun);

		movement = new Vector3(-vertical, 0, horizontal).normalized * (isRun ? 2f : 1f);

		if (!IsUIOpen && !animReciever.isActing && !animReciever.isFishing)
		{
			isMoving = true;
			animator.SetFloat("speed", movement.magnitude);

			if (movement.magnitude > 0.1f)
			{
				characterController.Move(moveSpeed * Time.deltaTime * movement);

				transform.forward = movement;
			}
			else if (movement.magnitude <= 0f)
				isMoving = false;
		}
		animReciever.gameObject.transform.localPosition = Vector3.zero;
	}

	private void HandleCollection()
	{
		if (!IsUIOpen && !animReciever.isActing && Input.GetKeyDown(KeyCode.Space))
		{
			Collect();
		}
		if (!IsUIOpen && !animReciever.isActing && !animReciever.isFishing && !isMoving && Input.GetKeyDown(KeyCode.Escape))
			UIManager.Instance.ShowPauseOptionPanel();
	}

	public void Collect()
	{
		if (isMoving || animReciever.isActing) return;

		if (currentTool != null)
		{
			if (currentTool.toolInfo.toolType == ToolInfo.ToolType.FishingPole && equippedTool.TryGetComponent(out FishingPole fPole) && animReciever.isFishing)
			{
				ActivateAnimation(null, false, 3);
				fPole.UnExecute();
				return;
			}
			else
				currentTool.Execute(transform.position, transform.forward);

			if (animator != null && !animReciever.isActing && !isMoving)
			{
				switch (currentTool.toolInfo.toolType)
				{
					case ToolInfo.ToolType.Axe:
						ActivateAnimation("UseAxe");
						break;
					case ToolInfo.ToolType.FishingPole:
						ActivateAnimation("UseFishingPole", true, 0);
						break;
					case ToolInfo.ToolType.BugNet:
						ActivateAnimation("UseBugNet");
						break;
					default:
						ActivateAnimation("Idle");
						break;
				}
			}


			if (currentTool.toolInfo.currentDurability <= 0)
			{
				if (currentTool.toolInfo.toolType == ToolInfo.ToolType.FishingPole && equippedTool.TryGetComponent(out FishingPole fishingPole))
				{
					ActivateAnimation(null, false, 3);
					fishingPole.UnExecute();
				}
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

	public IEnumerator UnequipAndDestroyTool()
	{
		yield return StartCoroutine(UnequipToolCoroutine());
		Destroy(equippedTool);

		equippedTool = null;
		currentTool = null;

		Debug.Log($"equippedTool != NULL!!{(equippedTool != null)}");
	}
	public void CollectItem(Item item)
	{
		OnItemCollected?.Invoke(item);
	}

	private IEnumerator RotateToFaceDirection(Vector3 targetDirection, Item itemInfo)
	{
		if (itemInfo is FishInfo)
		{
			ActivateAnimation(null, true, 2);
			yield return new WaitUntil(() => !animReciever.isActing);
		}

		float rotationSpeed = 5f;
		Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

		JudgeActivationOfPrefabs(itemInfo, true);

		while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
			yield return null;
		}

		transform.rotation = targetRotation;
		ActivateAnimation("ShowOff");
	}

	public IEnumerator CollectItemWithCeremony(Item itemInfo = null)
	{
		yield return StartCoroutine(RotateToFaceDirection(Vector3.right, itemInfo)); // Xï¿?+ë°©í–¥?ï¿½ë¡œ ?ï¿½ì „ ?ï¿½ìž‘

		// CineMachine Coroutine Active...
		yield return StartCoroutine(CeremonyCoroutine(itemInfo));
	}

	private IEnumerator CeremonyCoroutine(Item itemInfo = null)
	{
		// CineMachine Active...
		changeCamera.ZoonIn(transform);
		Debug.Log("CeremonyCoroutine");
		yield return new WaitForSeconds(2.1f);        // Wait for CineMachine's Playtime
		yield return new WaitUntil(() => !animReciever.isActing); // Wait for Animation's End

		//Send itemInfo to inventory
		JudgeActivationOfPrefabs(itemInfo, false);
		changeCamera.ZoomOut(transform);

		OnItemCollected?.Invoke(itemInfo);

	}

	private void ApplyGravity()
	{
		if (!characterController.isGrounded)
		{
			velocity.y += gravity * Time.deltaTime;
		}

		characterController.Move(velocity * Time.deltaTime);
	}

	public void EquipTool(ToolInfo tool)
	{
		StartCoroutine(EquipToolCoroutine(tool));
	}
	private IEnumerator EquipToolCoroutine(ToolInfo tool)
	{
		if (equippedTool != null)
		{
			Debug.Log($"EquipToolCoroutine's equippedTool != NULL!!{(equippedTool != null)}");

			yield return StartCoroutine(UnequipAndDestroyTool());
		}

		ActivateAnimation("Arm");

		GameObject toolInstance = Instantiate(tool.prefab);

		Debug.Log($"toolInstance!!{toolInstance.name}");

		equippedTool = Instantiate(toolInstance);
		Debug.Log($"equippedTool!!{equippedTool.name}");

		if (equippedTool.TryGetComponent(out Tool eqTool))
		{
			Debug.Log($"TryGetComponent!!{eqTool.name}");

			eqTool.toolInfo = Instantiate(tool);
			currentTool = eqTool;
			Debug.Log($"currentTool!!{currentTool.name}, {currentTool.toolInfo.currentDurability}");
		}
		equippedTool.transform.SetParent(handPosition);
		equippedTool.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

		Destroy(toolInstance);
	}
	public void UnequipTool()
	{
		StartCoroutine(UnequipToolCoroutine());
	}
	public IEnumerator UnequipToolCoroutine()
	{
		if (equippedTool != null)
		{
			if (animReciever.isFishing && equippedTool.TryGetComponent(out FishingPole fishingPole))
			{
				fishingPole.UnExecute();
				yield return new WaitUntil(() => fishingPole.isDoneFishing);
			}

			ActivateAnimation("UnArm");
			yield return new WaitUntil(() => !animReciever.isActing);
		}
	}

	public void ActivateAnimation(string str = null, bool isFishing = false, int fishingTaskCount = 0)
	{
		animReciever.isActing = true;
		animReciever.isFishing = isFishing;
		animReciever.fishingTaskCount = fishingTaskCount;
		animator.SetBool("isFishing", isFishing);
		animator.SetInteger("FishingTaskCount", fishingTaskCount);
		if (str != null)
			animator.SetTrigger(str);
	}
	private void JudgeActivationOfPrefabs(Item itemInfo, bool activation)
	{
		if (itemInfo == null)
		{
			Debug.Log($"bugInfo{itemInfo.name}, {activation}");

			return;
		}

		if (itemInfo is BugInfo bugInfo)
		{
			Debug.Log($"bugInfo{bugInfo.name}, {activation}");
			switch (bugInfo.bugName)
			{
				case BugInfo.BugName.Bee:
					beePrefab.SetActive(activation);
					break;
				case BugInfo.BugName.Beetle:
					beetlePrefab.SetActive(activation);
					break;
				case BugInfo.BugName.Butterfly:
					butterflyPrefab.SetActive(activation);
					break;
				case BugInfo.BugName.Dragonfly:
					dragonflyPrefab.SetActive(activation);
					break;
				case BugInfo.BugName.BlackSpider:
					blackSpiderPrefab.SetActive(activation);
					break;
				case BugInfo.BugName.SandSpider:
					sandSpiderPrefab.SetActive(activation);
					break;
			}
		}
		else if (itemInfo is FishInfo fishInfo)
		{
			Debug.Log($"bugInfo{fishInfo.name}, {activation}");

			switch (fishInfo.type)
			{

				case FishInfo.FishType.ClownFish:
					clownFishPrefab.SetActive(activation);
					break;
				case FishInfo.FishType.Lobster:
					lobsterPrefab.SetActive(activation);
					break;
				case FishInfo.FishType.Dolphin:
					dolphinPrefab.SetActive(activation);
					break;
				case FishInfo.FishType.Orca:
					orcaPrefab.SetActive(activation);
					break;
				case FishInfo.FishType.SeaHorse:
					seaHorsePrefab.SetActive(activation);
					break;
				case FishInfo.FishType.Squid:
					squidPrefab.SetActive(activation);
					break;
				case FishInfo.FishType.Crab:
					crabPrefab.SetActive(activation);
					break;
			}
		}

		equippedTool.SetActive(!activation);
	}
}
