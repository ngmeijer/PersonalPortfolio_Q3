using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum E_State
{
    Idle,
    Chase,
    Attack,
    Death,
}

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent { get; private set; }
    public Animator anim { get; private set; }
    public GameObject player { get; private set; }
    
    private float timer;


    private float speed;

    private EnemyState currentState;
    private EnemyIdleState idleState;
    private EnemyChaseState chaseState;
    private EnemyDeathState deathState;
    private EnemyAttackState attackState;

    [SerializeField] [Range(25, 500)] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        currentHealth = maxHealth;
        GameStats.onPlayerDeath.AddListener(listenToPlayerDeathEvent);
        changeState(E_State.Chase);
    }

    private void listenToPlayerDeathEvent()
    {
        changeState(E_State.Death);
    }

    private void changeState(E_State pNewState)
    {
        if(currentState != null) currentState.ExitState();

        switch (pNewState)
        {
            case E_State.Idle:
                currentState = idleState;
                break;
            case E_State.Chase:
                currentState = chaseState;
                break;
            case E_State.Attack:
                currentState = attackState;
                break;
            case E_State.Death:
                currentState = deathState;
                break;
        }
        
        currentState.EnterState();
    }

    private void Update()
    {
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            changeState(E_State.Chase);
            timer = 0;
        }

        if (agent.remainingDistance <= agent.stoppingDistance && currentState != attackState)
        {
            changeState(E_State.Attack);
            anim.SetBool("IsAttacking", true);
        }

        if (currentHealth <= 0 && currentState != deathState)
        {
            changeState(E_State.Death);
            anim.SetTrigger("HasDied");
            agent.isStopped = true;
            if (agent.velocity.magnitude > 0)
                agent.velocity = Vector3.zero;
        }

        HandleAnimations();
    }

    private void HandleAnimations()
    {
        float velocity = agent.velocity.magnitude / agent.speed;

        if (velocity >= 1) return;

        anim.SetFloat("Speed", velocity, 0.05f, Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.collider.tag)
        {
            case "RifleBullet":
                takeDamage(PlayerStats.RifleDamage);
                break;
            case "PistolBullet":
                break;
            case "ShotgunShell":
                break;
            case "Grenade":
                break;
            default:
                Debug.Log(other.collider.tag + " (tag) does not have any consequences assigned.");
                break;
        }
    }

    private void takeDamage(int pDamage)
    {
        currentHealth -= pDamage;
    }
}