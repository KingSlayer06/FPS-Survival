using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager enemyManager;

    [SerializeField] private GameObject cannibalPrefab, boarPrefab;
    [SerializeField] private Transform cannibslSpawn, boarSpawn;
    [SerializeField] private float waitBeforeNextSpawn = 10f;

    [SerializeField] private Transform[] cannibslSpawnPoints, boarSpawnPoints;
    private int cannibalCount, boarCount;
    void Awake()
    {
        if(enemyManager == null)
        {
            enemyManager = this;
        }
    }

    private void Start()
    {
        cannibslSpawnPoints = cannibslSpawn.GetComponentsInChildren<Transform>();
        boarSpawnPoints = boarSpawn.GetComponentsInChildren<Transform>();

        cannibalCount = cannibslSpawnPoints.Length;
        boarCount = boarSpawnPoints.Length;

        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < cannibalCount; i++)
        {
            Instantiate(cannibalPrefab, cannibslSpawnPoints[i].position, Quaternion.identity);
        }

        for (int i = 0; i < boarCount; i++)
        {
            Instantiate(boarPrefab, boarSpawnPoints[i].position, Quaternion.identity);
        }
    }
}
