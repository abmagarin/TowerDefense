using UnityEngine;
using System.Collections;


public class spawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform waypointsLeft;
    public Transform waypointsRight;
    bool randomSpawnCounter = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemiesWithDelay(10));
    }
    IEnumerator SpawnEnemiesWithDelay(int totalEnemies)
    {
        for (int i = 0; i < totalEnemies; i++)
        {
            spawnEnemy();
            yield return new WaitForSeconds(1f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void spawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        EnemyWalk movement = newEnemy.GetComponent<EnemyWalk>();

        if (randomSpawnCounter == true)
        {
            movement.SetWaypoints(waypointsLeft);
        }
        else
        {
            movement.SetWaypoints(waypointsRight);
        }

        randomSpawnCounter = !randomSpawnCounter;


    }
}
