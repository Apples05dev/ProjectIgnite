using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LogBookUI : MonoBehaviour
{
    public RectTransform logBook;
    public float slideDuration = 0.6f;
    [SerializeField] private TMPro.TextMeshProUGUI soulDescription;
    [SerializeField] public Image alertNotifcation;

    private SoulSpawner soulSpawner;
    // Tracks whether the logbook is currently visible on screen
    private bool isOpened = false;
    void Start()
    {
        soulSpawner = GetComponent<SoulSpawner>();
    }


    #region bookPositioning
    public void ToggleLogBook()
    {
        // Flip the state when the button is clicked
        isOpened = !isOpened;
        alertNotifcation.gameObject.SetActive(false);

        StopAllCoroutines();
        StartCoroutine(SlideBook(isOpened));
    }

    IEnumerator SlideBook(bool open)
    {
        // Ensure the pivot and anchors remain at the top-right corner
        logBook.pivot = new Vector2(1, 1);
        logBook.anchorMin = new Vector2(1, 1);
        logBook.anchorMax = new Vector2(1, 1);

        float bookWidth = logBook.rect.width;

        // Position references
        Vector2 outOfBoundsPos = new Vector2(bookWidth, -20);
        Vector2 onScreenPos = new Vector2(-20, -20);

        // Determine starting and ending vectors based on the bool state
        Vector2 startPos = open ? outOfBoundsPos : onScreenPos;
        Vector2 targetPos = open ? onScreenPos : outOfBoundsPos;

        float elapsed = 0f;

        //if starting fresh or interrupted, smoothly transition from where it currently sits
        Vector2 currentPos = logBook.anchoredPosition;

        while (elapsed < slideDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / slideDuration;

            // Smooth interpolation
            t = Mathf.SmoothStep(0f, 1f, t);

            // Lerp from current actual position to target to prevent harsh snapping if clicked rapidly
            logBook.anchoredPosition = Vector2.Lerp(currentPos, targetPos, t);
            yield return null;
        }

        logBook.anchoredPosition = targetPos;
    }
    #endregion]


    public void StartingHint(string hint) 
    { 
        soulDescription.text = hint;
        ShowAlertNotification();
    }

    public void AddHint() 
    {
        ShowAlertNotification();
      for (int i = 0; i < soulSpawner.activeSoul.GetComponent<SoulObject>().chosenSoul.hints.Length; i++)
        {
            if (soulDescription.text.Contains(soulSpawner.activeSoul.GetComponent<SoulObject>().chosenSoul.hints[i]))
            {
                continue;
            }
            else
            {
                string newHint = soulSpawner.activeSoul.GetComponent<SoulObject>().chosenSoul.hints[i];
                soulDescription.text += "\n" + newHint;
                break;
            }
        }
    }


    public void ShowAlertNotification()
    {
        if (isOpened)
        {
          
            alertNotifcation.gameObject.SetActive(false);
        }
        else
        {
            
            alertNotifcation.gameObject.SetActive(true);
        }
    }
}
