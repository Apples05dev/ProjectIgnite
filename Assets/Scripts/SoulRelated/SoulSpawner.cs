
using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;

public class SoulSpawner : InteractableObject
{
    [SerializeField] private List<SoulData> soulDataArray;
    [SerializeField] private GameObject soul;
    internal GameObject activeSoul;
    [SerializeField] private AudioClip soulGetSoundEffect;
    private AudioSource audioSource;

    private VesselSpawner vesselSpawner;
    private bool existingSoul = false;
    internal bool anActiveSoulExists = false;
    private LogBookUI logBookUI;

    private void Start()
    {
        logBookUI = GetComponent<LogBookUI>();
        vesselSpawner = GetComponentInChildren<VesselSpawner>();
        audioSource = GetComponent<AudioSource>();
    }
    void SpawnSouls()
    {
        if (soulDataArray.Count == 0)
        {
            Debug.Log("No souls left to spawn!");
            return;
        }

        List<SoulData> validSouls = soulDataArray.Where(soulData =>
            vesselSpawner.spawnedVessels.Any(vessel => vessel.name.Contains(soulData.correctVessel.name))
        ).ToList();

        if (validSouls.Count == 0)
        {
            Debug.Log("No matching vessels found in the scene for the remaining souls!");
            return;
        }

        // 2. Pick a random soul from ONLY the valid list
        int randomIndex = Random.Range(0, validSouls.Count);
        SoulData chosenSoul = validSouls[randomIndex];

        // 3. Safe to spawn now
        activeSoul = Instantiate(soul, transform.position, Quaternion.identity);
        activeSoul.GetComponent<SoulObject>().chosenSoul = chosenSoul;

        logBookUI.StartingHint(chosenSoul.hints[0]);

        // Remove it from your master tracking list so it isn't picked again
        soulDataArray.Remove(chosenSoul);
        anActiveSoulExists = true;
    }

    public override void Interact() 
    {
        base.Interact();
        if (activeSoul != null)
        {
            anActiveSoulExists = true;
        }
        else
        {
            anActiveSoulExists = false;
        }
        if (activeSoul == null && !anActiveSoulExists)
        {
            audioSource.PlayOneShot(soulGetSoundEffect);
            SpawnSouls();
        }
        else
        {
            Debug.Log("A soul is already active!");
        }

    }
}
