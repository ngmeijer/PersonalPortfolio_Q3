using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum CurrentState
{
    Idle,
    Move,
    Attack,
    Death,
}

public class EnemyController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float redirectionInterval = 1f;
    [SerializeField] private Animator anim;
    private float timer;

    private GameObject player;

    private float speed;
    private CurrentState state = CurrentState.Idle;

    [SerializeField] [Range(25, 500)] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        currentHealth = maxHealth;
        GameStats.onPlayerDeath.AddListener(listenToPlayerDeathEvent);
        agent.SetDestination(player.transform.position);
    }

    private void listenToPlayerDeathEvent()
    {
        Debug.Log("Player has died");
        state = CurrentState.Idle;
    }

    private void Update()
    {
        if (state == CurrentState.Death) return;

        timer += Time.deltaTime;

        if (timer > redirectionInterval && agent.remainingDistance > agent.stoppingDistance)
        {
            agent.SetDestination(player.transform.position);
            timer = 0;
            state = CurrentState.Move;
        }

        if (agent.remainingDistance <= agent.stoppingDistance && state != CurrentState.Attack)
        {
            state = CurrentState.Attack;
            anim.SetBool("IsAttacking", true);
        }

        if (currentHealth <= 0 && state != CurrentState.Death)
        {
            state = CurrentState.Death;
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
}