
using UnityEngine;
using System.Collections.Generic;

public class SoulSpawner : MonoBehaviour
{
    [SerializeField] private List<SoulData> soulDataArray;
    [SerializeField] private GameObject soul;
    internal GameObject activeSoul;

    internal bool anActiveSoulExists = false;
    private LogBookUI logBookUI;

    private void Start()
    {
        logBookUI = GetComponent<LogBookUI>();
    }
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



        activeSoul = Instantiate(soul, transform.position, Quaternion.identity);
        logBookUI.StartingHint(chosenSoul.hints[0]);
        activeSoul.GetComponent<SoulObject>().chosenSoul = chosenSoul;
        soulDataArray.RemoveAt(randomIndex);
        anActiveSoulExists = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!anActiveSoulExists)
            {
                SpawnSouls();
            }
        }
    }
}
