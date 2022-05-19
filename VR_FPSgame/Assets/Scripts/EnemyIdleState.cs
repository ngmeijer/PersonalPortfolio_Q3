using System;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public override void EnterState()
    {
        stateIsActive = true;
        agent.speed = defaultSpeed;
    }
}