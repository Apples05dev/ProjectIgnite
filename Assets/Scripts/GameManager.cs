using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private HealthBarsManager healthBarsManager;
    private bool becomeHuman = false;
    private bool madeWrongChoice = false;
    private bool isTransitioning = false;



    public Image fadeOverlayImage;
    public MonoBehaviour playerInputScript;

    void Start()
    {
        healthBarsManager = GetComponent<HealthBarsManager>();
        if (fadeOverlayImage != null)
        {
            fadeOverlayImage.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (isTransitioning) return;

        becomeHuman = healthBarsManager.hasBecomeHuman;
        madeWrongChoice = healthBarsManager.hasFailed;

        if (becomeHuman)
        {
            isTransitioning = true;
            Debug.Log("Player has become human.");
            StartCoroutine(LoadSceneAfterDelay("HumanEnding", 5f));
        }
        else if (madeWrongChoice)
        {
            isTransitioning = true;
            Debug.Log("Player has made the wrong choice.");
            StartCoroutine(LoadSceneAfterDelay("BadEnding", 5f));
        }
        else if (healthBarsManager.hasWon)
        {
            isTransitioning = true;
            Debug.Log("Player has won.");
            StartCoroutine(LoadSceneAfterDelay("GoodEnding", 5f));
        }



        IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
        {
            if (playerInputScript != null)
            {
                playerInputScript.enabled = false;
            }

            if (fadeOverlayImage != null)
            {
                fadeOverlayImage.gameObject.SetActive(true);
                Color overlayColor = fadeOverlayImage.color;
                overlayColor.a = 0f;
                fadeOverlayImage.color = overlayColor;

                float elapsedTime = 0f;
                while (elapsedTime < delay)
                {
                    elapsedTime += Time.deltaTime;
                    overlayColor.a = Mathf.Clamp01(elapsedTime / delay);
                    fadeOverlayImage.color = overlayColor;
                    yield return null;
                }
            }
            else
            {
                yield return new WaitForSeconds(delay);
            }

            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
