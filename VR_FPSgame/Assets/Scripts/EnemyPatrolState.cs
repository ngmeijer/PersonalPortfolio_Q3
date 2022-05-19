using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    private Waypoint currentWaypoint;
    private int lastIndex = -1;

    public override void EnterState()
    {
        stateIsActive = true;
        agent.speed = maxSpeed;
    }

    private void Update()
    {
        if (!stateIsActive) return;

        if (!agent.hasPath)
        {
            setNewDestination();
        }

        float velocity = agent.velocity.magnitude / agent.speed;

        anim.SetFloat("Speed", velocity, 0.05f, Time.deltaTime);
    }

    private void setNewDestination()
    {
        if(currentWaypoint != null) currentWaypoint.HasEnemy = false;
        currentWaypoint = GetNewPatrolpoint();
        currentWaypoint.HasEnemy = true;
        agent.SetDestination(currentWaypoint.position);
    }

    private Waypoint GetNewPatrolpoint()
    {
        Waypoint possibleWaypoint;
        int randomIndex = Random.Range(0, GameManager.Instance.patrolPoints.Count);
        if (randomIndex != lastIndex) possibleWaypoint = GameManager.Instance.patrolPoints[randomIndex];
        else possibleWaypoint = GetNewPatrolpoint();

        if (possibleWaypoint.HasEnemy) possibleWaypoint = GetNewPatrolpoint();

        lastIndex = randomIndex;
        return possibleWaypoint;
    }

    public override void ExitState()
    {
        stateIsActive = false;
    }
}