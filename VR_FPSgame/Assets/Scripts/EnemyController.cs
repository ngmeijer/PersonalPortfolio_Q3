using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum E_State
{
    Idle,
    Patrol,
    Chase,
    Attack,
    Death,
}

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent enemyAgent
    {
        get => agent;
        private set => agent = value;
    }

    [SerializeField] private NavMeshAgent agent;

    public Animator enemyAnimator
    {
        get => anim;
        private set => anim = value;
    }

    [SerializeField] private Animator anim;

    public AudioSource enemyAudioSource
    {
        get => audioSource;
        private set => audioSource = value;
    }

    [SerializeField] private AudioSource audioSource;

    public GameObject player { get; private set; }

    private E_State e_currentState;
    private EnemyState currentState;
    private EnemyIdleState idleState;
    private EnemyPatrolState patrolState;
    private EnemyChaseState chaseState;
    private EnemyDeathState deathState;
    private EnemyAttackState attackState;

    [SerializeField] [Range(25, 500)] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    private bool agentIsAlert;
    public bool HasDied;
    public bool TestMode;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        currentHealth = maxHealth;
        GameStats.onPlayerDeath.AddListener(listenToPlayerDeathEvent);

        idleState = GetComponent<EnemyIdleState>();
        patrolState = GetComponent<EnemyPatrolState>();
        chaseState = GetComponent<EnemyChaseState>();
        deathState = GetComponent<EnemyDeathState>();
        attackState = GetComponent<EnemyAttackState>();

        if (TestMode) agent.enabled = false;
    }

    private void Start()
    {
        ChangeState(E_State.Patrol);
    }

    private void listenToPlayerDeathEvent() => ChangeState(E_State.Idle);

    public void ChangeState(E_State pNewState)
    {
        if (pNewState == e_currentState) return;
        if (HasDied) return;
        if (currentState != null) currentState.ExitState();
        checkLastState(e_currentState);

        switch (pNewState)
        {
            case E_State.Idle:
                e_currentState = E_State.Idle;
                currentState = idleState;
                break;
            case E_State.Patrol:
                e_currentState = E_State.Patrol;
                currentState = patrolState;
                break;
            case E_State.Chase:
                agentIsAlert = true;
                e_currentState = E_State.Chase;
                currentState = chaseState;
                break;
            case E_State.Attack:
                agentIsAlert = true;
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

    private void checkLastState(E_State pLastState)
    {
        switch (pLastState)
        {
            case E_State.Idle:
                break;
            case E_State.Patrol:
                agentIsAlert = true;
                break;
            case E_State.Chase:
                break;
            case E_State.Attack:
                break;
            case E_State.Death:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(pLastState), pLastState, null);
        }
    }

    private void Update()
    {
        //if (!agentIsAlert) changeState(E_State.Patrol);

        if (!TestMode)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer > agent.stoppingDistance && agentIsAlert) ChangeState(E_State.Chase);
            if (distanceToPlayer <= agent.stoppingDistance && agentIsAlert) ChangeState(E_State.Attack);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.collider.tag)
        {
            case "RifleBullet":
                takeDamage(PlayerStats.RifleDamage);
                break;
            case "PistolBullet":
                takeDamage(PlayerStats.PistolDamage);
                break;
            case "ShotgunShell":
                takeDamage(PlayerStats.ShotgunDamage);
                break;
            case "Grenade":
                break;
        }
    }

    private void takeDamage(int pDamage)
    {
        currentHealth -= pDamage;

        if (currentHealth <= 0) ChangeState(E_State.Death);
    }
}