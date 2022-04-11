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
    public NavMeshAgent enemyAgent
    {
        get { return agent; }
        private set { agent = value; }
    }
    [SerializeField] private NavMeshAgent agent;

    public Animator enemyAnimator
    {
        get { return anim; }
        private set { anim = value; }
    }
    [SerializeField]private Animator anim;
    
    public GameObject player { get; private set; }

    private float timer;

    private float speed;

    private E_State e_currentState;
    private EnemyState currentState;
    private EnemyIdleState idleState;
    private EnemyChaseState chaseState;
    private EnemyDeathState deathState;
    private EnemyAttackState attackState;

    [SerializeField] [Range(25, 500)] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;
    public bool HasDied;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        currentHealth = maxHealth;
        GameStats.onPlayerDeath.AddListener(listenToPlayerDeathEvent);

        idleState = GetComponent<EnemyIdleState>();
        chaseState = GetComponent<EnemyChaseState>();
        deathState = GetComponent<EnemyDeathState>();
        attackState = GetComponent<EnemyAttackState>();
    }

    private void Start()
    {
        changeState(E_State.Chase);
    }

    private void listenToPlayerDeathEvent() => changeState(E_State.Idle);

    private void changeState(E_State pNewState)
    {
        if (pNewState == e_currentState) return;
        if (HasDied) return;
        if (currentState != null) currentState.ExitState();

        switch (pNewState)
        {
            case E_State.Idle:
                e_currentState = E_State.Idle;
                currentState = idleState;
                break;
            case E_State.Chase:
                e_currentState = E_State.Chase;
                currentState = chaseState;
                break;
            case E_State.Attack:
                e_currentState = E_State.Attack;
                currentState = attackState;
                break;
            case E_State.Death:
                e_currentState = E_State.Death;
                currentState = deathState;
                break;
        }

        if (currentState != null) currentState.EnterState();
        else
            Debug.LogError($"State {currentState} is not attached to the Enemy prefab, " +
                           $"or added in the EnemyController script!");
    }

    private void Update()
    {
        if (agent.remainingDistance > agent.stoppingDistance) changeState(E_State.Chase);
        if (agent.remainingDistance <= agent.stoppingDistance) changeState(E_State.Attack);
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

    private void decreaseSpeed()
    {
        
    }

    private void takeDamage(int pDamage)
    {
        currentHealth -= pDamage;

        if (currentHealth <= 0) changeState(E_State.Death);
    }
}