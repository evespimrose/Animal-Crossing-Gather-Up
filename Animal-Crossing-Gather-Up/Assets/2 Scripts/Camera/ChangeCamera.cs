using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeCamera : MonoBehaviour
{
    public Camera mainCamera; //���� ī�޶�
    private MMFCamera mmfCamera;
    private float changeFOV = 15f; //Ȯ���� FOV
    private float originalFOV = 40f; //���� FOV
    private float duration = 0.5f;

    private void Start()
    {
        mmfCamera = GetComponent<MMFCamera>();
        mmfCamera.currentY = mmfCamera.transform.position.y;
    }

    public void ZoomIn(Transform playerTransform)
    {
        StartCoroutine(ZoomCoroutine(changeFOV, 8f, playerTransform));
    }

    public void ZoomOut(Transform playerTransform)
    {
        StartCoroutine(ZoomCoroutine(originalFOV, -8f, playerTransform)); //y�� ���� �ȵǴ� ��
    }

    private IEnumerator ZoomCoroutine(float targetFOV, float yValue, Transform playerTransform)
    {
        float currentFOV = mainCamera.fieldOfView; 
        Vector3 currentPos = mainCamera.transform.position; 

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            mainCamera.fieldOfView = Mathf.Lerp(currentFOV, targetFOV, elapsedTime / duration);

            mainCamera.transform.position = new Vector3(currentPos.x, mainCamera.transform.position.y + yValue * (elapsedTime / duration), currentPos.z);
            mmfCamera.currentY += yValue * (Time.deltaTime / duration);
            yield return null;
        }

        mainCamera.fieldOfView = targetFOV;
        mainCamera.transform.position = new Vector3(currentPos.x, mainCamera.transform.position.y + yValue, currentPos.z); 
    }

}
