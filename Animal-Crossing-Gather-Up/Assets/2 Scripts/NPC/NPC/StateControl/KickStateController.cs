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
        //print($"ÇöÀç Å± »óÅÂ: {npcState.ToString()}");
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
        float x = Random.Range(0f, 7f);
        float z = Random.Range(0f, 10f);
        Vector3 myWaypoint = new Vector3(x, 0.6f, z);

        return myWaypoint;

    }
}
