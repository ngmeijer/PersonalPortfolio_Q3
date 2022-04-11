using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyState : MonoBehaviour
{
    protected bool stateIsActive;

    protected Animator anim;
    protected GameObject player;
    protected NavMeshAgent agent;
    protected EnemyController master;

    private void Awake()
    {
        master = GetComponent<EnemyController>();
        agent = master.enemyAgent;
        anim = master.enemyAnimator;
        player = master.player;
    }

    public abstract void EnterState();
    
    protected virtual  void Update()
    {
        if (!stateIsActive) return;
    }

    public virtual void ExitState() => stateIsActive = false;
}