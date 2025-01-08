using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject fencePrefab;
    [SerializeField] GameObject applePrefab;
    [SerializeField] GameObject coinPrefab;

    [SerializeField] float appleSpawnChance = 0.3f;
    [SerializeField] float coinSpawnChance = 0.5f;


    [SerializeField] float[] lanes = {-2.5f, 0f, 2.5f};
    List<int> availableLanes = new List<int>{0, 1, 2};
    
    void Start()
    {
        SpawnFences();
        SpawnApple();
        SpawnCoins();
    }

    void SpawnFences()
    {
        int fencesToSpawn = Random.Range(0, lanes.Length);

        for (int i = 0; i < fencesToSpawn; i++)
        {
            if (availableLanes.Count <= 0) break; // failsafe

            int selectedLane = SelectLane();

            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
            Instantiate(fencePrefab, spawnPosition, Quaternion.identity, this.transform);
        }
    }

    void SpawnApple()
    {
        if(Random.value > appleSpawnChance || availableLanes.Count <= 0) return; // failsafe

        int selectedLane = SelectLane();

        Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
        Instantiate(applePrefab, spawnPosition, Quaternion.identity, this.transform);
    }

    void SpawnCoins()
    {
        if(Random.value > coinSpawnChance || availableLanes.Count <= 0) return; // failsafe

        int selectedLane = SelectLane();

        Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
        Instantiate(coinPrefab, spawnPosition, Quaternion.identity, this.transform);
    }

    

    int SelectLane()
    {
        int randomLaneIndex = Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randomLaneIndex];
        availableLanes.RemoveAt(randomLaneIndex); // tam anlamadım 
        return selectedLane;
    }
}
