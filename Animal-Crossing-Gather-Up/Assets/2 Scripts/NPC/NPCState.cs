using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum NPCStateType
{
    Idle,
    Walk,
    Talk,
    Happy,
    Dance,
}
public abstract class NPCState : MonoBehaviour, INPCState
{

    protected List<Vector3> waypoints = new List<Vector3>();
    protected int currentWaypointIndex = 0;
    protected Animator anim;
    protected float rotateSpeed = 1.5f;
    protected float rotateToPlayerSpeed = 3f;
    public Transform player;

    protected NPCPanelUI uiManager;
    private IDialogState dialogState;
    protected NPCStateType npcState;
    protected Vector3 currentTarget;
    protected float moveSpeed;

    protected virtual void Start()
    {
        uiManager = FindObjectOfType<NPCPanelUI>();
        anim = GetComponent<Animator>();
        dialogState = GetComponent<IDialogState>();
        npcState = NPCStateType.Idle; //idle로 기본 설정
    }

    protected virtual void Update()
    {
        switch (npcState)
        {
            case NPCStateType.Idle:
                Idle();
                break;
            case NPCStateType.Walk:
                Walk();
                break;
            case NPCStateType.Talk:
                Talk();
                break;
            case NPCStateType.Happy:
                Happy();
                break;
            case NPCStateType.Dance:
                Dance();
                break;
        }
    }

    protected virtual void Idle()
    {
        anim.Play("Idle");
        anim.SetFloat("Speed", 0f);
    }

    protected virtual void Walk()
    {
        Wander();
    }

    protected virtual void Talk()
    {
        LookAtPlayer();

        if (dialogState.currentCoroutine != null && uiManager.dialogPanel.activeSelf)
        {
            anim.SetBool("Talking", true);
        }
        else if (dialogState.currentCoroutine == null && uiManager.dialogPanel.activeSelf)
        {
            anim.SetBool("Talking", false);
        }
    }

    protected abstract Vector3 RandomWaypoint();

    public virtual void Wander()
    {
        if (currentTarget == Vector3.zero || Vector3.Distance(transform.position, currentTarget) < 0.5f)
        {
            if (Vector3.Distance(transform.position, currentTarget) < 6f)
            {
                currentTarget = RandomWaypoint();
            }
        }

        Vector3 direction = (currentTarget - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }

        transform.position = Vector3.MoveTowards(transform.position, currentTarget, moveSpeed * Time.deltaTime);

        float currentSpeed;
        if (Vector3.Distance(transform.position, currentTarget) > 0.1f)
        {
            currentSpeed = 0.3f;
        }
        else
        {
            currentSpeed = 0.05f;
        }

        anim.SetFloat("Speed", currentSpeed);
    }

    public void LookAtPlayer()
    {
        anim.SetFloat("Speed", 0f);
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0f;


        if (direction != player.position)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }

        //if (Quaternion.Angle(transform.rotation, player.rotation) < 1f)
        //{
        //    SetCurrentState(NPCStateType.Talk);
        //}
    }

    protected virtual void Happy()
    {
        anim.SetTrigger("Happy");

    }

    protected virtual void Dance()
    {
        anim.SetTrigger("Dance");

    }

    public void SetCurrentState(NPCStateType newState)
    {
        npcState = newState;
        print($"상태변경 {newState}");
    }


}

