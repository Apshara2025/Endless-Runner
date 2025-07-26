
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject fencePrefab;
    [SerializeField] GameObject applePrefab;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] float appleSpawnChance = 0.3f;
    [SerializeField] float coinSpawnChance = 0.5f;
    [SerializeField] float chunkLength = 10f;
    [SerializeField] float coinInFiveChance = 0.5f;
    [SerializeField] float coinSeparationLength = 2f;
    [SerializeField] float[] lanes = { -2.5f, 0f, 2.5f };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    List<int> availableLanes = new List<int> { 0, 1, 2 };

    void Start()
    {
        SpawnFences();
        SpawnApple();
        SpawnCoin();
    }

    void SpawnFences()
    {


        int FencesToSpawn = Random.Range(0, lanes.Length);
        for (int i = 0; i < FencesToSpawn; i++)
        {
            if (availableLanes.Count <= 0) break;
            int selectedLane = SelectLane();
            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
            Instantiate(fencePrefab, spawnPosition, Quaternion.identity, this.transform);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }

    void SpawnApple()
    {   if (Random.value > appleSpawnChance || availableLanes.Count == 0) return;
       
        int selectedLane = SelectLane();
        Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, transform.position.z);
        Instantiate(applePrefab, spawnPosition, Quaternion.identity, this.transform);
    }

    void SpawnCoin()
    {
        if (Random.value > coinSpawnChance || availableLanes.Count <= 0) return;
        int selectedLane = SelectLane();

        int maxCoinsToSpawn = 6;
        int CoinsToSpawn = Random.Range(1, maxCoinsToSpawn);
        float TopofChunkZPos = transform.position.z + (coinSeparationLength * 2f);

        for (int i = 0; i < CoinsToSpawn; i++)
        {
            float spawnPositionZ = TopofChunkZPos - (i * coinSeparationLength);
            Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, spawnPositionZ);
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity, this.transform);
        }
        
    }

    private int SelectLane()
    {
        int randomLanesIndex = Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randomLanesIndex];
        availableLanes.RemoveAt(randomLanesIndex);
        return selectedLane;
    }

}
