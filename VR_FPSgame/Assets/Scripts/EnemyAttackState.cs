using System;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public override void EnterState()
    {
        stateIsActive = true;
        
        anim.SetBool("IsAttacking", true);
    }

    public override void ExitState()
    {
        stateIsActive = false;
        
        anim.SetBool("IsAttacking", false);
    }

    protected override void Update()
    {
        if (!stateIsActive) return;
    }
}