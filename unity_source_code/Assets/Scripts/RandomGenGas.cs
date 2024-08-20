using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenGas : MonoBehaviour
{
    public GameObject gasoline;
    // Fixed position where at least one gasoline should spawn
    public Vector2 fixedPosition;

    // List of specific spawn positions
    public Vector2[] spawnPositions;
    public int numberOfGasolineToSpawn;

    void Start()
    {
        SpawnObjectAtRandomPosition();
    }

    void SpawnObjectAtRandomPosition()
    {
        // Ensure there are positions defined
        if (spawnPositions.Length == 0)
        {
            Debug.LogWarning("No spawn positions defined!");
            return;
        }

        // Ensure the number of objects to spawn is not greater than the number of positions available
        if (numberOfGasolineToSpawn > spawnPositions.Length)
        {
            Debug.LogWarning("Number of objects to spawn exceeds the number of available positions!");
            numberOfGasolineToSpawn = spawnPositions.Length;
        }

        // To track used positions
        List<int> usedIndices = new List<int>();

        Instantiate(gasoline, fixedPosition, Quaternion.identity);

        for (int i = 0; i < numberOfGasolineToSpawn; i++)
        {
            int randomIndex;

            do
            {
                randomIndex = Random.Range(0, spawnPositions.Length);
            } while (usedIndices.Contains(randomIndex));

            usedIndices.Add(randomIndex);

            Vector2 spawnPosition = spawnPositions[randomIndex];
            Instantiate(gasoline, spawnPosition, Quaternion.identity);
        }
    }
}
