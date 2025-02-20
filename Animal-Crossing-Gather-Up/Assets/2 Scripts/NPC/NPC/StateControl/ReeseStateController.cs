using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class ReeseStateController : NPCState
{
    protected override void Start()
    {
        base.Start();
        moveSpeed = 0f; // Reese의 이동 속도 설정
        originalRotation = transform.rotation; // 원래 회전 저장
        SetCurrentState(NPCStateType.LookAround);
    }

    protected override void HandleTalk()
    {
        if (UIManager.Instance.dialogUI.dialogPanel.activeSelf)
        {
            base.HandleTalk();
        }
        else
        {
            HandleRotationBackToOriginal(); // 대화 UI가 비활성화되면 원래 방향으로 회전
        }
    }

    protected override Vector3 RandomWaypoint()
    {
        return transform.position; // 현재 위치를 웨이포인트로 사용
    }
}
