using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float redirectionInterval = 1f;
    private float timer;

    private GameObject player;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent.SetDestination(player.transform.position);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > redirectionInterval)
        {
            agent.SetDestination(player.transform.position);
            timer = 0;
        }
    }
}
