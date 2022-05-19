using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyState
{
    [SerializeField] [Range(0.1f, 3f)] private float redirectionInterval = 1f;
    private float timer;

    public override void EnterState()
    {
        stateIsActive = true;
        agent.speed = maxSpeed;
        agent.SetDestination(player.transform.position);
    }

    protected override void Update()
    {
        if (!stateIsActive) return;

        timer += Time.deltaTime;

        if (timer > redirectionInterval)
        {
            agent.SetDestination(player.transform.position);
            timer = 0;
        }
        
        float velocity = agent.velocity.magnitude / agent.speed;
        
        anim.SetFloat("Speed", velocity, 0.05f, Time.deltaTime);
    }

    public override void ExitState()
    {
        anim.SetFloat("Speed", 0, 0, Time.deltaTime);
        stateIsActive = false;
    }
}