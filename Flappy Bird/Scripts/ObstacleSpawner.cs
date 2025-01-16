using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab; // Préfabriqué de l'obstacle
    public float spawnRateMin = 1f; // Temps minimum entre chaque spawn
    public float spawnRateMax = 3f; // Temps maximum entre chaque spawn
    public float heightOffsetMin = -2f; // Hauteur minimale de spawn
    public float heightOffsetMax = 2f; // Hauteur maximale de spawn

    private void Start()
    {
        // Appel initial du spawn avec un délai aléatoire
        float randomSpawnRate = Random.Range(spawnRateMin, spawnRateMax);
        InvokeRepeating("SpawnObstacle", 1f, randomSpawnRate);
    }

    private void SpawnObstacle()
    {
        // Hauteur de spawn aléatoire
        float randomHeight = Random.Range(heightOffsetMin, heightOffsetMax);
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + randomHeight, 0);
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);

        // Réajuster le temps entre chaque spawn avec un délai aléatoire
        float randomSpawnRate = Random.Range(spawnRateMin, spawnRateMax);
        CancelInvoke("SpawnObstacle"); // Annuler l'appel précédent
        InvokeRepeating("SpawnObstacle", randomSpawnRate, randomSpawnRate); // Redémarrer avec le nouveau spawn rate
    }
}
