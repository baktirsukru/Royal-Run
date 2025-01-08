using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] float obstacleSpawnTime = 5f;
    [SerializeField] Transform obstacleParent;
    [SerializeField] float spawnWidth = 4f;
    
    
    void Start()
    {
        StartCoroutine(ObstacleSpawnRoutine());
    }

    IEnumerator ObstacleSpawnRoutine()
    {
        while(true)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnWidth,spawnWidth), transform.position.y, transform.position.z);
            GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0,obstaclePrefabs.Length)];
            yield return new WaitForSeconds(obstacleSpawnTime); 
            Instantiate(obstaclePrefab, spawnPosition, Random.rotation, obstacleParent);
        }
    }
}
