using System.Collections;
using UnityEngine;

public class Branch : MonoBehaviour, ICollectable
{
    [Header("드롭 설정")]
    [SerializeField] private float dropHeight = 1f;      
    [SerializeField] private float dropDuration = 0.25f;
    [SerializeField] private float bounceRadius = 1f;
    [SerializeField] private float bounceDuration = 0.15f;
    
    private BranchInfo branchInfo;
    private Vector3 groundPosition;
    private Vector3 targetPosition;
    private bool isDropping = false;
    
    public void Spawn(BranchInfo bInfo)
    {
        branchInfo = bInfo;
        groundPosition = transform.position;
        
        float randomAngle = Random.Range(0f, 360f);
        float randomRadius = Random.Range(0f, bounceRadius);
        Vector3 randomOffset = new Vector3(Mathf.Cos(randomAngle * Mathf.Deg2Rad) * randomRadius, 0f, Mathf.Sin(randomAngle * Mathf.Deg2Rad) * randomRadius);
        
        targetPosition = groundPosition + randomOffset;
        transform.position = groundPosition + Vector3.up * dropHeight;
        
        StartCoroutine(DropAnimation());
    }
    
    private IEnumerator DropAnimation()
    {
        isDropping = true;
        Vector3 startPos = transform.position;
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
        isDropping = false;
    }
    
    public void Collect()
    {
        if (isDropping) return;
        
        Debug.Log("Branch collected.");
        GameManager.Instance.inventory.AddItem(branchInfo);
        Destroy(gameObject);
    }
}
