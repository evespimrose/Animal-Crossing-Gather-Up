using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMFCamera : SingletonManager<MMFCamera>
{
	[Header("Target Settings")]
	[SerializeField] private Transform target;
	[SerializeField] private Vector3 offset = new Vector3(8f, 5f, 0f);

	[Header("Camera Settings")]
	[SerializeField] private float smoothSpeed = 5f;
	[SerializeField] private Vector2 heightMinMax = new Vector2(2f, 12f);
	[SerializeField] private float viewTransitionDuration = 0.5f;
	[SerializeField] private Vector3 lookAtPosition;
	[SerializeField] private float lookAtOffset;
	[SerializeField] private float defaultLookAtOffset = 0f;
	[SerializeField] private float inventoryLookAtOffset = 4f;

	private Vector3 defaultOffset;
	private Vector3 closeUpOffset = new Vector3(8f, 5f, 0f);
	private Vector3 topViewOffset = new Vector3(2f, 7f, 0f);
	private Vector3 inventoryViewOffset = new Vector3(5f, 8f, 0f);

	private float currentXPosition;
	private Vector3 velocity = Vector3.zero;
	[SerializeField] private Vector3 currentOffset;

	// Added for adjustable Y position
	public float currentY;

	public bool isTransitioning = false;

	private void Start()
	{
		if (target == null)
		{
			target = GameObject.FindGameObjectWithTag("Player").transform;
		}

		defaultOffset = offset;
		currentOffset = offset;
		lookAtOffset = 0f;
		Vector3 desiredPosition = target.position + offset;
		currentXPosition = desiredPosition.x;
		currentY = target.position.y + offset.y; // Initialize currentY
		transform.position = desiredPosition;
	}

	private void Update()
	{
		if (target == null)
		{
			target = GameObject.FindGameObjectWithTag("Player").transform;
		}

		if (Input.GetKeyDown(KeyCode.T) && !isTransitioning)
		{
			StartCoroutine(TransitionTopCameraView());
		}
		if (Input.GetKeyDown(KeyCode.I) && !isTransitioning)
		{
			StartCoroutine(TransitionInventoryCameraView());
		}
		if (Input.GetKeyDown(KeyCode.C) && !isTransitioning)
		{
			StartCoroutine(TransitionCloseUpCameraView());
		}
	}

	public void TopView()
	{
		StartCoroutine(TransitionTopCameraView());
	}

	public void CloseUp()
	{
		StartCoroutine(TransitionCloseUpCameraView());
	}

	public void InventoryView()
	{
		StartCoroutine(TransitionInventoryCameraView());
	}

	private IEnumerator TransitionTopCameraView()
	{
		isTransitioning = true;

		Vector3 startOffset = currentOffset;
		Vector3 targetOffset = topViewOffset;

		float startLookAtOffset = lookAtOffset;
		float targetLookAtOffset = defaultLookAtOffset;

		float elapsedTime = 0f;

		while (elapsedTime < viewTransitionDuration)
		{
			elapsedTime += Time.deltaTime;
			float t = elapsedTime / viewTransitionDuration;

			t = Mathf.SmoothStep(0, 1, t);

			currentOffset = Vector3.Lerp(startOffset, targetOffset, t);
			lookAtOffset = Mathf.Lerp(startLookAtOffset, targetLookAtOffset, t);

			yield return null;
		}

		currentOffset = targetOffset;
		lookAtOffset = targetLookAtOffset;
		isTransitioning = false;
	}

	private IEnumerator TransitionInventoryCameraView()
	{
		isTransitioning = true;

		Vector3 startOffset = currentOffset;
		Vector3 targetOffset = inventoryViewOffset;

		float startLookAtOffset = lookAtOffset;
		float targetLookAtOffset = inventoryLookAtOffset;

		float elapsedTime = 0f;

		while (elapsedTime < viewTransitionDuration)
		{
			elapsedTime += Time.deltaTime;
			float t = elapsedTime / viewTransitionDuration;

			t = Mathf.SmoothStep(0, 1, t);

			currentOffset = Vector3.Lerp(startOffset, targetOffset, t);
			lookAtOffset = Mathf.Lerp(startLookAtOffset, targetLookAtOffset, t);

			yield return null;
		}

		currentOffset = targetOffset;
		lookAtOffset = targetLookAtOffset;
		isTransitioning = false;
	}

	private IEnumerator TransitionCloseUpCameraView()
	{
		isTransitioning = true;

		Vector3 startOffset = currentOffset;
		Vector3 targetOffset = closeUpOffset;

		float startLookAtOffset = lookAtOffset;
		float targetLookAtOffset = defaultLookAtOffset;

		float elapsedTime = 0f;

		while (elapsedTime < viewTransitionDuration)
		{
			elapsedTime += Time.deltaTime;
			float t = elapsedTime / viewTransitionDuration;

			t = Mathf.SmoothStep(0, 1, t);

			currentOffset = Vector3.Lerp(startOffset, targetOffset, t);
			lookAtOffset = Mathf.Lerp(startLookAtOffset, targetLookAtOffset, t);

			yield return null;
		}

		currentOffset = targetOffset;
		lookAtOffset = targetLookAtOffset;
		isTransitioning = false;
	}

	private void LateUpdate()
	{
		if (target == null) return;

		float targetXPosition = target.position.x;
		currentXPosition = Mathf.SmoothDamp(currentXPosition, targetXPosition, ref velocity.x, 1f / smoothSpeed);

		Vector3 newPosition = new Vector3(currentXPosition, currentY, target.position.z) + currentOffset;

		// Allow `currentY` to be controlled externally without clamping
		newPosition.y = currentY;

		transform.position = newPosition;
		lookAtPosition = target.position + new Vector3(0f, lookAtOffset, 0f);
		transform.LookAt(lookAtPosition);
	}
}
