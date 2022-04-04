using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private Dictionary<int, GameObject> enemyPrefabs = new Dictionary<int, GameObject>();
    [SerializeField] [Range(10, 100)] private int enemySpawnRange;
    [SerializeField] [Range(0.1f, 10f)] private float enemySpawnRate;

    
    private Vector3 randomSpawnPoint;

    private void Start()
    {
        GameObject enemy = Resources.Load<GameObject>("Prefabs/Enemy");

        enemyPrefabs.Add(100, enemy);
        StartCoroutine(spawnEnemy());
    }

    private IEnumerator spawnEnemy()
    {
        //Spawn enemy
        Vector3 direction = Random.insideUnitSphere * enemySpawnRange;
        direction += player.transform.position;       

        NavMeshHit hit;

        NavMesh.SamplePosition(direction, out hit, enemySpawnRange, 1);
        Vector3 spawnPosition = hit.position;

        GameObject selectedEnemy;
        enemyPrefabs.TryGetValue(100, out selectedEnemy);
        
        Instantiate(selectedEnemy, spawnPosition, Quaternion.identity, transform);

        yield return new WaitForSeconds(enemySpawnRate);

        StartCoroutine(spawnEnemy());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(player.transform.position, enemySpawnRange);
    }
}