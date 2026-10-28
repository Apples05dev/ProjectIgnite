
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

    private string humanityGained =
        "You have peered too far into the soul.\n" +
        "Careful, lest thee gain their vanity, and lose thy purity.\n" +
        "For when that heart is complete, so too, wlll you fall";

    private string healthLost =
        "You have chosen the wrong vessel..\n" +
        "The soul has rejected you, and your health has been diminished.\n" +
        "Be careful, for if your health reaches zero, we will have no more need for you.";


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void DecreaseHealth()
    {
        healthBarPrefab.value -= 1;
        ShowAlert(healthLost);
    }

    public void IncreaseHumanity()
    {
        humanityValue.fillAmount += 0.25f;
        ShowAlert(humanityGained);
    }

    public void ShowAlert(string message)
    {
        audioSource.PlayOneShot(alertSound);

        // Stop any currently running alert animation
        if (alertCoroutine != null)
        {
            StopCoroutine(alertCoroutine);
        }

        alertCoroutine = StartCoroutine(ShowAlertRoutine(message));
    }

    private IEnumerator ShowAlertRoutine(string message)
    {
        CanvasGroup canvasGroup = alert.GetComponent<CanvasGroup>();

        
        if (canvasGroup == null)
        {
            canvasGroup = alert.gameObject.AddComponent<CanvasGroup>();
        }

        
        alert.gameObject.SetActive(true);
        alert.GetComponentInChildren<TextMeshProUGUI>().text = message;

      
        yield return StartCoroutine(Fade(canvasGroup, 0f, 1f));

        
        yield return new WaitForSeconds(displayDuration);

        
        yield return StartCoroutine(Fade(canvasGroup, 1f, 0f));

        alert.gameObject.SetActive(false);

        alertCoroutine = null;
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float startAlpha, float endAlpha)
    {
        float elapsed = 0f;

        canvasGroup.alpha = startAlpha;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;

            canvasGroup.alpha = Mathf.Lerp(
                startAlpha,
                endAlpha,
                Mathf.Clamp01(elapsed / fadeDuration)
            );

            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }
}
