using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CameraController cameraController;
    [SerializeField] GameObject[] chunkPrefabs;
    [SerializeField] GameObject checkpointChunkPrefab;
    [SerializeField] Transform chunkParent;
    [SerializeField] ScoreManager scoreManager;

    [Header("Level Settings")]
    [SerializeField] int startingChunkAmount = 12;
    [SerializeField] int checkpointChunkInterval = 8;

    [Tooltip("Do not change chunk length value")]
    [SerializeField] float chunkLenght = 10f;
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float minMoveSpeed = 2f;
    [SerializeField] float maxMoveSpeed = 20f;
    [SerializeField] float minGravityZ = -22f;
    [SerializeField] float maxGravityZ = -2f;

    List<GameObject> chunks = new List<GameObject>();  //list

    int chunkSpawned = 0;

    void Start()
    {
        SpawnStartingChunks();
    }

    void Update()
    {
        MoveChunks();
    }

    public void ChangeChunkMoveSpeed(float speedAmount)
    {
        float newMoveSpeed = moveSpeed + speedAmount;
        newMoveSpeed = Mathf.Clamp(newMoveSpeed, minMoveSpeed, maxMoveSpeed);

        if (newMoveSpeed != moveSpeed)
        {
            moveSpeed = newMoveSpeed;

            float newGravityZ = Physics.gravity.z - speedAmount;

            newGravityZ = Mathf.Clamp(newGravityZ, minGravityZ, maxGravityZ);
            Physics.gravity = new Vector3 (Physics.gravity.x, Physics.gravity.y, newGravityZ);
            cameraController.ChangeCameraFOV(speedAmount);  
        }


    }

    void SpawnStartingChunks()
    {
        for (int i = 0; i < startingChunkAmount; i++)
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        float spawnPositionZ = CalculateSpawnPositionZ();
        Vector3 chunkSpawnPos = new Vector3(transform.position.x, transform.position.y, spawnPositionZ);
        GameObject chunkToSpawn = ChooseChunkToSpawn();
        GameObject newChunkGO = Instantiate(chunkToSpawn, chunkSpawnPos, Quaternion.identity, chunkParent);
        chunks.Add(newChunkGO);
        Chunk newChunk = newChunkGO.GetComponent<Chunk>();
        newChunk.Init(this, scoreManager);

        chunkSpawned++;
    }

    private GameObject ChooseChunkToSpawn()
    {
        GameObject chunkToSpawn;
        
        if (chunkSpawned % checkpointChunkInterval == 0 && chunkSpawned != 0)
        {
            chunkToSpawn = checkpointChunkPrefab;
        }
        else
        {
            chunkToSpawn = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
        }

        return chunkToSpawn;
    }

    float CalculateSpawnPositionZ()
    {
        float spawnPositionZ;

        if (chunks.Count == 0)
        {
            spawnPositionZ = transform.position.z;
        }
        else
        {
            spawnPositionZ = chunks[chunks.Count - 1].transform.position.z + chunkLenght;
        }

        return spawnPositionZ;
    }

    void MoveChunks()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunk.transform.Translate(-transform.forward * (moveSpeed * Time.deltaTime));

            if(chunk.transform.position.z <= Camera.main.transform.position.z - chunkLenght)
            {
                chunks.Remove(chunk);
                Destroy(chunk);
                SpawnChunk();
            }
        }

    }
}
