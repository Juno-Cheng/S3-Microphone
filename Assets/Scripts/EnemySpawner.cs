using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // enemy prefabs
    public float spawnInterval = 5f; // spawn time
    public Transform[] spawnPoints; // spawn locations, empty objects

    void Start()
    {
        // Spawn enemies
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // random enemy from array "enemyPrefabs"
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // random spawn point from "spawnPoints"
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
