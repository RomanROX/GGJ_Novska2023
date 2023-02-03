using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Wave
{
    public string waveName;
    public int numOfEnemies;
    public GameObject[] typeOfEnemies;
    public float spawnInterval;
}

public class Wave_Manager : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;

    private Wave currentWave;
    private int currentWaveNumber; 
    private float nextSpawnTime;
    public float timeBetweenWaves;
    public bool timeBtwWaves = true;

    private bool canSpawn = true;

    public Text waveNumber;

    private void Update()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (totalEnemies.Length == 0 && canSpawn == false && currentWaveNumber + 1 != waves.Length && timeBtwWaves == true)
        {
            Invoke("SpawnNextWave", timeBetweenWaves);
            timeBtwWaves = false;
        }
    }
    void SpawnNextWave()
    {
        currentWaveNumber++;
        waveNumber.text = currentWave.waveName.ToString();
        canSpawn = true; 
        timeBtwWaves = true; 
    }
    public void SpawnWave()
    {
        if(canSpawn && nextSpawnTime < Time.time) 
        {
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)]; 
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            currentWave.numOfEnemies--;
            nextSpawnTime = Time.time + currentWave.spawnInterval; 
            if(currentWave.numOfEnemies==0)
            {
                canSpawn = false; 
            }
        }
    }
}
