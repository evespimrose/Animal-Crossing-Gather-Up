using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NPCState : MonoBehaviour
{

    public List<Vector3> waypoints = new List<Vector3>();
    private int currentWaypointIndex = 0;
    Animator anim;

    public Transform player;
    private float rotateSpeed = 5f; //고개 돌리기

    enum State
    {
        Idle,
        Walk,
        Talk,
        Happy,
        Dance,
    }

    State npcState;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        npcState = State.Idle;
    }

    private void Start()
    {
        npcState = State.Walk;
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
        Wander();
        anim.SetFloat("Speed", 0.5f);
    }

    public void LookAtPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    private void Wander()
    {
        if (waypoints.Count == 0 || Vector3.Distance(transform.position, waypoints[currentWaypointIndex]) < 0.5f)
        {
            Vector3 newWaypoint = RandomWaypoint();
            waypoints.Add(newWaypoint);
        }

        if (waypoints.Count == 0) return;

        Vector3 targetPosition = waypoints[currentWaypointIndex];
        float moveSpeed = 0.5f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed);

        if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
        }

        anim.SetFloat("Speed", 0.5f);
    }

    private Vector3 RandomWaypoint() //리즈 기준
    {
        float x = Random.Range(-23f, -26f);
        float y = 1.5f;
        float z = Random.Range(22f, 28f);

        return new Vector3(x, y, z);
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

}

