using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCState : MonoBehaviour
{

    public List<Vector3> waypoints = new List<Vector3>();
    Animator anim;
    NavMeshAgent agent;

    public Transform player;
    private float rotateSpeed = 5f;

    enum State
    {
        Idle,
        Walk,
        NearByPlayer,
        Talk,
        Happy,
        Dance,
    }

    State npcState;

    private void Start()
    {
        npcState = State.Idle;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (IsPlayerNearby())
        {
            npcState = State.NearByPlayer;
        }

        switch (npcState)
        {
            case State.Idle:
                Idle();
                break;
            case State.Walk:
                Walk();
                break;
            case State.NearByPlayer:
                NearByPlayer();
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
        anim.SetFloat("Walk", 0.1f);
    }

    private void NearByPlayer()
    {
        //�÷��̾ ���� ���ƺ��� -> �� ���� NPC
        //Vector3 direction = player.position - transform.position;
        //direction.y = 0;

        //Quaternion targetRotation = Quaternion.LookRotation(direction);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        //��� ���󰡱�

    }

    private void Talk()
    {
        anim.SetBool("Talk", true); // -> Bool ����
    }

    private void Happy()
    {
        anim.SetTrigger("Happy"); // �ѹ����� ������ Trigger�� ����

    }

    private void Dance()
    {
        anim.SetTrigger("Dance");

    }

    private bool IsPlayerNearby() //�÷��̾ �ݰ� �ȿ� ������ ���ƺ��� (�ʱ��� or ��� �ֹ�)
    {
        float distance = Vector3.Distance(transform.position, player.position);
        return distance < 4f;
    }
}

