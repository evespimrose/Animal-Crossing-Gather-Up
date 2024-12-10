using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class DaisyStateController : NPCState
{
    protected override void Start()
    {
        base.Start();
        moveSpeed = 0.3f;
        currentTarget = RandomWaypoint();
        SetCurrentState(NPCStateType.Walk);
    }
    protected override void Update()
    {
        base.Update();
    }

    protected override void Talk()
    {
        base.Talk();
        if (!UIManager.Instance.dialogUI.dialogPanel.activeSelf)
        {
            SetCurrentState(NPCStateType.Walk);
        }
    }

    protected override Vector3 RandomWaypoint()
    {
        float x = Random.Range(-2f, -22f);
        float z = Random.Range(-18f, -33f);
        Vector3 myWaypoint = new Vector3(x, 0.6f, z);

        return myWaypoint;

    }
}
