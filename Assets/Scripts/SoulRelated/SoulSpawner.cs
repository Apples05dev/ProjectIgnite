
using UnityEngine;
using System.Collections.Generic;

public class SoulSpawner : MonoBehaviour
{
    [SerializeField] private List<SoulData> soulDataArray;
    [SerializeField] private GameObject soul;

    void SpawnSouls()
    {
        if (soulDataArray.Count == 0)
        {
            Debug.Log("No souls left to spawn!");
            return;
        }

        // Choose a random index
        int randomIndex = Random.Range(0, soulDataArray.Count);

        SoulData chosenSoul = soulDataArray[randomIndex];
        soulDataArray.RemoveAt(randomIndex);

      
        GameObject spawnedSoul = Instantiate(soul, transform.position, Quaternion.identity);
        spawnedSoul.GetComponent<SoulObject>().chosenSoul = chosenSoul;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnSouls();
        }
    }
}
