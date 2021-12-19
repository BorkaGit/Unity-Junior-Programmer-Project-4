using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    private float spawnRange = 9.0f;
    public int enemyCount;
    public int waveNumber = 1;
    public int waveNumberS;
    public GameObject powerupPrefab;
    public GameObject shotgunPrefab;

    void Start()
    {

        SpawnEnemyWave(waveNumber);
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        Instantiate(shotgunPrefab, GenerateSpawnPosition(), shotgunPrefab.transform.rotation);
        
    }

    
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            waveNumberS = waveNumber;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);;
            Instantiate(shotgunPrefab, GenerateSpawnPosition(), shotgunPrefab.transform.rotation);

        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int enemyPrefabIndex=Random.Range(0,enemyPrefab.Length);
            Instantiate(enemyPrefab[enemyPrefabIndex], GenerateSpawnPosition(), enemyPrefab[enemyPrefabIndex].transform.rotation);
        }
    }

    
}
