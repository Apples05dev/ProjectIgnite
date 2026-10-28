using System.Collections.Generic;
using UnityEngine;

public class VesselSpawner : MonoBehaviour
{
    [SerializeField] internal List<GameObject> vesselArray;
    [SerializeField] private GameObject floor;
    [SerializeField] internal List<GameObject> spawnedVessels = new List<GameObject>();

    [SerializeField] private int amountToSpawn;

    [SerializeField] private float heightOffset = 0.5f;
    private Collider floorCollider;


    private void Start()
    {
        floorCollider = floor.GetComponent<Collider>();
        for (int i = 0; i < amountToSpawn; i++)
        {
            SpawnVessel();
        }
    }

    private void SpawnVessel()
    {

        GameObject vessel = vesselArray[Random.Range(0, vesselArray.Count)];
        Bounds bounds = floorCollider.bounds;

        float padding = 2.0f;
        float spawnY = bounds.max.y + heightOffset;

        Vector3 randomSpawnPosition = Vector3.zero;
        bool validPositionFound = false;

      
        float clearanceRadius = 1.0f;

        while (!validPositionFound)
        {
           float randomX = Random.Range(bounds.min.x + padding, bounds.max.x - padding);
            float randomZ = Random.Range(bounds.min.z + padding, bounds.max.z - padding);
            randomSpawnPosition = new Vector3(randomX, spawnY, randomZ);

           
            Collider[] colliders = Physics.OverlapSphere(randomSpawnPosition, clearanceRadius);

            bool spotIsClear = true;
            foreach (var col in colliders)
            {
                if (col.gameObject != floor)
                {
                    spotIsClear = false;
                    break;
                }
            }

            if (spotIsClear)
            {
                validPositionFound = true;
            }
        }

       
        if (validPositionFound)
        {
            vesselArray.Remove(vessel);
            Instantiate(vessel, randomSpawnPosition, floor.transform.rotation);
            spawnedVessels.Add(vessel);
        }

    }
}