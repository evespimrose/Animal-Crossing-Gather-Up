using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeCamera : MonoBehaviour
{
    public Camera mainCamera; //메인 카메라
    public float changeFOV = 25f; //확대할 FOV
    public float originalFOV = 40f; //기존 FOV
    public float duration = 0.5f; 
    public float yValue = 1.5f; 

    public void ZoomIn(Transform playerTransform)
    {
        StartCoroutine(ZoomCoroutine(changeFOV, yValue, playerTransform));
    }

    public void ZoomOut(Transform playerTransform)
    {
        StartCoroutine(ZoomCoroutine(originalFOV, -yValue, playerTransform)); //y값 조정 안되는 중
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

            mainCamera.transform.position = new Vector3(currentPos.x, playerTransform.position.y + yValue * (elapsedTime / duration), currentPos.z);

            yield return null;
        }

        mainCamera.fieldOfView = targetFOV;
        mainCamera.transform.position = new Vector3(currentPos.x, playerTransform.position.y + yValue, currentPos.z); 
    }

}
