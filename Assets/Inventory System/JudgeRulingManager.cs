using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class JudgeRulingManager : MonoBehaviour
{
    [Header("Ruling Timer")]
    public float rulingTime = 60f; // seconds to place clues
    private float timeLeft;
    private bool rulingActive = true;

    [Header("Required Clues for Good Ending")]
    public int requiredClues = 4;
    private int currentCluesOnTable = 0;

    [Header("UI Elements")]
    public TextMeshProUGUI rulingTimerText;
    public CanvasGroup endScreenCanvas;
    public TextMeshProUGUI endMessageText;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip goodEndingClip;
    public AudioClip badEndingClip;

    void Start()
    {
        timeLeft = rulingTime;
        UpdateTimerUI();

        if (endScreenCanvas != null)
        {
            endScreenCanvas.alpha = 0f;
            endScreenCanvas.interactable = false;
            endScreenCanvas.blocksRaycasts = false;
        }
    }

    void Update()
    {
        if (!rulingActive) return;

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0f)
        {
            timeLeft = 0f;
            rulingActive = false;

            if (currentCluesOnTable >= requiredClues)
            {
                TriggerGoodEnding();
            }
            else
            {
                TriggerBadEnding();
            }
        }
        UpdateTimerUI();
    }

    public void OnCluePlaced()
    {
        currentCluesOnTable++;

        if (currentCluesOnTable >= requiredClues && rulingActive)
        {
            rulingActive = false;
            TriggerGoodEnding();
        }
    }

    void UpdateTimerUI()
    {
        if (rulingTimerText != null)
        {
            int seconds = Mathf.CeilToInt(timeLeft);
            rulingTimerText.text = "Time left: " + seconds + "s";
        }
    }

    void TriggerGoodEnding()
    {
        rulingActive = false;

        if (audioSource != null && goodEndingClip != null)
        {
            audioSource.clip = goodEndingClip;
            audioSource.Play();
            StartCoroutine(DelayedFadeToBlackAndShowMessage("Congratulations! You've won the case!", 9f));
        }
    }

    void TriggerBadEnding()
    {
        rulingActive = false;

        if (audioSource != null && badEndingClip != null)
        {
            audioSource.clip = badEndingClip;
            audioSource.Play();
            StartCoroutine(DelayedFadeToBlackAndShowMessage("Unfortunately, you have not been able to expose the mayor...", 3f));
        }
    }

    System.Collections.IEnumerator DelayedFadeToBlackAndShowMessage(string msg, float delay)
    {
        if (endMessageText != null) endMessageText.text = msg;

        yield return new WaitForSeconds(delay);

        float duration = 2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsed / duration);
            endScreenCanvas.alpha = alpha;
            yield return null;
        }

        endScreenCanvas.interactable = true;
        endScreenCanvas.blocksRaycasts = true;
    }
}
