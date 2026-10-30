using System.Collections;
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
        if (soul.GetComponent<SoulObject>().chosenSoul.correctVessel.name == gameObject.name)
        {
            Debug.Log("Correct vessel!");
            Destroy(soul);
            healthBarsManager.PlayCorrectChoiceSound();
            StartCoroutine(AscendOrDescend(gameObject));
            healthBarsManager.UpdateScore();
           
        }
        else
        {
            Debug.Log("Incorrect vessel!");
            healthBarsManager.DecreaseHealth();
                        StartCoroutine(Shake(gameObject));
        }

    }


    IEnumerator Shake(GameObject vessel, float duration = 0.5f, float magnitude = 0.1f)
    {
        Vector3 originalPosition = vessel.transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            vessel.transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        vessel.transform.localPosition = originalPosition;
    }


    IEnumerator AscendOrDescend(GameObject vessel, float duration = 1.0f, float speed = 2.0f)
    {
        
        if (vessel.TryGetComponent<Collider>(out Collider col)) col.enabled = false;
        if (vessel.TryGetComponent<Collider2D>(out Collider2D col2D)) col2D.enabled = false;

        
        SpriteRenderer spriteRenderer = vessel.GetComponent<SpriteRenderer>();
        Material originalMaterial = spriteRenderer.material;

        
        spriteRenderer.material = new Material(Shader.Find("UI/Default"));
        spriteRenderer.color = Color.white;


        Vector3 direction = (Random.value < 0.5f) ? Vector3.up : Vector3.down;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            vessel.transform.Translate(direction * speed * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}

