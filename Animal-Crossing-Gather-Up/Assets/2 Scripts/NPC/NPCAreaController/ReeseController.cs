using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReeseController : NPCState
{

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        SetState(NPCStateType.Walk);
    }
    protected override void Update()
    {
        base.Update();
        if (waypoints.Count > 0)
        {
            Debug.Log($"Current waypoint: {waypoints[currentWaypointIndex]}");
        }
    }

    //protected override void Wander()
    //{
    //    if (waypoints.Count == 0 || Vector3.Distance(transform.position, waypoints[currentWaypointIndex]) < 0.5f)
    //    {
    //        Vector3 newWaypoint = RandomWaypoint();
    //        waypoints.Add(newWaypoint);
    //    }

    //    if (waypoints.Count == 0) return;

    //    Vector3 targetPosition = waypoints[currentWaypointIndex];
    //    float moveSpeed = 0.5f * Time.deltaTime;
    //    transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed);

    //    if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
    //    {
    //        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
    //    }

    //    anim.SetFloat("Speed", 0.5f);
    //}

    protected override void StartNPCDialog()
    {

    }

    protected override Vector3 RandomWaypoint()
    {
        float x = Random.Range(-23f, -26f);
        float y = 1.5f;
        float z = Random.Range(22f, 28f);

        return new Vector3(x, y, z);
    }
}
