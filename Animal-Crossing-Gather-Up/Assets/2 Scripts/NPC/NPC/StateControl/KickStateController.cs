using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickStateController : NPCState
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
        float x = Random.Range(115f, 123f);
        float z = Random.Range(-19f, -29f);
        Vector3 myWaypoint = new Vector3(x, 0.7f, z);

        return myWaypoint;

    }
}
