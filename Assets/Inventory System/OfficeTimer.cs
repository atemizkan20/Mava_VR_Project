using UnityEngine;
using TMPro;       // If using standard UI Text
using UnityEngine.SceneManagement; // For loading scenes

public class OfficeTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float timeLimit = 60f;  // e.g., 60 seconds in the office
    private float timeLeft;

    [Header("UI Elements")]
    public TextMeshProUGUI timerText;  // Drag a Text UI component here in Inspector

    [Header("Scene To Load When Time Up")]
    public string courtSceneName = "CourtScene"; // The name of the court scene

    private bool timerActive = true;

    void Start()
    {
        // Start the timer
        timeLeft = timeLimit;
        UpdateTimerUI();
    }

    void Update()
    {
        if (!timerActive) return;

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0f)
        {
            timeLeft = 0f;
            timerActive = false;

            // Kick the user out of the office by loading the Court scene
            SceneManager.LoadScene(courtSceneName);
        }

        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int seconds = Mathf.CeilToInt(timeLeft);
            timerText.text = "Time Left: " + seconds + "s";
        }
    }
}
