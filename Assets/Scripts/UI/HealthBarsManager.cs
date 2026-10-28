using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarsManager : MonoBehaviour
{
    [SerializeField] private Slider healthBarPrefab;
    [SerializeField] private Image humanityValue;
    [SerializeField] public Image alert;

    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float displayDuration = 3f;

    [SerializeField] private AudioClip alertSound;
    private AudioSource audioSource;

    private Coroutine alertCoroutine;
    private TextMeshProUGUI alertText;

    public static float humanityAmount { get; private set; } = 0;

    private string humanityGained =
    "You have gazed too deep.\n" +
    "And lost some purity";

    private string healthLost =
        "The vessel rejects you. Fool";


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        alertText = alert.GetComponentInChildren<TextMeshProUGUI>();

        // Start fully hidden and disabled
        SetAlertAlpha(0f);
        alert.gameObject.SetActive(false);
     

    }

    public void DecreaseHealth()
    {
        Debug.Log("Health decreased");
        healthBarPrefab.value -= 1;
        ShowAlert(healthLost);
    }

    public void IncreaseHumanity()
    {
        humanityValue.fillAmount += 0.25f;
        humanityAmount = humanityValue.fillAmount;
        ShowAlert(humanityGained);

    }

    public void ShowAlert(string message)
    {
        if (alertSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(alertSound);
        }

        if (alertCoroutine != null)
        {
            StopCoroutine(alertCoroutine);
        }

        alertCoroutine = StartCoroutine(ShowAlertRoutine(message));
    }

    private IEnumerator ShowAlertRoutine(string message)
    {
        // Initialize at 0 alpha before turning on to prevent visual pops
        SetAlertAlpha(0f);
        alert.gameObject.SetActive(true);

        if (alertText != null)
        {
            alertText.text = message;
        }

        // Fade in: From transparent (0.0) up to half opacity (0.5)
        yield return StartCoroutine(FadeAlert(0f, 0.5f));

        // Hold at half opacity
        yield return new WaitForSeconds(displayDuration);

        // Fade out: From half opacity (0.5) back down to transparent (0.0)
        yield return StartCoroutine(FadeAlert(0.5f, 0f));

        // Disable immediately when fully transparent
        alert.gameObject.SetActive(false);

        alertCoroutine = null;
    }

    private IEnumerator FadeAlert(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;

        Color imgColor = alert.color;
        Color textColor = alertText != null ? alertText.color : Color.white;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float currentAlpha = Mathf.Lerp(startAlpha, endAlpha, Mathf.Clamp01(elapsed / fadeDuration));

            imgColor.a = currentAlpha;
            alert.color = imgColor;

            if (alertText != null)
            {
                textColor.a = currentAlpha;
                alertText.color = textColor;
            }

            yield return null;
        }

        // Snap precisely to target value at the end of the loop
        imgColor.a = endAlpha;
        alert.color = imgColor;
        if (alertText != null)
        {
            textColor.a = endAlpha;
            alertText.color = textColor;
        }
    }

    private void SetAlertAlpha(float alpha)
    {
        Color imgColor = alert.color;
        imgColor.a = alpha;
        alert.color = imgColor;

        if (alertText != null)
        {
            Color textColor = alertText.color;
            textColor.a = alpha;
            alertText.color = textColor;
        }
    }
}
