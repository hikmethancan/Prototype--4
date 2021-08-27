using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject enemysPrefabs;
    [SerializeField] GameObject powerUpPrefab;
    float spawnRange = 9f;
    [SerializeField] int enemyCount;
    [SerializeField] int enemyWave;
    private void Start()
    {
        SpawnWave(enemyWave);
        //Instantiate(powerUpPrefab, SpawnEnemiesPosition(), powerUpPrefab.transform.rotation);
    }

    private void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            enemyWave++;
            SpawnWave(enemyWave);
            Instantiate(powerUpPrefab, SpawnEnemiesPosition(), powerUpPrefab.transform.rotation);
        }
    }

    Vector3 SpawnEnemiesPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 spawnPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return spawnPos;
    }

    void SpawnWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemysPrefabs, SpawnEnemiesPosition(), enemysPrefabs.transform.rotation);
        }
    }

}
