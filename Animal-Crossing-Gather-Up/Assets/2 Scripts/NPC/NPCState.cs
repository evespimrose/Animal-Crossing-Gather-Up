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
    private float rotateSpeed = 5f;

    enum State
    {
        Idle,
        Walk,
        Talk,
        Happy,
        Dance,
    }

    State npcState;

    private void Start()
    {
        npcState = State.Walk;
    }


    private void Awake()
    {
        anim = GetComponent<Animator>();
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
                Wander();
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

        Wander();
    }

    private void Idle()
    {

    }

    private void Walk()
    {
        anim.SetFloat("WalkForward", 0.1f);
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
        Vector3 newWaypoint = RandomWaypoint();
        waypoints.Add(newWaypoint);

        if (waypoints.Count == 0) return;

        Vector3 targetPosition = waypoints[currentWaypointIndex];
        float moveSpeed = 0.5f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
        }

        anim.SetFloat("Walk", 0.5f);
    }

    private Vector3 RandomWaypoint()
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

