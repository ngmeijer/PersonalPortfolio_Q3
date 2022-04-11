using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    [SerializeField] private List<AudioClip> attackNoises;
    
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