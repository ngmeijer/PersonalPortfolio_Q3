using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStats : MonoBehaviour
{
    public static float EnemySpawnRate = 2f;

    public static UnityEvent onPlayerDeath = new UnityEvent();

    public static int PistolDamage;
}
