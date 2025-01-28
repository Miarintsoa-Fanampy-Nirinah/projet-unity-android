using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnRateMin = 1f;
    public float spawnRateMax = 3f;
    public float heightOffsetMin = -2f;
    public float heightOffsetMax = 2f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), 1f, Random.Range(spawnRateMin, spawnRateMax));
    }

    private void SpawnObstacle()
    {
        float randomHeight = Random.Range(heightOffsetMin, heightOffsetMax);
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + randomHeight, 0);
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);

        CancelInvoke(nameof(SpawnObstacle));
        InvokeRepeating(nameof(SpawnObstacle), Random.Range(spawnRateMin, spawnRateMax), Random.Range(spawnRateMin, spawnRateMax));
    }
}
