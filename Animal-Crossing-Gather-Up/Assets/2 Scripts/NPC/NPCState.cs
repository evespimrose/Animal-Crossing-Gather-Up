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
public abstract class NPCState : MonoBehaviour//, INPCState
{

    protected List<Vector3> waypoints = new List<Vector3>();
    protected int currentWaypointIndex = 0;
    protected Animator anim;
    protected float rotateSpeed = 5f;
    public Transform player;

    private IDialogState dialogState;
    protected NPCStateType npcState;
    protected Vector3 currentTarget;
    protected float moveSpeed;

    protected virtual void Awake()
    {
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
    }

    protected virtual void Walk()
    {
        Wander();
        anim.SetFloat("Speed", 1f);
    }

    protected virtual void Talk()
    {
        if (dialogState.currentCoroutine != null)
        {
            anim.SetBool("Talk", true);

        }
        else
        {
            anim.SetBool("Talk", false);
        }
    }

    //protected virtual void LookAtPlayer()
    //{
    //    Vector3 direction = player.position - transform.position;
    //    direction.y = 0.68f;

    //    Quaternion targetRotation = Quaternion.LookRotation(direction);
    //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

    //}

    protected abstract Vector3 RandomWaypoint();

    public virtual void Wander()
    {
        if (currentTarget == Vector3.zero || Vector3.Distance(transform.position, currentTarget) < 0.5f)
        {
            currentTarget = RandomWaypoint();
        }

        print($"wayPoint: {currentTarget}");
        print($"현재 위치: {transform.position}");

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
            currentSpeed = 0.5f;
        }
        else
        {
            currentSpeed = 0.1f;
        }

        anim.SetFloat("Speed", currentSpeed);
    }

    //protected virtual IEnumerator InteractionSequence()
    //{
    //    SetState(NPCStateType.Idle);

    //    // 플레이어를 향해 회전
    //    while (true)
    //    {
    //        Vector3 direction = player.position - transform.position;
    //        direction.y = 0.68f;

    //        Quaternion targetRotation = Quaternion.LookRotation(direction);
    //        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

    //        if (Quaternion.Angle(transform.rotation, targetRotation) < 2f)
    //        {
    //            SetState(NPCStateType.Talk);
    //            break;
    //        }

    //        yield return null;
    //    }

    //}

    protected virtual void Happy()
    {
        anim.SetTrigger("Happy");

    }

    protected virtual void Dance()
    {
        anim.SetTrigger("Dance");

    }

    public void SetState(NPCStateType newState)
    {
        npcState = newState;
    }


}

