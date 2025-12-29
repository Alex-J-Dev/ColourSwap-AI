using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; // array of obstacle prefabs
    public GameObject coinPrefab;
    public GameObject colorChangerPrefab;

    private float spawnY = 0f;
    private float distance = 5f;

    void Start()
    {
        SpawnObstacle();
    }

    void Update()
    {
        if (Camera.main.transform.position.y > spawnY - 10)
        {
            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        spawnY += distance;

        // Randomly select obstacle
        int rand = Random.Range(0, obstaclePrefabs.Length);
        GameObject obstacle = Instantiate(obstaclePrefabs[rand], new Vector3(0, spawnY, 0), Quaternion.identity);

        // Spawn coin and color changer relative to obstacle
        Instantiate(coinPrefab, obstacle.transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        Instantiate(colorChangerPrefab, obstacle.transform.position + new Vector3(0, 2f, 0), Quaternion.identity);
    }
}
