using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite humanoidSprite; // Array of sprites for different states


    private void Start()
    {
       spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        Debug.Log("Humanity Amount: " + HealthBarsManager.humanityAmount);
        if (HealthBarsManager.humanityAmount > 0.50f)
        {
            spriteRenderer.sprite = humanoidSprite;
        }
    }
}
