using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] Transform obstacleParent;

    [Header("Settings")]
    [SerializeField] float obstacleSpawnTime = 5f;
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
            yield return new WaitForSeconds(obstacleSpawnTime); // tam anlamadım
            Instantiate(obstaclePrefab, spawnPosition, Random.rotation, obstacleParent);
        }
    }
}