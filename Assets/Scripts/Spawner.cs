using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
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

        GameObject obstacle = Instantiate(obstaclePrefab, new Vector3(0, spawnY, 0), Quaternion.identity);
        Instantiate(coinPrefab, obstacle.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        Instantiate(colorChangerPrefab, obstacle.transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);

    }
}
