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
public abstract class NPCState : MonoBehaviour
{

    protected List<Vector3> waypoints = new List<Vector3>();
    protected int currentWaypointIndex = 0;
    protected Animator anim;
    protected float rotateSpeed = 5f;
    public Transform player;

    private NPCStateType npcState;
    protected Vector3 currentTarget;
    protected float moveSpeed;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
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
    { }

    protected virtual void Walk()
    {
        Wander();
        anim.SetFloat("Speed", 1f);
    }

    protected virtual void LookAtPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

    }

    //protected abstract void Wander();

    protected abstract Vector3 RandomWaypoint();

    protected abstract void StartNPCDialog();

    protected virtual void Wander()
    {
        if (Vector3.Distance(transform.position, currentTarget) < 0.5f)
        {
            currentTarget = RandomWaypoint();
        }

        Vector3 direction = (currentTarget - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }

        transform.position = Vector3.MoveTowards(transform.position, currentTarget, moveSpeed * Time.deltaTime);

        float currentSpeed;
        if (Vector3.Distance(transform.position, currentTarget) > 0.1f) //목표 지점까지의 거리가 0.1 보다 크면 걷기
        {
            currentSpeed = 1f;
        }
        else //목표 지점까지 거의 도착했으면 멈추기
        {
            currentSpeed = 0f;
        }

        anim.SetFloat("Speed", currentSpeed);
    }

    protected virtual IEnumerator InteractionSequence()
    {
        SetState(NPCStateType.Idle);

        // 플레이어를 향해 회전
        while (true)
        {
            Vector3 direction = player.position - transform.position;
            direction.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 5f)
            {
                SetState(NPCStateType.Talk);
                break;
            }
            yield return null;
        }

    }

    protected virtual void Talk()
    {
        anim.SetBool("Talk", true); // -> Bool ����
    }

    protected virtual void Happy()
    {
        anim.SetTrigger("Happy"); // �ѹ����� ������ Trigger�� ����

    }

    protected virtual void Dance()
    {
        anim.SetTrigger("Dance");

    }

    protected virtual void SetState(NPCStateType newState)
    {
        npcState = newState;
    }


}

