using UnityEngine;

public class EnemyDeathState : EnemyState
{
    public override void EnterState()
    {
        stateIsActive = true;

        master.HasDied = true;
        anim.SetTrigger("HasDied");
        agent.isStopped = true;
        if (agent.velocity.magnitude > 0)
            agent.velocity = Vector3.zero;
    }

    public override void ExitState()
    {
        stateIsActive = false;
    }
}