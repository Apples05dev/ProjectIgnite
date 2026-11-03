using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite humanoidSprite; // Array of sprites for different states
    [SerializeField] private Sprite angelSprite; // Array of sprites for different states



    private void Start()
    {
       spriteRenderer = GetComponentInChildren<SpriteRenderer>();
      
    }

    private void Update()
    {
        if (HealthBarsManager.humanityAmount > 0.50f)
        {
            spriteRenderer.sprite = humanoidSprite;
        }
        else
        {
            spriteRenderer.sprite = angelSprite;
        }

        Debug.Log($"Current Humanity Amount: {HealthBarsManager.humanityAmount}");
    }
}
