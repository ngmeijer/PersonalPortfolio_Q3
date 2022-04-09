using UnityEngine;

public class EnemyDeathState : EnemyState
{
    public EnemyController master;
    
    private void Start()
    {
        anim = master.anim;
        player = master.player;
    }

    public override void EnterState()
    {
        stateIsActive = true;
    }

    public override void ExitState()
    {
        stateIsActive = false;
    }

    private void Update()
    {
        if (!stateIsActive) return;
    }
}