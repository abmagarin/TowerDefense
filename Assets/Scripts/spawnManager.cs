using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class EnemyWave
{
    public GameObject[] enemyTypes;  // diferentes prefabs de enemigos
    public int totalEnemies = 10;    // enemigos a spawnear en esta horda
    public float spawnDelay = 1f;    // tiempo entre enemigos
    public float startDelay = 0f;    // tiempo antes de que empiece la horda

    public Transform spawnPoint;     // punto desde el que se instancian
    public Transform[] waypoints;    // 0 = izquierda, 1 = derecha
}

public class spawnManager : MonoBehaviour
{
    public List<EnemyWave> waves; // lista de hordas
    public Transform waypointsLeft;
    public Transform waypointsRight;
    public Transform waypointsCentre;
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

            Transform spawnPos = wave.spawnPoint != null ? wave.spawnPoint : transform;

            GameObject newEnemy = Instantiate(enemyToSpawn, spawnPos.position, Quaternion.identity);
            EnemyWalk movement = newEnemy.GetComponent<EnemyWalk>();

            if (wave.spawnPoint != null && wave.spawnPoint.CompareTag("FirstSpawn"))
            {
                if (randomSpawnCounter)
                {
                    if (waypointsLeft != null)
                        movement.SetWaypoints(waypointsLeft);
                    else
                        Debug.LogWarning("waypointsLeft no está asignado en el inspector.");
                }
                else
                {
                    if (waypointsRight != null)
                        movement.SetWaypoints(waypointsRight);
                    else
                        Debug.LogWarning("waypointsRight no está asignado en el inspector.");
                }

                randomSpawnCounter = !randomSpawnCounter;
            }
            else if (wave.spawnPoint != null && wave.spawnPoint.CompareTag("SecondSpawn"))
            {
                if (waypointsCentre != null)
                {
                    movement.SetWaypoints(waypointsCentre);
                }
                else
                {
                    Debug.LogWarning("waypointsCentre no está asignado en el inspector.");
                }
            }
            else
            {
                Debug.LogWarning("spawnPoint sin etiqueta válida (debe ser 'FirstSpawn' o 'SecondSpawn').");
            }

            yield return new WaitForSeconds(wave.spawnDelay);
        }
    }


}
