using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class EnemyWave
{
    public GameObject[] enemyTypes; // diferentes prefabs de enemigos
    public int totalEnemies = 10;   // enemigos a spawnear en esta horda
    public float spawnDelay = 1f;   // tiempo entre enemigos
    public float startDelay = 0f;   // tiempo antes de que empiece la horda
}

public class spawnManager : MonoBehaviour
{
    public List<EnemyWave> waves; // lista de hordas
    public Transform waypointsLeft;
    public Transform waypointsRight;
    private bool randomSpawnCounter = true;

    void Start()
    {
        StartCoroutine(SpawnAllWaves());
    }

    IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = 0; waveIndex < waves.Count; waveIndex++)
        {
            EnemyWave currentWave = waves[waveIndex];

            // Espera antes de empezar esta horda
            if (currentWave.startDelay > 0)
            {
                yield return new WaitForSeconds(currentWave.startDelay);
            }

            yield return StartCoroutine(SpawnEnemiesInWave(currentWave));

        }
    }


    IEnumerator SpawnEnemiesInWave(EnemyWave wave)
    {
        for (int i = 0; i < wave.totalEnemies; i++)
        {
            int randIndex = Random.Range(0, wave.enemyTypes.Length);
            GameObject enemyToSpawn = wave.enemyTypes[randIndex];

            GameObject newEnemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            EnemyWalk movement = newEnemy.GetComponent<EnemyWalk>();

            if (randomSpawnCounter)
            {
                movement.SetWaypoints(waypointsLeft);
            }
            else
            {
                movement.SetWaypoints(waypointsRight);
            }

            randomSpawnCounter = !randomSpawnCounter;
            yield return new WaitForSeconds(wave.spawnDelay);
        }
    }
}
