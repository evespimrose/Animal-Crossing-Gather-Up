using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PascalStateController : NPCState
{
    protected override void Start()
    {
        base.Start();
        moveSpeed = 0.3f; // Pascal의 이동 속도 설정
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
        return GetRandomWaypoint(107f, 115f, -42f, -51f, 0.7f); // Pascal의 랜덤 웨이포인트
    }
}
