using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HealthBarsManager : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider healthBarPrefab;
    [SerializeField] private UnityEngine.UI.Image humanityValue;

    public void DecreaseHealth() 
    { 
        healthBarPrefab.value -= 1;
    }

    public void IncreaseHumanity() 
    { 
        humanityValue.fillAmount += 0.25f;
    }
}
