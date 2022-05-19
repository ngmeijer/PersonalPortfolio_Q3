using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAttackState : EnemyState
{
    [SerializeField] [Range(1.5f, 4)] private float minAttackSpeed;
    [SerializeField] [Range(4, 10)] private float maxAttackSpeed;
    private float randomAttackSpeed;
    
    private void Start()
    {
        randomAttackSpeed = Random.Range(minAttackSpeed, maxAttackSpeed);
        agent.speed = defaultSpeed;
    }

    public override void EnterState()
    {
        stateIsActive = true;

        StartCoroutine(attack());
    }

    private IEnumerator attack()
    {
        if (!stateIsActive)
        {
            ExitState();
        }
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(randomAttackSpeed);
        StartCoroutine(attack());
    }

    public override void ExitState()
    {
        StopAllCoroutines();
        anim.ResetTrigger("Attack");
        stateIsActive = false;
    }
}