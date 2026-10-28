using UnityEngine;

public class Vessels : InteractableObject
{
    [SerializeField] private GameObject soul;
    private GameObject player;
    private HealthBarsManager healthBarsManager;

    private void OnDisable()
    {
        player.GetComponent<Player>()._vesselsInRange.Remove(this);
        ToggleInteractMarker(false);
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        healthBarsManager = FindAnyObjectByType<HealthBarsManager>();

    }
    
    public override void Interact() 
    { 
        base.Interact();
        soul = GameObject.FindGameObjectWithTag("Soul");
        if(soul.GetComponent<SoulObject>().chosenSoul.correctVessel.name == gameObject.name)
        {
            Debug.Log("Correct vessel!");
            Destroy(soul);

            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Incorrect vessel!");
            healthBarsManager.DecreaseHealth();
        }

    }
}