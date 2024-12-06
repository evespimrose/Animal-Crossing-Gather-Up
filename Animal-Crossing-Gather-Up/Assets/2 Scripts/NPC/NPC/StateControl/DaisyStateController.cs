using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class DaisyStateController : NPCState
{
    private void Start()
    {
        base.Start();
        moveSpeed = 0.3f;
        currentTarget = RandomWaypoint();
        SetCurrentState(NPCStateType.Walk);
    }
    private void Update()
    {
        base.Update();
        //print($"현재 데이지 상태: {npcState.ToString()}");
    }

    protected override void Talk()
    {
        base.Talk();
        if (!uiManager.dialogPanel.activeSelf)
        {
            SetCurrentState(NPCStateType.Walk);
        }
    }

    protected override Vector3 RandomWaypoint()
    {
        float x = Random.Range(-12f, -19f);
        float z = Random.Range(19f, 24f);
        Vector3 myWaypoint = new Vector3(x, 0.6f, z);

        return myWaypoint;

    }
}
