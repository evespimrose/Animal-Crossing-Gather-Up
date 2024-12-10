using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class MoriStateController : NPCState
{
    private Quaternion originalRotation;

    private void Awake()
    {

    }
    protected override void Start()
    {
        base.Start();
        moveSpeed = 0f;
        originalRotation = this.transform.rotation;
        SetCurrentState(NPCStateType.LookAround);
    }
    protected override void Update()
    {
        base.Update();
    }

    protected override void Talk()
    {
        if (UIManager.Instance.dialogUI.dialogPanel.activeSelf)
        {
            base.Talk();
        }

        if (!UIManager.Instance.dialogUI.dialogPanel.activeSelf)
        {
            anim.SetBool("Talk", false);
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, rotateToOriginalSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, originalRotation) < 0.1f)
            {
                SetCurrentState(NPCStateType.LookAround);
            }

        }
    }

    protected override Vector3 RandomWaypoint()
    {
        Vector3 myWaypoint = transform.position;
        return myWaypoint;
    }
}
