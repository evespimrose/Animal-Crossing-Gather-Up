using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickStateController : NPCState
{
    NPCStateType kickState;

    private void Start()
    {
        npcState = kickState;
        moveSpeed = 0.5f;
        currentTarget = RandomWaypoint();
        SetState(NPCStateType.Walk);
    }
    private void Update()
    {
        base.Update();
    }


    protected override Vector3 RandomWaypoint()
    {
        float x = Random.Range(0f, 7f);
        float z = Random.Range(0f, 10f);
        Vector3 myWaypoint = new Vector3(x, 1.5f, z);

        return myWaypoint;

    }
}
