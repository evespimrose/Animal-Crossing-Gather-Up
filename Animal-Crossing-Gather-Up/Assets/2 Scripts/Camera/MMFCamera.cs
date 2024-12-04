using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMFCamera : MonoBehaviour
{
    [Header("타겟 설정")]
    [SerializeField] private Transform target; 
    [SerializeField] private Vector3 offset = new Vector3(0f, 5f, -8f); 
    
    [Header("카메라 설정")]
    [SerializeField] private float smoothSpeed = 5f; 
    [SerializeField] private Vector2 heightMinMax = new Vector2(2f, 12f); 
    [SerializeField] private float viewTransitionDuration = 0.5f;
    
    private Vector3 defaultOffset;
    private Vector3 closeUpOffset = new Vector3(6f, 5f, 0f);
    private Vector3 topViewOffset = new Vector3(2f, 7f, 0f);
    private bool isCloseUpView = false;
    
    private float currentXPosition;
    private Vector3 velocity = Vector3.zero;
    private Vector3 currentOffset;
    private bool isTransitioning = false;
    
    private void Start()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        defaultOffset = offset;
        currentOffset = offset;
        Vector3 desiredPosition = target.position + offset;
        currentXPosition = desiredPosition.x;
        transform.position = desiredPosition;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isTransitioning)
        {
            StartCoroutine(TransitionCameraView());
        }
    }
    
    private IEnumerator TransitionCameraView()
    {
        isTransitioning = true;
        Vector3 startOffset = currentOffset;
        Vector3 targetOffset = isCloseUpView ? topViewOffset : closeUpOffset;
        
        float elapsedTime = 0f;
        
        while (elapsedTime < viewTransitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / viewTransitionDuration;

            t = Mathf.SmoothStep(0, 1, t);
            
            currentOffset = Vector3.Lerp(startOffset, targetOffset, t);
            
            yield return null;
        }
        
        currentOffset = targetOffset;
        isCloseUpView = !isCloseUpView;
        isTransitioning = false;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        float targetXPosition = target.position.x;
        currentXPosition = Mathf.SmoothDamp(currentXPosition, targetXPosition, ref velocity.x, 1f / smoothSpeed);

        Vector3 newPosition = new Vector3(currentXPosition, target.position.y, target.position.z) + currentOffset;

        newPosition.y = Mathf.Clamp(newPosition.y, target.position.y + heightMinMax.x, target.position.y + heightMinMax.y);
        
        transform.position = newPosition;
        transform.LookAt(target.position);
    }
}
