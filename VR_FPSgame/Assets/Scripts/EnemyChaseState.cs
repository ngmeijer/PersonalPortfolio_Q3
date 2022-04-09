using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyState
{
    public EnemyController master;
    public NavMeshAgent agent;
    [SerializeField] [Range(0.1f, 3f)] private float redirectionInterval = 1f;
    private float timer;
    
    private void Start()
    {
        anim = master.anim;
        player = master.player;
        agent = master.agent;
    }

    public override void EnterState()
    {
        stateIsActive = true;
        agent.SetDestination(player.transform.position);
    }

    public override void ExitState()
    {
        stateIsActive = false;
    }

    private void Update()
    {
        if (!stateIsActive) return;

        timer += Time.deltaTime;

        if (timer > redirectionInterval)
        {
            agent.SetDestination(player.transform.position);
            timer = 0;
        }
    }
}