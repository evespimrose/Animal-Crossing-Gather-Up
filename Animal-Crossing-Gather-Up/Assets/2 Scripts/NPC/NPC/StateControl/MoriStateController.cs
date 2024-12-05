using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class MoriStateController : NPCState
{
    private void Start()
    {
        base.Start();
        moveSpeed = 0f;
        SetCurrentState(NPCStateType.Idle);
    }
    private void Update()
    {
        base.Update();
        //print($"현재 모리 상태: {npcState.ToString()}");
    }

    protected override void Talk()
    {
        base.Talk();
        if (uiManager.enterPanel!.activeSelf)
        {
            SetCurrentState(NPCStateType.Idle);
        }
    }

    protected override Vector3 RandomWaypoint()
    {
        Vector3 myWaypoint = transform.position;
        return myWaypoint;
    }
}
