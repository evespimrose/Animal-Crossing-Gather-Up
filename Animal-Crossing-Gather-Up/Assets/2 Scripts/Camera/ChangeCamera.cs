using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public Camera mainCamera;
    private MMFCamera mmf;
    private float changeFOV = 15f;
    private float originalFOV = 40f;
    private float duration = 0.5f;

    public void ZoonIn(Transform transform)
    {
        StartCoroutine(ZoomCoroutine(changeFOV, 8f, transform));
    }

    public void ZoomOut(Transform transform)
    {
        StartCoroutine(ZoomCoroutine(originalFOV, -8f, transform));
    }

    private IEnumerator ZoomCoroutine(float targetFOV, float Y, Transform playerTransform)
    {
        float currentFOV = mainCamera.fieldOfView;
        Vector3 currentPOS = mainCamera.transform.position;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            mainCamera.fieldOfView = Mathf.Lerp(currentFOV, targetFOV, elapsedTime / duration);

            mainCamera.transform.position = new Vector3(currentPOS.x, Y, currentPOS.z);

            mmf.currentY = Y;
            yield return null;
        }

        mainCamera.fieldOfView = targetFOV;
        mainCamera.transform.position = new Vector3(currentPOS.x, Y, currentPOS.z);
    }
}
