using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using UnityEngine;

public class MoriStateController : NPCState
{

    private void Awake()
    {
    }
    protected override void Start()
    {
        base.Start();
        moveSpeed = 0f;
        SetCurrentState(NPCStateType.LookAround);
    }
    protected override void Update()
    {
        base.Update();
        print($"현재 모리 상태: {npcState.ToString()}");
    }

    protected override void Talk()
    {
        base.Talk();
        if (!uiManager.dialogPanel.activeSelf)
        {
            Quaternion.LookRotation(Vector3.forward, Vector3.up);
            anim.SetBool("Talk", false);
            Task.Delay(2000);
            SetCurrentState(NPCStateType.LookAround);
        }
    }

    protected override Vector3 RandomWaypoint()
    {
        Vector3 myWaypoint = transform.position;
        return myWaypoint;
    }
}
