using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaisyStateController : NPCState
{
    protected override void Start()
    {
        base.Start();
        moveSpeed = 0.3f; // Daisy의 이동 속도 설정
        currentTarget = RandomWaypoint();
        SetCurrentState(NPCStateType.Walk);
    }

    protected override void HandleTalk()
    {
        base.HandleTalk();
        if (!UIManager.Instance.dialogUI.dialogPanel.activeSelf)
        {
            SetCurrentState(NPCStateType.Walk); // 대화 UI가 비활성화되면 걷기 상태로 전환
        }
    }

    protected override Vector3 RandomWaypoint()
    {
        return GetRandomWaypoint(-8.5f, 3f, -33f, -24f, 0.6f); // Daisy의 랜덤 웨이포인트
    }
}