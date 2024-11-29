using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCState : MonoBehaviour
{

    public List<Vector3> waypoints = new List<Vector3>();
    Animator animator;
    NavMeshAgent agent;

    public Transform player;
    private float rotateSpeed = 5f;

    enum State
    {
        Idle,
        Walk,
        LookAtPlayer,
        Talk,
        Happy,
        Dance,
    }

    State npcState;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        switch (npcState)
        {
            case State.Idle:
                Idle();
                break;
            case State.Walk:
                Walk();
                break;
            case State.LookAtPlayer:
                LookAtPlayer();
                break;
            case State.Talk:
                Talk();
                break;
            case State.Happy:
                Happy();
                break;
            case State.Dance:
                Dance();
                break;
        }
    }

    private void Idle()
    {

    }

    private void Walk()
    {
        animator.SetFloat("Walk", 0.1f);
    }

    private void LookAtPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    private void Talk()
    {
        animator.SetBool("Talk", true); // -> Bool 설정
    }

    private void Happy()
    {
        animator.SetTrigger("Happy"); // 한번으로 끝나서 Trigger로 설정

    }

    private void Dance()
    {
        animator.SetTrigger("Dance");

    }

    private bool IsPlayerNearby() //플레이어가 반경 안에 있으면 돌아보기 (너구리 or 몇몇 주민)
    {
        float distance = Vector3.Distance(transform.position, player.position);
        return distance < 4f;
    }
}

