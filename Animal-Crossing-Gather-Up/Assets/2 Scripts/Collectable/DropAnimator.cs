using System.Collections;
using UnityEngine;

public class DropAnimator : MonoBehaviour
{
    [SerializeField] private float dropHeight = 1f;
    [SerializeField] private float dropDuration = 0.25f;
    [SerializeField] private float bounceRadius = 1f;
    
    private Vector3 groundPosition;
    private Vector3 targetPosition;
    public bool IsAnimating { get; private set; }

    public void StartDropAnimation()
    {
        groundPosition = transform.position;
        CalculateTargetPosition();
        StartCoroutine(PlayAnimation());
    }

    private void CalculateTargetPosition()
    {
        float randomAngle = Random.Range(0f, 360f);
        float randomRadius = Random.Range(0f, bounceRadius);
        Vector3 randomOffset = new Vector3(Mathf.Cos(randomAngle * Mathf.Deg2Rad) * randomRadius, 0f, Mathf.Sin(randomAngle * Mathf.Deg2Rad) * randomRadius);
        
        targetPosition = groundPosition + randomOffset;
    }

    public IEnumerator PlayAnimation()
    {
        IsAnimating = true;
        Vector3 startPos = groundPosition + Vector3.up * dropHeight;
        transform.position = startPos;
        
        float elapsedTime = 0f;

        while (elapsedTime < dropDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / dropDuration;

            Vector3 horizontalPos = Vector3.Lerp(new Vector3(startPos.x, 0, startPos.z), new Vector3(targetPosition.x, 0, targetPosition.z), t);
            
            float height = Mathf.Lerp(dropHeight, 0f, t * t);
            transform.position = new Vector3(horizontalPos.x, groundPosition.y + height, horizontalPos.z);
            
            yield return null;
        }
        
        transform.position = targetPosition;
        IsAnimating = false;
    }
} 